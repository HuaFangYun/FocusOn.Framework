using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.EquivalencyExpression;

using FocusOn.Framework.Endpoint.HttpApi.Test.Host.BusinessServices;
using FocusOn.Framework.Endpoint.HttpApi.Test.Host.Controllers;
using FocusOn.Framework.Endpoint.HttpApi.Test.WebHost.BusinessServices.Entities;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework.Endpoint.HttpApi.UnitTest
{
    public abstract class TestBase : IDisposable
    {
        private readonly IServiceProvider _provider;
        const string file = "./file.db";
        const string connectionString = $"DataSource={file}";
        private SqliteConnection _con;

        public TestBase()
        {
            var services = new ServiceCollection();
            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddFocusOn(options =>
            {
                options.AddAutoMapper(typeof(UserController).Assembly)
                ;

                options.AddAutoMapper((serviceProvider, mapper) =>
                {
                    mapper.CreateMap<User, User>();

                }, typeof(TestDbContext).Assembly);
            });

            services.AddTransient<ITestUserBusinessService, UserController>();

            _provider = services.BuildServiceProvider();


            var db = _provider.GetService<TestDbContext>().Database;
            if (db.EnsureCreated())
            {
                //var migrations=db.GetPendingMigrations();
                //if (migrations.Any())
                //{
                //    db.Migrate();
                //}
            }
        }

        protected TService? GetService<TService>() => _provider.GetService<TService>();
        protected TService GetRequiredService<TService>() where TService : notnull
            => _provider.GetRequiredService<TService>();

        public void Dispose()
        {
            var db = _provider.GetService<TestDbContext>();

            var users = db.Set<User>().ToList();

            db.RemoveRange(users);
            db.SaveChanges();

            if (File.Exists(file))
            {
                //File.Delete(file);
            }
        }
    }
}
