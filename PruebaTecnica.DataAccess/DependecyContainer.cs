﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DataAccess
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddDALDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("url");

            services.AddScoped<UserDAL>(provider => new UserDAL(connectionString));
            services.AddScoped<QuestionDAL>(provider => new QuestionDAL(connectionString));
            services.AddScoped<AnswerDAL>(provider => new AnswerDAL(connectionString));

            return services;
        }
    }
}
