﻿using Microsoft.Extensions.DependencyInjection;
using RiotSharp.Http;
using RiotSharp.Interfaces;
using System;

namespace RiotSharp.AspNetCore
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Adds and configures RiotSharp's services
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddRiotSharp(this IServiceCollection serviceCollection, Action<RiotSharpOptions> options)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var riotSharpOptions = new RiotSharpOptions();
            options(riotSharpOptions);

            if (riotSharpOptions.TournamentApi.ApiKey == null && riotSharpOptions.RiotApi.ApiKey == null)
                throw new ArgumentNullException("No api key provided.", innerException: null);

            if (riotSharpOptions.RiotApi.ApiKey != null)
            {
                var rateLimitedRequester = new RateLimitedRequester(riotSharpOptions.RiotApi.ApiKey,
                    riotSharpOptions.RiotApi.RateLimits);
                serviceCollection.AddSingleton<ITournamentRiotApi>(serviceProvider => 
                    new TournamentRiotApi(rateLimitedRequester));
                serviceCollection.AddSingleton<IRiotApi>(serviceProvider => new RiotApi(rateLimitedRequester));

                var requester = new Requester(riotSharpOptions.RiotApi.ApiKey);

                if(riotSharpOptions.UseMemoryCache)
                {
                    serviceCollection.AddMemoryCache();
                    serviceCollection.AddSingleton<ICache, MemoryCache>();
                }
                else
                    serviceCollection.AddSingleton<ICache, Cache>();            

                serviceCollection.AddSingleton<IStaticRiotApi>(serviceProvider => 
                    new StaticRiotApi(requester, serviceProvider.GetRequiredService<ICache>())); 
            }

            if (riotSharpOptions.TournamentApi.ApiKey != null)
            {
                var rateLimitedRequester = new RateLimitedRequester(riotSharpOptions.TournamentApi.ApiKey, 
                    riotSharpOptions.TournamentApi.RateLimits);
                serviceCollection.AddSingleton<ITournamentRiotApi>(serviceProvider => 
                    new TournamentRiotApi(rateLimitedRequester, riotSharpOptions.TournamentApi.UseStub));
            }
            
            return serviceCollection;
        }
    }
}
