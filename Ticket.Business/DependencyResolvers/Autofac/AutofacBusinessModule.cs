﻿using Autofac;
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

            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerDependency();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerDependency();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).InstancePerDependency();
        }
    }
}
