
//using Microsoft.AspNetCore.Builder;
using Generally;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Factory;
using UnitOfWorkPJEx_DapperRepository.Factory.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Repository;

namespace UnitOfWorkPJEx_DapperRepository
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IDbConnection>((sp) => new SqlConnection(configuration.GetConnectionString("DefConnectionStr")));

            services.AddScoped<IDbConnection, SqlConnection>(serviceProvider =>
            {
                SqlConnection conn = new SqlConnection();
                //指派連線字串
                conn.ConnectionString = configuration.GetConnectionString("DefConnectionStr");
                services.AddTransient<IDbTransaction>((sp) =>
                {
                    //var connection = sp.GetRequiredService<IDbConnection>();
                    return conn.BeginTransaction();
                });
                return conn;
            });
            services.AddSingleton<IConfiguration>(configuration);

            //services.AddScoped<IConfiguration>(_ => configuration.GetConnectionString("DefConnectionStr"));

            services.AddScoped<IUnitOfWork_Dapper, UnitOfWork_Dapper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepositoryFactory, UserRepositoryFactory>();
           // services.AddScoped<IMsDBConn, MsDBConn>();

            return services;
        }
    }
}
