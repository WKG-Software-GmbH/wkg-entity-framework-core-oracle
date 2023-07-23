using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Oracle.Extensions;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration;

/// <summary>
/// Represents the configuration of a stored procedure that will be reflectively configured by the <see cref="ModelBuilderExtensions.LoadReflectiveProcedures(ModelBuilder)"/> method.
/// </summary>
/// <typeparam name="TProcedure">The type of the stored procedure command object to load and configure.</typeparam>
/// <typeparam name="TIOContainer">The type of the input/output container used by <typeparamref name="TProcedure"/>.</typeparam>
public interface IReflectiveProcedureConfiguration<TProcedure, TIOContainer> : IProcedureConfiguration<TProcedure, TIOContainer>
    where TProcedure : OracleStoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>
    where TIOContainer : class
{
}