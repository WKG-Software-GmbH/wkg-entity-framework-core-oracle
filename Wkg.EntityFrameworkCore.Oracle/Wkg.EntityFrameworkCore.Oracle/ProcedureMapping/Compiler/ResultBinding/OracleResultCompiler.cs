using Oracle.ManagedDataAccess.Client;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;

internal class OracleResultCompiler(IOracleResultBuilder builder) : ResultCompiler<IOracleResultBuilder>(builder), IResultCompiler<OracleDataReader>
{
    public CompiledResult<OracleDataReader> Compile(CompiledResultColumn[] compiledResultColumns)
    {
        CompiledResultFactory<OracleDataReader> factory = CompileResultFactoryFor<OracleDataReader>(compiledResultColumns);
        return new OracleCompiledResult(Builder.IsCollection, factory);
    }
}
