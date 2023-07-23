using Oracle.ManagedDataAccess.Client;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;

/// <summary>
/// Provides a simple API for configuring a parameter of a stored procedure in an Oracle database.
/// </summary>
internal interface IOracleParameterBuilder : IParameterBuilder
{
    /// <summary>
    /// The <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> associated with the parameter of this Oracle procedure.
    /// </summary>
    OracleDbType? OracleDbType { get; }
}

/// <summary>
/// Provides a simple API for configuring a parameter of a stored procedure in an Oracle database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the Input/Output container object used to pass arguments to and from the stored procedure.</typeparam>
/// <typeparam name="TParameter">The CLR type of the parameter being mapped.</typeparam>
public class OracleParameterBuilder<TIOContainer, TParameter>
    : ParameterBuilder<
        TIOContainer,
        TParameter,
        OracleCompiledParameter,
        OracleParameterBuilder<TIOContainer, TParameter>>,
    IOracleParameterBuilder
    where TIOContainer : class
{
    private static readonly OracleTypeMap _typeMap = new();

    private OracleDbType? OracleDbType { get; set; } = null;

    OracleDbType? IOracleParameterBuilder.OracleDbType => OracleDbType;

    /// <summary>
    /// Creates a new instance of <see cref="OracleParameterBuilder{TIOContainer, TParameter}"/>.
    /// </summary>
    /// <param name="parameterSelector">An expression that selects the parameter from the <typeparamref name="TIOContainer"/> object.</param>
    /// <param name="throwHelper">The <see cref="IProcedureThrowHelper"/> to be used for throwing exceptions.</param>
    public OracleParameterBuilder(Expression<Func<TIOContainer, TParameter>> parameterSelector, IProcedureThrowHelper throwHelper) : base(parameterSelector, throwHelper)
    {
        OracleDbType = _typeMap.GetDbTypeOrDefault(Context.PropertyInfo.PropertyType);
    }

    /// <summary>
    /// Specifies the <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> associated with the parameter of this MySQL procedure.
    /// </summary>
    /// <param name="oracleDbType">The <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> associated with the parameter of this MySQL procedure.</param>
    /// <returns>The current <see cref="OracleParameterBuilder{TIOContainer, TParameter}"/> instance for fluent configuration.</returns>
    public OracleParameterBuilder<TIOContainer, TParameter> HasDbType(OracleDbType oracleDbType)
    {
        OracleDbType = oracleDbType;
        return this;
    }

    /// <inheritdoc/>
    [MemberNotNull(nameof(OracleDbType))]
    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = OracleDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, OracleDbType>("Attempted to build Parameter with undefined database type. Either no database type was specified or the type could not be inferred from the type of the mapped property.");
    }

    /// <inheritdoc/>
    protected override IParameterCompiler<OracleCompiledParameter> Build() =>
        new OracleParameterCompiler(this);
}
