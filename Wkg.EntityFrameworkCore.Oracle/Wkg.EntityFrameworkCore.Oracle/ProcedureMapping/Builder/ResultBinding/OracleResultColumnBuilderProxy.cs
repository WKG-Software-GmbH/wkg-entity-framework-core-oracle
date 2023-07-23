﻿using Oracle.ManagedDataAccess.Client;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.Extensions.Common;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;

/// <summary>
/// Represents a type safe proxy builder for a corresponding <see cref="OracleResultColumnBuilder{TResult, TProperty}"/>.
/// </summary>
/// <remarks>
/// This class ensures that the type read from the database is compatible with the conversion expression and the property type.
/// </remarks>
/// <typeparam name="TResult">The type of the result entity owning the column being configured.</typeparam>
/// <typeparam name="TProperty">The type of the property being mapped.</typeparam>
/// <typeparam name="TColumn">The CLR type of the column being mapped.</typeparam>
public class OracleResultColumnBuilderProxy<TResult, TProperty, TColumn> : TypedResultColumnBuilderProxy<TResult, TProperty, TColumn, OracleResultColumnBuilder<TResult, TProperty>, OracleResultColumnBuilderProxy<TResult, TProperty, TColumn>>, IOracleResultColumnBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OracleResultColumnBuilderProxy{TResult, TProperty, TColumn}"/> class.
    /// </summary>
    /// <param name="proxiedBuilder">The proxied builder.</param>
    public OracleResultColumnBuilderProxy(OracleResultColumnBuilder<TResult, TProperty> proxiedBuilder) : base(proxiedBuilder)
    {
    }

    OracleDbType? IOracleResultColumnBuilder.OracleDbType => ProxiedBuilder.To<IOracleResultColumnBuilder>().OracleDbType;

    /// <inheritdoc/>
    /// <exception cref="NotSupportedException">This method is not supported.</exception>
    protected override void AttemptAutoConfiguration() => throw new NotSupportedException();
}