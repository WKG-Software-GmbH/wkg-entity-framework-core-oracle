﻿using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Configuration.Reflection;
using Wkg.EntityFrameworkCore.Oracle.Extensions;
using Wkg.EntityFrameworkCore.ProcedureMapping;
using Wkg.Logging;

namespace Wkg.EntityFrameworkCore.Oracle.ProcedureMapping.Configuration.Reflection;

internal class ReflectiveProcedureConfigurationLoader : ReflectiveLoaderBase
{
    private static object? _reflectiveProcedureLoaderSentinel = new();

    public static ModelBuilder LoadAll(ModelBuilder builder)
    {
        AssertLoadOnce(builder, ref _reflectiveProcedureLoaderSentinel);

        Log.WriteInfo($"{nameof(ReflectiveProcedureConfigurationLoader)} is initializing.");

        LoadAllProceduresInternal(typeof(IStoredProcedure),
            typeof(OracleStoredProcedure<>),
            typeof(IReflectiveProcedureConfiguration<,>),
            typeof(ModelBuilderExtensions),
            nameof(ModelBuilderExtensions.LoadProcedure),
            builder);

        Log.WriteInfo($"{nameof(ReflectiveProcedureConfigurationLoader)} is exiting.");
        return builder;
    }
}