using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;

public interface IOracleResultBuilder : IResultBuilder
{
}

public class OracleResultBuilder<TResult> : ResultBuilder<TResult, OracleDataReader, OracleResultBuilder<TResult>>, IOracleResultBuilder
    where TResult : class
{
    public OracleResultBuilder(IProcedureThrowHelper throwHelper) : base(throwHelper, typeof(TResult))
    {
    }

    public OracleResultColumnBuilder<TResult, TProperty> Column<TProperty>(Expression<Func<TResult, TProperty>> propertySelector)
    {
        OracleResultColumnBuilder<TResult, TProperty> columnBuilder = new(propertySelector, ThrowHelper);
        ColumnBuilders.Add(columnBuilder);
        return columnBuilder;
    }

    protected override IResultCompiler<OracleDataReader> Build() =>
        new OracleResultCompiler(this);
}