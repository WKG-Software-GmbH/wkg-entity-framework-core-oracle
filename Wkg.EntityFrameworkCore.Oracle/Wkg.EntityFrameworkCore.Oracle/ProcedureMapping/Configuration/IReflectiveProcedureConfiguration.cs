using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration;

public interface IReflectiveProcedureConfiguration<TProcedure, TIOContainer> : IProcedureConfiguration<TProcedure, TIOContainer>
    where TProcedure : OracleStoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>
    where TIOContainer : class
{
}