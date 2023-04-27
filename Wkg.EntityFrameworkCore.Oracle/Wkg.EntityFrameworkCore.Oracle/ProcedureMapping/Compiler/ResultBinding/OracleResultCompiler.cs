using Oracle.ManagedDataAccess.Client;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;

internal class OracleResultCompiler : ResultCompiler<IOracleResultBuilder>, IResultCompiler<OracleDataReader>
{
    public OracleResultCompiler(IOracleResultBuilder builder) : base(builder)
    {
    }

    public CompiledResult<OracleDataReader> Compile(CompiledResultColumn[] compiledResultColumns)
    {
        CompiledResultFactory<OracleDataReader> factory = CompileResultFactoryFor<OracleDataReader>(compiledResultColumns);
        return new OracleCompiledResult(Builder.IsCollection, factory);
    }
}
