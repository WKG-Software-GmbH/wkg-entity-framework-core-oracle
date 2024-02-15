using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping;

/// <summary>
/// Represents a stored procedure command object in an Oracle database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the input/output container used to pass parameters to the stored procedure and to retrieve the result of <see langword="out"/> or <see langword="ref"/> parameters.</typeparam>
public interface IOracleStoredProcedure<TIOContainer>
    where TIOContainer : class;

/// <summary>
/// A stored procedure command object in an Oracle database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the input/output container used to pass parameters to the stored procedure and to retrieve the result of <see langword="out"/> or <see langword="ref"/> parameters.</typeparam>
public abstract class OracleStoredProcedure<TIOContainer> : StoredProcedure<TIOContainer>, IOracleStoredProcedure<TIOContainer>
    where TIOContainer : class;

/// <summary>
/// A stored procedure command object with a result set of type <typeparamref name="TResult"/> in an Oracle database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the input/output container used to pass parameters to the stored procedure and to retrieve the result of <see langword="out"/> or <see langword="ref"/> parameters.</typeparam>
/// <typeparam name="TResult">The type of the result set.</typeparam>
public abstract class OracleStoredProcedure<TIOContainer, TResult> : StoredProcedure<TIOContainer, TResult>, IOracleStoredProcedure<TIOContainer>
    where TIOContainer : class
    where TResult : class;