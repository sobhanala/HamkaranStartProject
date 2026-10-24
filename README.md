# Anbar â€” Warehouse Management Desktop Project

A C# Windows Forms project for warehouse and inventory management, organized around domain entities, persistence, application services, and UI modules.

## Main components

- `AnbarDomain/`: inventory, products, parties, warehouses, and receipt models.
- `AnbarForm/`: warehouse, product, party, and receipt-management forms.
- `AdminForm/`: administration UI.
- `Anbar.sln`: Visual Studio solution.
- Additional application, infrastructure, and persistence projects support the desktop modules.

## Open and build

Use Windows with Visual Studio, the .NET desktop development workload, and the .NET Framework 4.7.2 targeting pack.

1. Open `Anbar.sln`.
2. Restore NuGet dependencies.
3. Review project references and local database connection strings in the application configuration files.
4. Select `Shell` as the executable startup project and build the solution.

The repository includes legacy project files, generated datasets, and bundled DLLs. Some references and output paths may depend on the original development environment; a clean build on a new machine has not been verified.

Preserve existing third-party license files when using the bundled components.
