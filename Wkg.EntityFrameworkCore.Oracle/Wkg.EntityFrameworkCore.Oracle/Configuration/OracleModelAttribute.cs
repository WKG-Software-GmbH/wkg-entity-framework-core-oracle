﻿using Wkg.EntityFrameworkCore.Configuration.Reflection.Attributes;
using Wkg.EntityFrameworkCore.Configuration.Reflection.Discovery;

namespace Wkg.EntityFrameworkCore.Oracle.Configuration;

/// <summary>
/// Indicates that the decorated class should be loaded by the Oracle configuration loader, if multiple configuration loaders are used by the application.
/// </summary>
/// <remarks>
/// The attribute on its own does not do anything. It must be passed as the generic type parameter to <see cref="IDiscoveryOptionsBuilder.AddTargetDatabaseEngine{TTargetEngine}()"/> to be effective.
/// </remarks>
public sealed class OracleModelAttribute : DatabaseEngineModelAttribute;