# CarrierAPI

## Structure Overview

##### Repository pattern is used:
- Repositories are available under the `Repositories` folder
- Services are available under the `Services` folder
- Controllers are available under the `Controllers` folder
- Entities/Models are available under the `Models` folder
- DTOs are available under the `DTOs` folder
- Startup file is `Program.cs`
- Database script is `database.sql`

Hangfire is implemented, and it is available under `/hangfire` endpoint

## Running
Run the `database.sql` script on your SQL server instance. It is generated using [ef migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli) script generation tool.

Ensure that you have set environment variable `DB_CONNECTION_STRING` using user-secrets via `dotnet user-secrets set`. Read more [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0)

```console
dotnet user-secrets set "DB_CONNECTION_STRING" "your_connection_string_here"
```

### Differences & Detected Problems
- Carrier's `CarrierConfigurationId` field was not added to the `Carrier` entity to prevent breaking the one-to-many relationship between `Carriers` and `CarrierConfigurations`, as shown on the diagram. Adding that would result in one-to-one relationship between `Carriers` and `CarrierConfigurations`.
- Carrier's `CarrierPlusDesiCost` field is implemented as a `decimal` type to be consistent with `CarrierConfiguration`'s `CarrierCost` field.