using AspNetCore.AsyncInitialization;
using Elsa.Activities.Http.Drivers;
using Elsa.Activities.Http.Initialization;
using Elsa.Activities.Http.Services;
using Elsa.Activities.Http.Services.Implementations;
using Elsa.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Elsa.Activities.Http.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpWorkflowDescriptors(this IServiceCollection services)
        {
            return services.AddActivityDescriptors<ActivityDescriptors>();
        }

        public static IServiceCollection AddHttpWorkflowDrivers(this IServiceCollection services)
        {
            services.AddHttpWorkflowDescriptors();
            services.TryAddSingleton<IHttpWorkflowCache, DefaultHttpWorkflowCache>();
            services.AddAsyncInitialization();
            services.TryAddTransient<IAsyncInitializer, HttpWorkflowCacheInitializer>();
            services.AddActivityDriver<HttpRequestTriggerDriver>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            return services;
        }
    }
}