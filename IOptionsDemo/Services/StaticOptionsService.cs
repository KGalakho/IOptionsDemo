using IOptionsDemo.Configuration;
using Microsoft.Extensions.Options;

namespace IOptionsDemo.Services;
public class StaticOptionsService(IOptions<FeatureOptions> options)
{

    public string GetMessage() => options.Value.Enabled ? options.Value.Message : "Feature disabled";

}