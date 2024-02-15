using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;

internal class OracleProcedureCompiler(IOracleProcedureBuilder builder, Type procedureType) 
    : ProcedureCompiler<IOracleProcedureBuilder>(builder, procedureType), IProcedureCompiler<OracleCompiledParameter>
{
    public ICompiledProcedure Compile(OracleCompiledParameter[] compiledParameters, CompiledResult? compiledResult)
    {
        IOracleProcedureBuilder b = Builder;
        string fullProcedureName = string.IsNullOrEmpty(b.PackageName) ? b.ProcedureName! : $"{b.PackageName}.{b.ProcedureName}";
        return new CompiledProcedure<OracleCompiledParameter>(fullProcedureName, b.IsFunction, compiledParameters, ProcedureType, compiledResult);
    }
}