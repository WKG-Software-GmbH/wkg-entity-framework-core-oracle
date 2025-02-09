using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Configuration.Discovery;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration.Reflection;

internal class OracleProcedureDiscoveryContext : ProcedureDiscoveryContext
{
    private static readonly List<WeakReference<ModelBuilder>> s_configuredModelBuilders = [];

    protected override List<WeakReference<ModelBuilder>> StaticModelBuilderCache => s_configuredModelBuilders;
}
