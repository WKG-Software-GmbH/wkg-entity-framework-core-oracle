using Oracle.ManagedDataAccess.Client;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping;

internal class OracleProcedureBuildPipeline : ProcedureBuildPipeline
{
    public static void Execute(IProcedureBuilder<OracleCompiledParameter, OracleDataReader> procedureBuilder) => 
        ProcedureBuildPipeline.Execute(procedureBuilder);
}
