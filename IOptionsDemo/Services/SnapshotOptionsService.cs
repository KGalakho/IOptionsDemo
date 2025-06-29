using IOptionsDemo.Configuration;
using Microsoft.Extensions.Options;

namespace IOptionsDemo.Services;
public class SnapshotOptionsService(IOptionsSnapshot<FeatureOptions> options)
{
    public string GetMessage() => options.Value.Enabled ? options.Value.Message : "Feature disabled";
}

