using Oracle.ManagedDataAccess.Client;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;

internal interface IOracleParameterBuilder : IParameterBuilder
{
    OracleDbType? OracleDbType { get; }
}

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

    public OracleParameterBuilder(Expression<Func<TIOContainer, TParameter>> parameterSelector, IProcedureThrowHelper throwHelper) : base(parameterSelector, throwHelper)
    {
        OracleDbType = _typeMap.GetDbTypeOrDefault(Context.PropertyInfo.PropertyType);
    }

    public OracleParameterBuilder<TIOContainer, TParameter> HasDbType(OracleDbType oracleDbType)
    {
        OracleDbType = oracleDbType;
        return this;
    }

    [MemberNotNull(nameof(OracleDbType))]
    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = OracleDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, OracleDbType>("Attempted to build Parameter with undefined database type. Either no database type was specified or the type could not be inferred from the parameter type.");
    }

    protected override IParameterCompiler<OracleCompiledParameter> Build() =>
        new OracleParameterCompiler(this);
}
