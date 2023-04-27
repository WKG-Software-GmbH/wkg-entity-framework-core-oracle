using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;

public interface IOracleResultColumnBuilder : IResultColumnBuilder
{
    OracleDbType? OracleDbType { get; }
}

public class OracleResultColumnBuilder<TResult, TProperty>
    : ResultColumnBuilder<TResult, TProperty, OracleResultColumnBuilder<TResult, TProperty>>, IOracleResultColumnBuilder
{
    private static readonly OracleTypeMap _typeMap = new();

    private OracleDbType? OracleDbType { get; set; } = null;

    OracleDbType? IOracleResultColumnBuilder.OracleDbType => OracleDbType;

    internal OracleResultColumnBuilder(Expression<Func<TResult, TProperty>> columnSelector, IResultThrowHelper throwHelper) : base(columnSelector, throwHelper)
    {
    }

    public OracleResultColumnBuilder<TResult, TProperty> HasDbType(OracleDbType dbType)
    {
        if (OracleDbType is not null)
        {
            Context.ThrowHelper.Throw<InvalidOperationException>("Attempted to set DB Type on a column that already has a DB Type! This was unexpected at this time.");
        }
        OracleDbType = dbType;
        return this;
    }

    public override OracleResultColumnBuilder<TResult, TProperty> RequiresConversion<TColumn>(Expression<Func<TColumn, TProperty>> conversion)
    {
        ParameterExpression columnExpression = conversion.Parameters[0];
        OracleDbType = _typeMap.GetDbTypeOrDefault(columnExpression.Type);

        return base.RequiresConversion(conversion);
    }

    protected override void AttemptAutoConfiguration() =>
        OracleDbType ??= _typeMap.GetDbTypeOrDefault(Context.ResultProperty.PropertyType);

    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = OracleDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, OracleDbType>("Attempted to build column with no valid DB Type!", nameof(OracleDbType));
    }

    internal void SetCompilerHint(OracleResultColumnCompilerHint hint) => 
        CompilerHint = hint;

    protected override IResultColumnCompiler Build() =>
        new OracleResultColumnCompiler(this);
}