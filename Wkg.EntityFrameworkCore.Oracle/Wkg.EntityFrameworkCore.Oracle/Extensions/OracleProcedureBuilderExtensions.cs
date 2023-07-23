using System.Data;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.Oracle.Extensions;

/// <summary>
/// Provides extension methods for configuring a MySQL stored procedure.
/// </summary>
public static class OracleProcedureBuilderExtensions
{
    /// <summary>
    /// Creates a new <see cref="OracleParameterBuilder{TIOContainer, TParameter}"/> instance with <see cref="ParameterDirection.ReturnValue"/> for configuring a scalar return value of this MySQL procedure.
    /// </summary>
    /// <typeparam name="TProcedure">The type of the stored procedure being configured.</typeparam>
    /// <typeparam name="TIOContainer">The type of the Input/Output container object used to pass arguments to and from the stored procedure.</typeparam>
    /// <typeparam name="TParameter">The CLR type of the parameter being mapped.</typeparam>
    /// <param name="builder">The procedure builder.</param>
    /// <param name="parameterExpression">An lambda expression that identifies the property of the <typeparamref name="TIOContainer"/> that should be mapped to this parameter.</param>
    /// <returns>A new <see cref="OracleParameterBuilder{TIOContainer, TParameter}"/> instance to configure the scalar return value parameter.</returns>
    public static OracleParameterBuilder<TIOContainer, TParameter> ReturnsScalar<TProcedure, TIOContainer, TParameter>(this OracleProcedureBuilder<TProcedure, TIOContainer> builder, Expression<Func<TIOContainer, TParameter>> parameterExpression)
        where TProcedure : StoredProcedure<TIOContainer>, IOracleStoredProcedure<TIOContainer>
        where TIOContainer : class
    {
        OracleParameterBuilder<TIOContainer, TParameter> paramBuilder = builder.Parameter(parameterExpression);
        paramBuilder.HasDirection(ParameterDirection.ReturnValue);
        return paramBuilder;
    }
}
