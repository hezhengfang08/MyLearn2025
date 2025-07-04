﻿using MySelf.MSACommerce.HttpApi.Common;

namespace MySelf.MSACommerce.SeckillService.HttpApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpApi(this IServiceCollection services)
        {
            services.AddHttpApiCommon();

            return services;
        }
    }
}
