using IOptionsDemo.Configuration;
using Microsoft.Extensions.Options;

namespace IOptionsDemo.Services
{

    public class MonitorOptionsService
    {
        private FeatureOptions _options;

        public MonitorOptionsService(IOptionsMonitor<FeatureOptions> monitor)
        {
            _options = monitor.CurrentValue;
            monitor.OnChange(updated => _options = updated);
        }

        public string GetMessage() => _options.Enabled ? _options.Message : "Feature disabled";
    }

}
