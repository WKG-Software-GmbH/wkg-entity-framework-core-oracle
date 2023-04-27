using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultConverters;
using Wkg.EntityFrameworkCore.Extensions;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;

internal class OracleResultColumnCompiler : ResultColumnCompiler<IOracleResultColumnBuilder, OracleDataReader>, IResultColumnCompiler
{
    public OracleResultColumnCompiler(IOracleResultColumnBuilder builder) : base(builder)
    {
    }

    protected override Expression? GetColumnConverterOrDefault()
    {
        if (Builder.OracleDbType is OracleDbType.Json or OracleDbType.ObjectAsJson or OracleDbType.ArrayAsJson
            && Builder.Conversion is null
            && Builder.Context.ResultProperty.PropertyType != typeof(string))
        {
            return JsonResultConverter.For(Builder.Context.ResultProperty.PropertyType);
        }
        return null;
    }

    protected override (Type, Expression) GetColumnFactory() => GetColumnFactoryExpression(Builder.OracleDbType!.Value);

    private static (Type, Expression) GetColumnFactoryExpression(OracleDbType dbType) => dbType switch
    {
        OracleDbType.BFile => ReadColumn((reader, name) => reader.GetOracleBFile(reader.GetOrdinal(name))),
        OracleDbType.Blob => ReadColumn((reader, name) => reader.GetOracleBlob(reader.GetOrdinal(name))),
        OracleDbType.Byte => ReadColumn((reader, name) => reader.GetByte(reader.GetOrdinal(name))),
        OracleDbType.Char
            or OracleDbType.NChar => ReadColumn((reader, name) => reader.GetChar(reader.GetOrdinal(name))),
        OracleDbType.Clob
            or OracleDbType.NClob => ReadColumn((reader, name) => reader.GetOracleClob(reader.GetOrdinal(name))),
        OracleDbType.Date => ReadColumn((reader, name) => reader.GetOracleDate(reader.GetOrdinal(name))),
        OracleDbType.Decimal => ReadColumn((reader, name) => reader.GetDecimal(reader.GetOrdinal(name))),
        OracleDbType.Double
            or OracleDbType.BinaryDouble => ReadColumn((reader, name) => reader.GetDouble(reader.GetOrdinal(name))),
        OracleDbType.Int16 => ReadColumn((reader, name) => reader.GetInt16(reader.GetOrdinal(name))),
        OracleDbType.Int32 => ReadColumn((reader, name) => reader.GetInt32(reader.GetOrdinal(name))),
        OracleDbType.Int64
            or OracleDbType.Long
            or OracleDbType.LongRaw => ReadColumn((reader, name) => reader.GetInt64(reader.GetOrdinal(name))),
        OracleDbType.IntervalDS => ReadColumn((reader, name) => reader.GetOracleIntervalDS(reader.GetOrdinal(name))),
        OracleDbType.IntervalYM => ReadColumn((reader, name) => reader.GetOracleIntervalYM(reader.GetOrdinal(name))),
        OracleDbType.NVarchar2 => ReadColumn((reader, name) => reader.GetOracleString(reader.GetOrdinal(name))),
        OracleDbType.Raw => ReadColumn((reader, name) => reader.GetBytes(reader.GetOrdinal(name))),
        OracleDbType.Single
            or OracleDbType.BinaryFloat => ReadColumn((reader, name) => reader.GetFloat(reader.GetOrdinal(name))),
        OracleDbType.TimeStamp => ReadColumn((reader, name) => reader.GetOracleTimeStamp(reader.GetOrdinal(name))),
        OracleDbType.TimeStampLTZ => ReadColumn((reader, name) => reader.GetOracleTimeStampLTZ(reader.GetOrdinal(name))),
        OracleDbType.TimeStampTZ => ReadColumn((reader, name) => reader.GetOracleTimeStampTZ(reader.GetOrdinal(name))),
        OracleDbType.Varchar2
            or OracleDbType.Json
            or OracleDbType.ArrayAsJson
            or OracleDbType.ObjectAsJson => ReadColumn((reader, name) => reader.GetString(reader.GetOrdinal(name))),
        OracleDbType.XmlType => ReadColumn((reader, name) => reader.GetOracleXmlType(reader.GetOrdinal(name))),
        OracleDbType.Object => ReadColumn((reader, name) => reader.GetValue(reader.GetOrdinal(name))),
        OracleDbType.Ref => ReadColumn((reader, name) => reader.GetOracleRef(reader.GetOrdinal(name))),
        OracleDbType.Boolean => ReadColumn((reader, name) => reader.GetBoolean(reader.GetOrdinal(name))),
        _ => throw new NotSupportedException($"The type {dbType} is not supported.")
    };
}
