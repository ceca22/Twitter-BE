using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.DataAccess;
using TwitterApp.DataAccess.Implementations;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;
using TwitterApp.Services.Implementations;
using TwitterApp.Services.Interfaces;

namespace TwitterApp.Helper
{
    public class DependencyInjectionHelper
    {

        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TwitterAppDbContext>(x =>
                x.UseSqlServer(connectionString));
        }

        public static void InjectRepository(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostDetailsRepository, PostDetailsRepository>();
            



        }


        public static void InjectServices(IServiceCollection services)
        {

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<ILoginService, LoginService>();



        }
    }
}
