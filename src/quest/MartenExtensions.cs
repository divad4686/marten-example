using System;
using System.Collections.Generic;
using System.Reflection;
using Marten;
using Marten.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace quest
{
    public static class MartenExtensions
    {
        public static IServiceCollection AddMarten(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["MARTEN_DB"] ?? "Username=david;Password=david;Host=localhost;Port=5433";
            Retry(() => services.AddSingleton<IDocumentStore>(
                    DocumentStore.For(_ =>
                    {
                        _.Connection(host);
                        _.Events.DatabaseSchemaName = "marten_store";
                        _.AutoCreateSchemaObjects = AutoCreate.All;

                    })));

            return services;
        }

        internal static void Retry(Action action, int retries = 5) =>
            Policy.Handle<Exception>()
                .WaitAndRetry(retries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .Execute(action);
    }
}