using Infnet.Etapa4.Domain.Model.Interfaces.Infrastructure;
using Infnet.Etapa4.Domain.Model.Interfaces.Repositories;
using Infnet.Etapa4.Domain.Model.Interfaces.Services;
using Infnet.Etapa4.Domain.Services.Services;
using Infnet.Etapa4.Infrastructure.Data.Context;
using Infnet.Etapa4.Infrastructure.Data.Repositories;
using Infnet.Etapa4.Infrastructure.Services.Blob;
using Infnet.Etapa4.Infrastructure.Services.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infnet.Etapa4.Infrastructure.IoC
{
    public class DependencyInjectorHelper
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AmizadeContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AmizadeContext")));

            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<IAmigoService, AmigoService>();

            services.AddScoped<IBlobService, BlobService>(provider => 
            new BlobService(configuration.GetValue<string>("ConnectionStringStorageAccount")));

            services.AddScoped<IQueueMessage, QueueMessage>(provider =>
                new QueueMessage(configuration.GetValue<string>("ConnectionStringStorageAccount")));

        }
    }
}
