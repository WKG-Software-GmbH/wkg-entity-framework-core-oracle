using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data;
using System.Runtime.CompilerServices;
using Wkg.Extensions.Common;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;

/// <summary>
/// Represents a parameter that has been compiled for runtime use.
/// </summary>
/// <param name="Name">The name of the parameter.</param>
/// <param name="OracleDbType">The <see cref="global::Oracle.ManagedDataAccess.Client.OracleDbType"/> of the parameter.</param>
/// <param name="Direction">The <see cref="ParameterDirection"/> of the parameter.</param>
/// <param name="IsOutput">Whether the parameter requires output binding.</param>
/// <param name="Getter">The <see cref="PropertyGetter"/> that can be used to retrieve the value of the parameter from the input/output container object.</param>
/// <param name="Setter">The <see cref="PropertySetter"/> that can be used to store the value of the parameter to the input/output container object.</param>
/// <param name="Size">The size of the parameter.</param>
public readonly record struct OracleCompiledParameter
(
    string Name,
    OracleDbType OracleDbType,
    ParameterDirection Direction,
    bool IsOutput,
    PropertyGetter Getter,
    PropertySetter? Setter,
    int Size
) : ICompiledParameter
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Load(ref DbParameter? parameter, object context) =>
        parameter = new OracleParameter(Name, OracleDbType, Getter.Invoke(context), Direction)
        {
            Size = Size
        };

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Store(ref DbParameter param, object context)
    {
        Setter!.Invoke(context, param.Value!);
        param.To<OracleParameter>().Dispose();
    }
}