# PressAgency

A press agency website written in .NET 5 MVC. A college assignment.

# Build

## Prepare System

Make sure the PostgresSQL server is running.

Create the initial migration:

```
dotnet ef migrations add InitialMigration
dotnet ef database update
```

If you changed the model or want to reset the database:

```
rm -r Migrations/
dotnet ef database drop
```

Then create the initial migration and update again.

## Development Build

Open a browser and run:

```
dotnet watch run
```
