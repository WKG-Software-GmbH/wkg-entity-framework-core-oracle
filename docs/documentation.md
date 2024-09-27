# `Wkg.EntityFrameworkCore.Oracle` Documentation

`Wkg.EntityFrameworkCore.Oracle` only provides the RECAP bindings for Oracle PL/SQL. For more information on how to use the RECAP framework, please refer to the [WKG Entity Framework Core documentation](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core/blob/main/docs/documentation.md).

- [`Wkg.EntityFrameworkCore.Oracle` Documentation](#wkgentityframeworkcoreoracle-documentation)
  - [Getting Started](#getting-started)
    - [Requirements](#requirements)
    - [Installation](#installation)
  - [Usage](#usage)
    - [Mapping Procedure Command Objects](#mapping-procedure-command-objects)
    - [Features](#features)

> :warning: **Warning**
> This documentation is a work in progress and may not be complete or up-to-date. For the most accurate and up-to-date information, please refer to the source code and the XML documentation comments.

## Getting Started

### Requirements

In order to use the `Wkg.EntityFrameworkCore.Oracle` package, you must have the following installed:

- A .NET runtime matching a major version of the `Wkg.EntityFrameworkCore.Oracle` package.
- The `Wkg.EntityFrameworkCore` package containing the core components of the RECAP framework. The major and minor versions of the `Wkg.EntityFrameworkCore` and `Wkg.EntityFrameworkCore.Oracle` packages must match.

### Installation

Please refer to the installation instructions provided in the [README](../README.md).

## Usage

> :bulb: **Tip**
> It is recommended to read the [WKG Entity Framework Core documentation](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core/blob/main/docs/documentation.md) before using the `Wkg.EntityFrameworkCore.Oracle` package.

### Mapping Procedure Command Objects

Mapping RECAP Procedure Command Objects (PCOs) targeting Oracle follows the same pattern described in the [WKG Entity Framework Core documentation](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core/blob/main/docs/documentation.md#getting-started-with-pco-mapping), however, there are some minor differences to be aware of:

- All PCOs targeting Oracle Databases must inherit from the `OracleStoredProcedure<TIOContainer>` or `OracleStoredProcedure<TIOContainer, TResult>` base classes, where `TIOContainer` is the type of the Input/Output Container (I/O Container) used by the PCO.
- When configuring a PCO, use the `InPackage(string packageName)` method to specify the name of the package containing the procedure. This is required for all PCOs targeting Oracle Databases. The following example demonstrates the usage of the `InPackage()` method:

    ```csharp
    public class MyProcedure : OracleStoredProcedure<MyIOContainer>
    {
        protected override void Configure(IReflectiveModelConfiguration<MyIOContainer> self)
        {
            self.ToDatabaseProcedure("my_procedure")
                .InPackage("my_package");
        }
    }
    ```
    
    In above example, the `MyProcedure` PCO will be mapped to a procedure named `my_procedure` in the `my_package` package.

    > :bulb: **Tip**
    > Alternatively, the fully qualified name of the procedure can be specified using `ToDatabaseProcedure("my_package.my_procedure")`.

### Features

The `Wkg.EntityFrameworkCore.Oracle` package provides the following additional features:

- **JSON support** - When mapping JSON columns of a result set, RECAP can automatically deserialize the JSON data into a .NET object for you. The following example demonstrates the usage of the `GetAsJson()` extension method in `OracleResultColumnBuilderExtensions`:

    ```csharp
    public record MyJsonModel(int Id, string Name);
    public record MyResult(MyJsonModel? JsonModel, string SomeOtherProperty);
    ...
    resultBuilder.Column(result => result.JsonModel)
        .HasName("my_json_column")
        .MayBeNull()    // enables null value awareness for the column
        .GetAsJson();   // tells RECAP to deserialize the JSON data into the CLR type of the property
    ```
    
    In above example, the `JsonModel` property will be mapped to a JSON column named `my_json_column` of Oracle type `JSON`. If the JSON column contains a `null` value, the `JsonModel` property will be set to `null`. If the JSON column contains a JSON object, the JSON object will be deserialized into a `MyJsonModel` object and assigned to the `JsonModel` property.

    > :information_source: **Note**
    > RECAP uses `System.Text.Json` to deserialize JSON data. Therefore, you may use the `System.Text.Json.Serialization` attributes to control the deserialization process. For more information, please refer to the [System.Text.Json documentation](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization?view=net-7.0).

    You can also opt to implicitly deserialize the JSON data by mapping a column of type `OracleDbType.Json`, `OracleDbType.ObjectAsJson`, or `OracleDbType.ArrayAsJson` to matching CLR types. The following example demonstrates this:

    ```csharp
    ...
    resultBuilder.Column(result => result.JsonModel)
        .HasName("my_json_column")
        .HasDbType(OracleDbType.Json);   // implicitly instructs RECAP to deserialize the JSON data into the CLR type of the property
    ```

    > :pray: **Feature Request**
    > JSON support is also provided by the [WKG Entity Framework Core - MySQL Bindings](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core-mysql) package, so it should be investigated whether a common implementation can be extracted to [WKG Entity Framework Core](https://github.com/WKG-Software-GmbH/wkg-entity-framework-core). Feel free to open merge requests in the Core repository to implement this feature. :slightly_smiling_face:
    
- **Support for Oracle datatypes** - The `OracleResultColumnBuilderExtensions` class provides exhaustive support for Oracle datatypes including `OracleTimeStamp`, `OracleClob`, and other types. PCOs can map these datatypes to parameters as usual via the `HasDbType()` method.
- **Conditional Entity Discovery** - If you are using multiple database providers in the same project, you can decorate `IReflectiveModelConfiguration<>` implementations with the `OracleModelAttribute` to restrict entity discovery to Oracle only. Subsequently pass the attribute type to the `LoadReflectiveModels<T>()` method:
  
    ```csharp
    public class OracleDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.LoadReflectiveModels<OracleModel>();
        }
    }
    ```
    
    In above example, only entities decorated with the `OracleModelAttribute` will be discovered by RECAP. All other entities will be ignored.
- **Automatic type inference** - RECAP's Oracle provider also supports automatic database type inference if `HasDbType()` is omitted when mapping PCO parameters. In these cases RECAP tries to determine the corresponding database type based on the CLR type of the I/O Container property being mapped. For a full list of supported types, please refer to the [`OracleTypeMap` class](../Wkg.EntityFrameworkCore.Oracle/Wkg.EntityFrameworkCore.Oracle/OracleTypeMap.cs).