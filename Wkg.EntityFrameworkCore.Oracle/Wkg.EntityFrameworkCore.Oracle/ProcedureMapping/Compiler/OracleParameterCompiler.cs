using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Compiler;

internal class OracleParameterCompiler : ParameterCompiler<IOracleParameterBuilder>, IParameterCompiler<OracleCompiledParameter>
{
    public OracleParameterCompiler(IOracleParameterBuilder parameterBuilder) : base(parameterBuilder)
    {
    }

    public OracleCompiledParameter Compile()
    {
        IOracleParameterBuilder b = Builder;
        return new OracleCompiledParameter(b.ParameterName!, b.OracleDbType!.Value, b.ParameterDirection, b.IsOutput, CreateGetter(), CreateSetter(), b.Size);
    }
}
