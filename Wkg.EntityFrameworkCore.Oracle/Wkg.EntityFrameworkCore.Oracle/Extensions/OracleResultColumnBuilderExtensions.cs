using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Extensions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultConverters;
using Wkg.Extensions.Common;

namespace Wkg.EntityFrameworkCore.Oracle.Extensions;

public static class OracleResultColumnBuilderExtensions
{
    public static OracleResultColumnBuilderProxy<TResult, TProperty, bool> GetAsBoolean<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Boolean);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetBoolean(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, object> GetAsJson<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Json);
        LambdaExpression jsonConversion = JsonResultConverter.For(builder
            .To<IOracleResultColumnBuilder>().Context.ResultProperty.PropertyType);
        builder.SetCompilerHint(OracleResultColumnCompilerHint
            .Create((reader, name) => reader.GetString(name), jsonConversion));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, byte> GetAsByte<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Byte);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetByte(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, byte[]> GetAsBytes<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Blob);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetBytes(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, char> GetAsChar<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Char);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetChar(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, DateTime> GetAsDateTime<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Date);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetDateTime(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, DateTimeOffset> GetAsDateTimeOffset<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.TimeStampTZ);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetDateTimeOffset(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, decimal> GetAsDecimal<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Decimal);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetDecimal(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, double> GetAsDouble<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Double);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetDouble(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, float> GetAsFloat<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Single);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetFloat(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, Guid> GetAsGuid<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Raw);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetGuid(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, short> GetAsInt16<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Int16);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetInt16(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, int> GetAsInt32<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Int32);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetInt32(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, long> GetAsInt64<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Int64);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetInt64(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleBFile> GetAsOracleBFile<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Raw);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleBFile(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleBinary> GetAsOracleBinary<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
          builder.HasDbType(OracleDbType.Raw);
          builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleBinary(reader.GetOrdinal(name))));
          return new(builder);
     }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleBlob> GetAsOracleBlob<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Blob);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleBlob(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleClob> GetAsOracleClob<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Clob);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleClob(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleDate> GetAsOracleDate<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Date);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleDate(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleDecimal> GetAsOracleDecimal<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Decimal);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleDecimal(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleIntervalDS> GetAsOracleIntervalDS<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.IntervalDS);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleIntervalDS(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleIntervalYM> GetAsOracleIntervalYM<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.IntervalYM);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleIntervalYM(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleRef> GetAsOracleRef<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Blob);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleRef(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleString> GetAsOracleString<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Varchar2);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleString(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleTimeStamp> GetAsOracleTimeStamp<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.TimeStamp);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleTimeStamp(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleTimeStampLTZ> GetAsOracleTimeStampLTZ<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.TimeStampLTZ);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleTimeStampLTZ(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleTimeStampTZ> GetAsOracleTimeStampTZ<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.TimeStampTZ);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleTimeStampTZ(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, OracleXmlType> GetAsOracleXmlType<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.XmlType);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetOracleXmlType(reader.GetOrdinal(name))));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, string> GetAsString<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.Varchar2);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetString(name)));
        return new(builder);
    }

    public static OracleResultColumnBuilderProxy<TResult, TProperty, TimeSpan> GetAsTimeSpan<TResult, TProperty>(this OracleResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(OracleDbType.IntervalDS);
        builder.SetCompilerHint(OracleResultColumnCompilerHint.Create((reader, name) => reader.GetTimeSpan(reader.GetOrdinal(name))));
        return new(builder);
    }
}
