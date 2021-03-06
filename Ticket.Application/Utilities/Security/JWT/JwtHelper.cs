using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Ticket.Application.Utilities.Security.Encryption;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Ticket.Application.Extensions;

namespace Ticket.Application.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration _configuration = builder.Build();
            string issuer = _configuration.GetValue<string>("TokenOptions:Issuer");
            string audience = _configuration.GetValue<string>("TokenOptions:Audience");
            int accessTokenExpiration = _configuration.GetValue<int>("TokenOptions:AccessTokenExpiration");
            string securityKey = _configuration.GetValue<string>("TokenOptions:SecurityKey");

            _tokenOptions = new TokenOptions
            {
                AccessTokenExpiration = accessTokenExpiration,
                Issuer = issuer,
                SecurityKey = securityKey,
                Audience = audience
            };
        }

        public AccessToken CreateToken(Customer customer, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, customer, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, Customer customer, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(customer, operationClaims),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(Customer customer, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(customer.Id.ToString());
            claims.AddEmail(customer.Email);
            claims.AddName(customer.Name);
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
