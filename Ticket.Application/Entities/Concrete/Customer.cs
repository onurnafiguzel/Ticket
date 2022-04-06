﻿using Ticket.Domain.Entities.Abstract;

namespace Ticket.Application.Entities.Concrete
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }

        public ICollection<CustomerOperationClaim> OperationClaims { get; set; } = new HashSet<CustomerOperationClaim>();
    }
}