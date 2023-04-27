using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration;

public interface IProcedureConfiguration<TProcedure, TIOContainer>
    where TProcedure : OracleStoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>
    where TIOContainer : class
{
    static abstract void Configure(OracleProcedureBuilder<TProcedure, TIOContainer> self);
}