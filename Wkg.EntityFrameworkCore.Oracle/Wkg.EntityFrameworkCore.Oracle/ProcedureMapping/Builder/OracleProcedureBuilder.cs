using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.ProcedureMapping.Runtime;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;

internal interface IOracleProcedureBuilder : IProcedureBuilder
{
    string? PackageName { get; }
}

public class OracleProcedureBuilder<TProcedure, TIOContainer>
    : ProcedureBuilder<
        TProcedure,
        TIOContainer,
        OracleCompiledParameter,
        OracleDataReader,
        OracleProcedureBuilder<TProcedure, TIOContainer>>,
    IOracleProcedureBuilder
    where TProcedure : OracleStoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>
    where TIOContainer : class
{
    protected string? _packageName;

    string? IOracleProcedureBuilder.PackageName => _packageName;

    public OracleProcedureBuilder<TProcedure, TIOContainer> InPackage(string packageName)
    {
        _packageName = packageName;
        return this;
    }

    public OracleParameterBuilder<TIOContainer, TParameter> Parameter<TParameter>(Expression<Func<TIOContainer, TParameter>> parameterExpression)
    {
        OracleParameterBuilder<TIOContainer, TParameter> paramBuilder = new(parameterExpression, ThrowHelper);
        ParameterBuilders.Add(paramBuilder);
        return paramBuilder;
    }

    protected override IProcedureCompiler<OracleCompiledParameter> Build() => 
        new OracleProcedureCompiler(this, typeof(TProcedure));
}
