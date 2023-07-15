using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;

/// <summary>
/// Represents an <see cref="IResultBuilder"/> for result entities of a stored procedure in an Oracle database.
/// </summary>
public interface IOracleResultBuilder : IResultBuilder
{
}

/// <summary>
/// The result entity builder for result type <typeparamref name="TResult"/> of a stored procedure in an Oracle database.
/// </summary>
/// <typeparam name="TResult">The type of the result collection.</typeparam>
public class OracleResultBuilder<TResult> : ResultBuilder<TResult, OracleDataReader, OracleResultBuilder<TResult>>, IOracleResultBuilder
    where TResult : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OracleResultBuilder{TResult}"/> class.
    /// </summary>
    /// <param name="throwHelper">The <see cref="IProcedureThrowHelper"/> to be used if an error is encountered.</param>
    public OracleResultBuilder(IProcedureThrowHelper throwHelper) : base(throwHelper, typeof(TResult))
    {
    }

    /// <summary>
    /// Creates a new <see cref="OracleResultColumnBuilder{TResult, TProperty}"/> to map a result column of the procedure to a property of the result entity.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to be mapped.</typeparam>
    /// <param name="propertySelector">A lambda expression selecting the property to be mapped.</param>
    /// <returns>A new <see cref="OracleResultColumnBuilder{TResult, TProperty}"/> to configure the column.</returns>
    public OracleResultColumnBuilder<TResult, TProperty> Column<TProperty>(Expression<Func<TResult, TProperty>> propertySelector)
    {
        OracleResultColumnBuilder<TResult, TProperty> columnBuilder = new(propertySelector, ThrowHelper);
        ColumnBuilders.Add(columnBuilder);
        return columnBuilder;
    }

    /// <inheritdoc/>
    protected override IResultCompiler<OracleDataReader> Build() =>
        new OracleResultCompiler(this);
}