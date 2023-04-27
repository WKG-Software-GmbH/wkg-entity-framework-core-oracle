using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping;

public abstract class OracleStoredProcedure<TIOContainer> : StoredProcedure<TIOContainer>
    where TIOContainer : class
{
}