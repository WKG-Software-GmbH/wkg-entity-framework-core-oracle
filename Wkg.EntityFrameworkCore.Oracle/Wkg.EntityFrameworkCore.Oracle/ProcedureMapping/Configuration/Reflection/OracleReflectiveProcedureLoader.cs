using Wkg.EntityFrameworkCore.Configuration.Reflection;
using Wkg.EntityFrameworkCore.Oracle.Extensions;
using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration.Reflection;

internal class OracleReflectiveProcedureLoader : ReflectiveProcedureLoader
{
    protected override StoredPrecedureLoaderOptions Options => new
    (
        typeof(IStoredProcedure),
        typeof(OracleStoredProcedure<>),
        typeof(IReflectiveProcedureConfiguration<,>),
        typeof(ModelBuilderExtensions),
        nameof(ModelBuilderExtensions.LoadProcedure)
    );
}
