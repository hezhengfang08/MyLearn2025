﻿using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Enums;
using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul;

namespace MySelf.MSACommerce.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IApolloConfigurationBuilder AddConfigCenter(this ConfigurationManager configurationManager, string defaultNamespace)
        {
            configurationManager.AddConsul("myself-commerce/appsettings.json", source =>
            {
                source.ReloadOnChange = true;
            });
            return configurationManager
                    .AddApollo(configurationManager.GetSection(nameof(ApolloOptions)))
                    .AddDefault(ConfigFileFormat.Json)
                    .AddNamespace(defaultNamespace, ConfigFileFormat.Json);

        }
    }
}
