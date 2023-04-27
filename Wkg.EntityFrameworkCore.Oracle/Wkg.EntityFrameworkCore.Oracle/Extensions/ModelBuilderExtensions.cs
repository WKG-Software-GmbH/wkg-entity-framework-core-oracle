using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration.Reflection;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;

namespace Wkg.EntityFrameworkCore.Oracle.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder LoadProcedure<TProcedure, TProcedureContext>(this ModelBuilder builder)
        where TProcedure : OracleStoredProcedure<TProcedureContext>,
            IProcedureConfiguration<TProcedure, TProcedureContext>
        where TProcedureContext : class
    {
        OracleProcedureBuilder<TProcedure, TProcedureContext> procedure = builder.Procedure<TProcedure, TProcedureContext>();
        TProcedure.Configure(procedure);
        OracleProcedureBuildPipeline.Execute(procedure);
        return builder;
    }

    public static OracleProcedureBuilder<TProcedure, TIOContainer> Procedure<TProcedure, TIOContainer>(this ModelBuilder _)
        where TProcedure : OracleStoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>
        where TIOContainer : class =>
            new();

    public static ModelBuilder LoadReflectiveProcedures(this ModelBuilder builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        ReflectiveProcedureConfigurationLoader.LoadAll(builder);
        return builder;
    }
}
