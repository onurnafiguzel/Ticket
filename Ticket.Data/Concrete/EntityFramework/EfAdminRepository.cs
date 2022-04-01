using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfAdminRepository : EfEntityRepositoryBase<Admin>, IAdminRepository
    {
        public EfAdminRepository(DbContext context) : base(context)
        {
        }
    }
}
