using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;

/// <summary>
/// Provides a simple API for configuring a stored procedure in an Oracle database.
/// </summary>
internal interface IOracleProcedureBuilder : IProcedureBuilder
{
    string? PackageName { get; }
}
