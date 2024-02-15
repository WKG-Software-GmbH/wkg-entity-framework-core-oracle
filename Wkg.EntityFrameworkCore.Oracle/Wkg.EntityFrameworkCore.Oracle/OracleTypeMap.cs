using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections.Frozen;

namespace Wkg.EntityFrameworkCore.Oracle;

internal class OracleTypeMap : DbTypeMap<OracleDbType>
{
    protected override FrozenDictionary<Type, OracleDbType> TypeMap { get; } = FrozenDictionary.ToFrozenDictionary(new Dictionary<Type, OracleDbType>
    {
        { typeof(int), OracleDbType.Int32 },
        { typeof(long), OracleDbType.Int64 },
        { typeof(string), OracleDbType.Varchar2 },
        { typeof(bool), OracleDbType.Boolean },
        { typeof(DateTime), OracleDbType.Date },
        { typeof(DateTimeOffset), OracleDbType.Date },
        { typeof(TimeOnly), OracleDbType.TimeStamp },
        { typeof(DateOnly), OracleDbType.Date },
        { typeof(TimeSpan), OracleDbType.TimeStamp },
        { typeof(float), OracleDbType.Single },
        { typeof(double), OracleDbType.Double },
        { typeof(decimal), OracleDbType.Decimal },
        { typeof(byte[]), OracleDbType.Blob },
        { typeof(Guid), OracleDbType.Raw },
        { typeof(uint), OracleDbType.Int32 },
        { typeof(ulong), OracleDbType.Int64 },
        { typeof(short), OracleDbType.Int16 },
        { typeof(ushort), OracleDbType.Int16 },
        { typeof(sbyte), OracleDbType.Byte },
        { typeof(byte), OracleDbType.Byte },
        { typeof(char), OracleDbType.Char },
        { typeof(char[]), OracleDbType.Varchar2 },
        { typeof(object), OracleDbType.Object },
        { typeof(OracleBinary), OracleDbType.Blob },
        { typeof(OracleBoolean), OracleDbType.Boolean },
        { typeof(OracleClob), OracleDbType.Clob },
        { typeof(OracleDate), OracleDbType.Date },
        { typeof(OracleDecimal), OracleDbType.Decimal },
        { typeof(OracleIntervalDS), OracleDbType.IntervalDS },
        { typeof(OracleIntervalYM), OracleDbType.IntervalYM },
        { typeof(OracleRefCursor), OracleDbType.RefCursor },
        { typeof(OracleString), OracleDbType.Varchar2 },
        { typeof(OracleTimeStamp), OracleDbType.TimeStamp },
        { typeof(OracleTimeStampLTZ), OracleDbType.TimeStampLTZ },
        { typeof(OracleTimeStampTZ), OracleDbType.TimeStampTZ },
        { typeof(OracleXmlType), OracleDbType.XmlType },
        { typeof(OracleBFile), OracleDbType.BFile },
        { typeof(OracleBlob), OracleDbType.Blob },
    });
}
