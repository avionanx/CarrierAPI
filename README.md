# CarrierAPI

# Running
Run `database.sql` script on your SQL server instance. It is generated using [ef migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli) script generation tool.

Ensure that you have set environment variable `DB_CONNECTION_STRING` using user-secrets or replace it with any other implementation.


# Differences & Detected Problems
- Carrier's `CarrierConfigurationId` field was not added since that would break one-to-many relationship between Carriers and CarrierConfiguraitons, as shown on the diagram.
- Carrier's `CarrierPlusDesiCost` field is implemented as decimal to fit CarrierConfiguration's CarrierCost field.
- Rather than having a foreign key, there is weak relationship between CarrierReports and Carriers to prevent deletion cascades