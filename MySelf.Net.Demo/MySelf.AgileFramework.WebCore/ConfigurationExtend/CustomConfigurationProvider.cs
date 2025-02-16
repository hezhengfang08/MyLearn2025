using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.AgileFramework.WebCore.ConfigurationExtend
{
    public class CustomConfigurationProvider: ConfigurationProvider
    {
        private CustomConfigurationOptions _CustomConfigurationOption = null;
        public CustomConfigurationProvider(CustomConfigurationOptions customConfigurationOption)
        {
            this._CustomConfigurationOption = customConfigurationOption;
        }

        public override void Load()
        {
            Console.WriteLine($"CustomConfigurationProvider load data");
            //当然也可以从数据库读取
            //var result = this._CustomConfigurationOption.DataInitFunc.Invoke();
            //this._CustomConfigurationOption.DataChangeAction()

            base.Data.Add("TodayCustom", "0117-Custom");
            base.Data.Add("RabbitMQOptions-Custom:HostName", "192.168.3.254-Custom");
            base.Data.Add("RabbitMQOptions-Custom:UserName", "guest-Custom");
            base.Data.Add("RabbitMQOptions-Custom:Password", "guest-Custom");
        }
        public override bool TryGet(string key, out string? value)
        {
            return base.TryGet(key, out value);
        }

        public override void Set(string key, string? value)
        {
            base.Set(key, value);
        }
    }
}
