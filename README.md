# IOptionsDemo

A demonstration project for using the `IOptions<T>`, `IOptionsSnapshot<T>`, and `IOptionsMonitor<T>` patterns in ASP.NET Core (.NET 9). This project shows how to configure, inject, and test options in a modern web application.

---

## Table of Contents

- [Project Structure](#project-structure)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Build and Run](#build-and-run)
- [Options Pattern Overview](#options-pattern-overview)
  - [IOptions<T>](#ioptionst)
  - [IOptionsSnapshot<T>](#ioptionssnapshott)
  - [IOptionsMonitor<T>](#ioptionsmonitort)
- [Configuration](#configuration)
- [Testing](#testing)
- [Extending the Demo](#extending-the-demo)
- [References](#references)

---

## Project Structure

IOptionsDemo/
|
├── IOptionsDemo/ # Main ASP.NET Core project
|   ├── Configuration/ # Options classes (e.g., FeatureOptions)
|   ├── Controllers/ # API controllers
│   │   └── IOptionsDemoController.cs
|   ├── Services/ # Services using options patterns
│   │   ├── StaticOptionsService.cs         # IOptions<T>
│   │   ├── SnapshotOptionsService.cs       # IOptionsSnapshot<T>
│   │   └── MonitorOptionsService.cs        # IOptionsMonitor<T>
│   ├── appsettings.json
│   ├── Program.cs # App entry endpoint and DI 
│   └── IOptionsVariantsDemo.csproj
├── IOptionsDemo.Tests/            # xUnit test project
|    ├── StaticOptionsServiceTests.cs
|    ├── SnapshotOptionsServiceTests.cs
|    ├── MonitorOptionsServiceTests.cs
|    └── IOptionsDemo.Tests.csproj
|
├── README.md
└── IOptionsDemo.sln


---

## Features

- Demonstrates registration and usage of:
  - `IOptions<T>` (singleton, static options)
  - `IOptionsSnapshot<T>` (scoped, per-request options)
  - `IOptionsMonitor<T>` (singleton, change-tracking options)
- Shows how to bind configuration sections to strongly-typed options classes.
- Includes unit tests for options-based services.
- Uses .NET 9 and ASP.NET Core minimal hosting model.
- OpenAPI (Swagger) enabled for API exploration.

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 (or later) or VS Code

### Build and Run

1. **Clone the repository:**

	``` cmd
	git clone <repo-url>
	```

2. **Restore dependencies:**
   ``` cmd
	donet restore
   ```

3. **Build the solution:**
   ``` cmd
	donet build
   ```

4. **Run the application:**
   ``` cmd
	donet run
   ```


5. **Access the API:**
   - Open [https://localhost:7172/swagger](https://localhost:7172/swagger) (or the URL shown in the console) to view the OpenAPI UI.

---

## Options Pattern Overview

### IOptions<T>

- **Lifetime:** Singleton
- **Usage:** For options that do not change during the application's lifetime.
- **Injection:** `IOptions<FeatureOptions>`
- **Example Service:** `StaticOptionsService`

### IOptionsSnapshot<T>

- **Lifetime:** Scoped (per-request)
- **Usage:** For options that may change between requests (e.g., in web apps).
- **Injection:** `IOptionsSnapshot<FeatureOptions>`
- **Example Service:** `SnapshotOptionsService`

### IOptionsMonitor<T>

- **Lifetime:** Singleton
- **Usage:** For options that can change at runtime and need to notify consumers.
- **Injection:** `IOptionsMonitor<FeatureOptions>`
- **Example Service:** `MonitorOptionsService`

---

## Configuration

Options are configured in `Program.cs`:
``` csharp
builder.Services.Configure<FeatureOptions>(builder.Configuration.GetSection("FeatureOptions"));
```


A typical `FeatureOptions` class:

``` csharp
public class FeatureOptions
{
    public const string SectionName = "Feature";
    public bool IsEnabled { get; init; }
    public required string Message { get; init; }
}
```


And in `appsettings.json`:

``` json
{
  "Feature": {
    "Enabled": true,
    "Message": "Hello from config!"
  }
}
```

---

## Testing

Unit tests are located in the `IOptionsDemoTest` project and use [xUnit](https://xunit.net/):

- **Run tests:**

``` cmd
dotnet test
```


---

## Extending the Demo

- Add new options classes and bind them in `Program.cs`.
- Implement additional services using different options patterns.
- Add integration tests for controllers.
- Experiment with runtime configuration changes and observe `IOptionsMonitor<T>` behavior.

---

## References

- [Microsoft Docs: Options pattern in ASP.NET Core](https://learn.microsoft.com/aspnet/core/fundamentals/configuration/options)
- [IOptions<T> vs IOptionsSnapshot<T> vs IOptionsMonitor<T>](https://andrewlock.net/exploring-options-monitor-in-asp-net-core/)
- [xUnit Testing](https://xunit.net/)

---

## License

This project is for demonstration purposes and is not licensed for production use.


  