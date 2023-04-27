using Oracle.ManagedDataAccess.Client;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.Extensions.Common;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;

public class OracleResultColumnBuilderProxy<TResult, TProperty, TColumn> : TypedResultColumnBuilderProxy<TResult, TProperty, TColumn, OracleResultColumnBuilder<TResult, TProperty>, OracleResultColumnBuilderProxy<TResult, TProperty, TColumn>>, IOracleResultColumnBuilder
{
    public OracleResultColumnBuilderProxy(OracleResultColumnBuilder<TResult, TProperty> proxiedBuilder) : base(proxiedBuilder)
    {
    }

    OracleDbType? IOracleResultColumnBuilder.OracleDbType => ProxiedBuilder.To<IOracleResultColumnBuilder>().OracleDbType;

    protected override void AttemptAutoConfiguration() => throw new NotSupportedException();
}