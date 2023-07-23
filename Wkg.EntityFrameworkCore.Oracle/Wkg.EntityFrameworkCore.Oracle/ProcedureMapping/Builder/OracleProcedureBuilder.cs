using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;

/// <summary>
/// Provides a simple API for configuring a stored procedure in an Oracle database.
/// </summary>
internal interface IOracleProcedureBuilder : IProcedureBuilder
{
    string? PackageName { get; }
}

/// <summary>
/// Provides a simple API for configuring a stored procedure in an Oracle database.
/// </summary>
/// <typeparam name="TProcedure">The concrete type of the stored procedure command object that represents the stored database procedure.</typeparam>
/// <typeparam name="TIOContainer">The type of the Input/Output container object used to pass arguments to and from the stored procedure.</typeparam>
public class OracleProcedureBuilder<TProcedure, TIOContainer>
    : ProcedureBuilder<
        TProcedure,
        TIOContainer,
        OracleCompiledParameter,
        OracleDataReader,
        OracleProcedureBuilder<TProcedure, TIOContainer>>,
    IOracleProcedureBuilder
    where TProcedure : StoredProcedure<TIOContainer>, IOracleStoredProcedure<TIOContainer>
    where TIOContainer : class
{
    private string? _packageName;

    internal OracleProcedureBuilder()
    {
    }

    string? IOracleProcedureBuilder.PackageName => _packageName;

    /// <summary>
    /// Specifies the package name of the stored procedure.
    /// </summary>
    /// <param name="packageName">The package name of the stored procedure.</param>
    /// <returns>The current <see cref="OracleProcedureBuilder{TProcedure, TIOContainer}"/> instance for fluent configuration.</returns>
    public OracleProcedureBuilder<TProcedure, TIOContainer> InPackage(string packageName)
    {
        _packageName = packageName;
        return this;
    }

    /// <summary>
    /// Creates a new <see cref="OracleParameterBuilder{TIOContainer, TParameter}"/> instance for configuring a parameter of this Oracle procedure.
    /// </summary>
    /// <typeparam name="TParameter">The CLR type of the parameter being mapped.</typeparam>
    /// <param name="parameterExpression">An lambda expression that identifies the property of the <typeparamref name="TIOContainer"/> that should be mapped to this parameter.</param>
    /// <returns>A new <see cref="OracleParameterBuilder{TIOContainer, TParameter}"/> instance to configure the parameter.</returns>
    public OracleParameterBuilder<TIOContainer, TParameter> Parameter<TParameter>(Expression<Func<TIOContainer, TParameter>> parameterExpression)
    {
        OracleParameterBuilder<TIOContainer, TParameter> paramBuilder = new(parameterExpression, ThrowHelper);
        ParameterBuilders.Add(paramBuilder);
        return paramBuilder;
    }
    /// <summary>
    /// Creates a new <see cref="OracleResultBuilder{TResult}"/> instance for configuring the result set of this Oracle procedure.
    /// </summary>
    /// <typeparam name="TResult">The CLR type of the result entities within the result set.</typeparam>
    /// <returns>A new <see cref="OracleResultBuilder{TResult}"/> instance to configure the result set.</returns>
    public OracleResultBuilder<TResult> Returns<TResult>() where TResult : class
    {
        OracleResultBuilder<TResult> resultBuilder = new(ThrowHelper);
        ResultBuilder = resultBuilder;
        return resultBuilder;
    }

    /// <inheritdoc/>
    protected override IProcedureCompiler<OracleCompiledParameter> Build() => 
        new OracleProcedureCompiler(this, typeof(TProcedure));
}
