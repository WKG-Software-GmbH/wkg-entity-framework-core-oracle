using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;

internal class OracleCompiledResult : CompiledResult<OracleDataReader>
{
    public OracleCompiledResult(bool isCollection, CompiledResultFactory<OracleDataReader> resultFactory) : base(isCollection, resultFactory)
    {
    }

    public override object ReadFrom(DbDataReader reader) => 
        CompiledResultFactory.Invoke(Unsafe.As<OracleDataReader>(reader));
}
