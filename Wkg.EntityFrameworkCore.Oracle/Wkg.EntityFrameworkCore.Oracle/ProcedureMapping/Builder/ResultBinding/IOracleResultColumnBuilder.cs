using Oracle.ManagedDataAccess.Client;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;

/// <summary>
/// Represents an <see cref="IResultColumnBuilder"/> for a result column of a stored procedure in an Oracle database.
/// </summary>
public interface IOracleResultColumnBuilder : IResultColumnBuilder
{
    /// <summary>
    /// The <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> of the column, if configured.
    /// </summary>
    OracleDbType? OracleDbType { get; }
}
