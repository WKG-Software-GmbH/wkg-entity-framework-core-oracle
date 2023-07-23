using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;

internal class OracleProcedureCompiler : ProcedureCompiler<IOracleProcedureBuilder>, IProcedureCompiler<OracleCompiledParameter>
{
    public OracleProcedureCompiler(IOracleProcedureBuilder builder, Type procedureType) : base(builder, procedureType)
    {
    }

    public ICompiledProcedure Compile(OracleCompiledParameter[] compiledParameters, CompiledResult? compiledResult)
    {
        IOracleProcedureBuilder b = Builder;
        string fullProcedureName = string.IsNullOrEmpty(b.PackageName) ? b.ProcedureName! : $"{b.PackageName}.{b.ProcedureName}";
        return new CompiledProcedure<OracleCompiledParameter>(fullProcedureName, b.IsFunction, compiledParameters, ProcedureType, compiledResult);
    }
}