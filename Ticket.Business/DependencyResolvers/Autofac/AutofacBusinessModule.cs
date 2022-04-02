using Autofac;
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

namespace Ticket.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdminManager>().As<IAdminService>().SingleInstance();
            builder.RegisterType<EfAdminRepository>().As<IAdminRepository>().SingleInstance();
            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EfCustomerRepository>().As<ICustomerRepository>().SingleInstance();
            builder.RegisterType<FilmManager>().As<IFilmService>().SingleInstance();
            builder.RegisterType<EfFilmRepository>().As<IFilmRepository>().SingleInstance();
            builder.RegisterType<Context>().As<DbContext>().SingleInstance();
        }
    }
}
