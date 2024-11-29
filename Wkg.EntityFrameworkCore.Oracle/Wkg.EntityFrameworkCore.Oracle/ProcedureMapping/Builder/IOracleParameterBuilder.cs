using Oracle.ManagedDataAccess.Client;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;

/// <summary>
/// Provides a simple API for configuring a parameter of a stored procedure in an Oracle database.
/// </summary>
internal interface IOracleParameterBuilder : IParameterBuilder
{
    /// <summary>
    /// The <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> associated with the parameter of this Oracle procedure.
    /// </summary>
    OracleDbType? OracleDbType { get; }
}
