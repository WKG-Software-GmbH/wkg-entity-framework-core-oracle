using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.ResultBinding;

internal record OracleResultColumnCompilerHint(Expression ReaderGetExpression, Type ReaderResultType, Expression? AutoConversion) : IResultColumnCompilerHint
{
    public static OracleResultColumnCompilerHint Create<TResult>(Expression<Func<OracleDataReader, string, TResult>> readerExpression) =>
        new(readerExpression, typeof(TResult), null);

    public static OracleResultColumnCompilerHint Create<TResult>(Expression<Func<OracleDataReader, string, TResult>> readerExpression, Expression conversion) =>
        new(readerExpression, typeof(TResult), conversion);
}