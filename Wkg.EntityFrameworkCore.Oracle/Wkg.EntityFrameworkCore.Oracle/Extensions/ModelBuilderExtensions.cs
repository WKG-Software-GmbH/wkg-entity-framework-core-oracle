using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Wkg.EntityFrameworkCore.Configuration.Reflection.Discovery;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration.Reflection;

namespace Wkg.EntityFrameworkCore.Oracle.Extensions;

/// <summary>
/// Extension methods for <see cref="ModelBuilder"/>.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Loads and configures the specified <typeparamref name="TProcedure"/> stored procedure with the specified <typeparamref name="TIOContainer"/> input/output container.
    /// </summary>
    /// <typeparam name="TProcedure">The type of the stored procedure command object to load and configure.</typeparam>
    /// <typeparam name="TIOContainer">The type of the input/output container used by <typeparamref name="TProcedure"/>.</typeparam>
    /// <param name="builder">The <see cref="ModelBuilder"/> to use.</param>
    /// <returns>The <see cref="ModelBuilder"/> instance.</returns>
    public static ModelBuilder LoadProcedure<TProcedure, TIOContainer>(this ModelBuilder builder)
        where TProcedure : OracleStoredProcedure<TIOContainer>,
            IProcedureConfiguration<TProcedure, TIOContainer>
        where TIOContainer : class
    {
        OracleProcedureBuilder<TProcedure, TIOContainer> procedure = builder.Procedure<TProcedure, TIOContainer>();
        TProcedure.Configure(procedure);
        OracleProcedureBuildPipeline.Execute(procedure);
        return builder;
    }

    /// <summary>
    /// Retrieves a <see cref="OracleProcedureBuilder{TProcedure,TIOContainer}"/> instance for the specified <typeparamref name="TProcedure"/> stored procedure with the specified <typeparamref name="TIOContainer"/> input/output container to be used for fluent configuration.
    /// </summary>
    /// <typeparam name="TProcedure">The type of the stored procedure command object to load and configure.</typeparam>
    /// <typeparam name="TIOContainer">The type of the input/output container used by <typeparamref name="TProcedure"/>.</typeparam>
    /// <param name="_">The <see cref="ModelBuilder"/> to use.</param>
    public static OracleProcedureBuilder<TProcedure, TIOContainer> Procedure<TProcedure, TIOContainer>(this ModelBuilder _)
        where TProcedure : OracleStoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>
        where TIOContainer : class =>
            new();

    /// <summary>
    /// Reflectively loads and configures all stored procedures implementing <see cref="IOracleStoredProcedure{TIOContainer}"/> and <see cref="IReflectiveProcedureConfiguration{TProcedure, TIOContainer}"/> from the calling assembly.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder"/> to use.</param>
    /// <returns>The <see cref="ModelBuilder"/> instance.</returns>
    /// <exception cref="ArgumentNullException">if <paramref name="builder"/> is <see langword="null"/>.</exception>
    public static ModelBuilder LoadReflectiveProcedures(this ModelBuilder builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        ReflectiveProcedureConfigurationLoader.LoadAll(builder, null);
        return builder;
    }

    /// <summary>
    /// Reflectively loads and configures all stored procedures implementing <see cref="IOracleStoredProcedure{TIOContainer}"/> and <see cref="IReflectiveProcedureConfiguration{TProcedure, TIOContainer}"/> from the calling assembly.
    /// </summary>
    /// <param name="builder">The <see cref="ModelBuilder"/> to use.</param>
    /// <param name="configureOptions">The action to configure the options for the reflective procedure discovery.</param>
    /// <returns>The <see cref="ModelBuilder"/> instance.</returns>
    /// <exception cref="ArgumentNullException">if <paramref name="builder"/> is <see langword="null"/>.</exception>
    public static ModelBuilder LoadReflectiveProcedures(this ModelBuilder builder, Action<IDiscoveryOptionsBuilder>? configureOptions = null)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        Assembly[]? assemblies = null;
        if (configureOptions is not null)
        {
            OracleDiscoveryOptionsBuilder optionsBuilder = new();
            configureOptions(optionsBuilder);
            assemblies = optionsBuilder.Build().TargetAssemblies;
        }
        ReflectiveProcedureConfigurationLoader.LoadAll(builder, assemblies);
        return builder;
    }
}
