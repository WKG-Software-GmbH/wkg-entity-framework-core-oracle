using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data;
using System.Runtime.CompilerServices;
using Wkg.Extensions.Common;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Load(ref DbParameter? parameter, object context) =>
        parameter = new OracleParameter(Name, OracleDbType, Getter.Invoke(context), Direction)
        {
            Size = Size
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Store(ref DbParameter param, object context)
    {
        Setter!.Invoke(context, param.Value!);
        param.To<OracleParameter>().Dispose();
    }
}