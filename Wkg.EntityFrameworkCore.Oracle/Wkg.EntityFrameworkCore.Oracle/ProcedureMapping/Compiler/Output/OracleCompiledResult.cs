using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;

internal class OracleCompiledResult(bool isCollection, CompiledResultFactory<OracleDataReader> resultFactory) 
    : CompiledResult<OracleDataReader>(isCollection, resultFactory)
{
    public override object ReadFrom(DbDataReader reader) => 
        CompiledResultFactory.Invoke(Unsafe.As<OracleDataReader>(reader));
}
