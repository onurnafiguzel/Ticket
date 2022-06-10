using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.Business.Concrete;
using Ticket.Data;
using Ticket.Data.Abstract;
using Ticket.Data.Concrete.EntityFramework;
using Castle.DynamicProxy;
using Ticket.Application.Utilities.Interceptors;
using Ticket.Application.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;
using Ticket.Business.MapperProfile;

namespace Ticket.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdminManager>().As<IAdminService>().InstancePerDependency();
            builder.RegisterType<EfAdminRepository>().As<IAdminRepository>().InstancePerDependency();
            builder.RegisterType<CustomerManager>().As<ICustomerService>().InstancePerDependency();
            builder.RegisterType<EfCustomerRepository>().As<ICustomerRepository>().InstancePerDependency();
            builder.RegisterType<MovieManager>().As<IMovieService>().InstancePerDependency();
            builder.RegisterType<EfMovieRepository>().As<IFilmRepository>().InstancePerDependency();
            builder.RegisterType<TicketContext>().As<DbContext>().InstancePerDependency();
            builder.RegisterType<EfCastRepository>().As<ICastRepository>().InstancePerDependency();
            builder.RegisterType<SessionManager>().As<ISessionService>().InstancePerDependency();
            builder.RegisterType<EfSessionRepository>().As<ISessionRepository>().InstancePerDependency();
            builder.RegisterType<CityManager>().As<ICityService>().InstancePerDependency();
            builder.RegisterType<EfCityRepository>().As<ICityRepository>().InstancePerDependency();
            builder.RegisterType<EfTheatherRepository>().As<ITheatherRepository>().InstancePerDependency();
            builder.RegisterType<EfTheatherPriceRepository>().As<ITheatherPriceRepository>().InstancePerDependency();
            builder.RegisterType<EfTheatherSeatRepository>().As<ITheatherSeatRepository>().InstancePerDependency();
            builder.RegisterType<EfMovieSessionSeatRepository>().As<IMovieSessionSeatRepository>().InstancePerDependency();
            builder.RegisterType<TicketManager>().As<ITicketService>().InstancePerDependency();
            builder.RegisterType<EfTicketRepository>().As<ITicketRepository>().InstancePerDependency();

            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerDependency();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerDependency();
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).InstancePerDependency();
        }
    }
}
