using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;

/// <summary>
/// Represents an <see cref="IResultColumnBuilder"/> for a result column of a stored procedure in an Oracle database.
/// </summary>
public interface IOracleResultColumnBuilder : IResultColumnBuilder
{
    /// <summary>
    /// The <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> of the column, if configured.
    /// </summary>
    OracleDbType? OracleDbType { get; }
}

/// <summary>
/// The builder for a result column of stored procedure in an Oracle database.
/// </summary>
/// <typeparam name="TResult">The type of the result collection.</typeparam>
/// <typeparam name="TProperty">The type of the property to be mapped.</typeparam>
public class OracleResultColumnBuilder<TResult, TProperty>
    : ResultColumnBuilder<TResult, TProperty, OracleResultColumnBuilder<TResult, TProperty>>, IOracleResultColumnBuilder
{
    private static readonly OracleTypeMap s_typeMap = new();

    private OracleDbType? OracleDbType { get; set; } = null;

    OracleDbType? IOracleResultColumnBuilder.OracleDbType => OracleDbType;

    internal OracleResultColumnBuilder(Expression<Func<TResult, TProperty>> columnSelector, IResultThrowHelper throwHelper) : base(columnSelector, throwHelper)
    {
    }

    /// <summary>
    /// Sets the <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> of the column.
    /// </summary>
    /// <param name="dbType">The <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> to set.</param>
    /// <returns>The column builder for fluent configuration.</returns>
    public OracleResultColumnBuilder<TResult, TProperty> HasDbType(OracleDbType dbType)
    {
        if (OracleDbType is not null)
        {
            Context.ThrowHelper.Throw<InvalidOperationException>("Attempted to set DB Type on a column that already has a DB Type! This was unexpected at this time.");
        }
        OracleDbType = dbType;
        return this;
    }

    /// <inheritdoc/>
    public override OracleResultColumnBuilder<TResult, TProperty> RequiresConversion<TColumn>(Expression<Func<TColumn, TProperty>> conversion)
    {
        ParameterExpression columnExpression = conversion.Parameters[0];
        OracleDbType = s_typeMap.GetDbTypeOrDefault(columnExpression.Type);

        return base.RequiresConversion(conversion);
    }

    /// <inheritdoc/>
    protected override void AttemptAutoConfiguration() =>
        OracleDbType ??= s_typeMap.GetDbTypeOrDefault(Context.ResultProperty.PropertyType);

    /// <inheritdoc/>
    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = OracleDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, OracleDbType>("Attempted to build column with no valid DB Type!", nameof(OracleDbType));
    }

    internal void SetCompilerHint(OracleResultColumnCompilerHint hint) => 
        CompilerHint = hint;

    /// <inheritdoc/>
    protected override IResultColumnCompiler Build() =>
        new OracleResultColumnCompiler(this);
}