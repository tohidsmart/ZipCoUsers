using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UserManaging.Tests
{
    public static class TestHelpers
    {
       
        public static ByteArrayContent CreateHttpContent<T>(T content)
        {
            var serializedContent = JsonConvert.SerializeObject(content);
            var byteContent = System.Text.Encoding.UTF8.GetBytes(serializedContent);
            var byteArrayContent = new ByteArrayContent(byteContent);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteArrayContent;
        }

        public static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<T>(responseContent);
            return responseObject;
        }

        public static async Task<HttpResponseMessage> MakePostRequest(HttpClient client, string route, ByteArrayContent content)
        {
            HttpResponseMessage response = await client.PostAsync(route, content);
            return response;
        }

        public static async Task<HttpResponseMessage> MakeGetRequest(HttpClient client, string route)
        {
            HttpResponseMessage response = await client.GetAsync(route);
            return response;
        }

        public static IServiceCollection SetupTestDatabase<TContext>(this IServiceCollection services,SqliteConnection connection) where TContext : DbContext
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddScoped(p =>
            DbContextOptionsFactory<TContext>(p, (sp, options) => options.UseSqlite(connection)));

            return services;
        }

        private static DbContextOptions<TContext> DbContextOptionsFactory<TContext>(
           IServiceProvider applicationServiceProvider,
           Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
           where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>(
                new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));

            builder.UseApplicationServiceProvider(applicationServiceProvider);

            optionsAction?.Invoke(applicationServiceProvider, builder);

            return builder.Options;
        }
    }
}
