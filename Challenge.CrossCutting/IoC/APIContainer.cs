using Challenge.Core.Interfaces.Repositories;
using Challenge.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.CrossCutting.IoC
{
    public class APIContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICheckingAccountRepository, AccountRepository>();
            services.AddSingleton<ICheckingAccountRepository, AccountRepository>();

        }
    }
}
