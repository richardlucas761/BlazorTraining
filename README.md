# BlazorTraining

Training in ASP.NET Core using Blazor

Starting with the instructions here:

https://dotnet.microsoft.com/en-us/learn/aspnet/blazor-tutorial/intro

There are some remarks in the Solution where things were changed from the sample code and longer form comments about the sample code and issues found here.

## Part Two

https://learn.microsoft.com/en-gb/aspnet/core/blazor/tutorials/movie-database-app/part-2?view=aspnetcore-8.0&pivots=vs

### Create the initial database schema using EF Core's migration feature failed?

Something went wrong with the "Add Migration" of "InitialCreate" where it accepted the name and loaded the database context the first time, but suggested it needed a ```--force``` parameter to make the changes.

After restarting Visual Studio the error persisted, running the command from the command line rather than the VS IDE gave an error message which could be copied and pasted here:

```
C:\git\BlazorTraining\BlazorWebAppMovies\BlazorWebAppMovies>dotnet new tool-manifest
Creating this template will make changes to existing files:
  Overwrite   ./dotnet-tools.json

To create the template anyway, run the command with '--force' option:
   dotnet new tool-manifest --force

For details on the exit code, refer to https://aka.ms/templating-exit-codes#73
```

Adding the ```--force``` parameter fixed this issue.

```
C:\git\BlazorTraining\BlazorWebAppMovies\BlazorWebAppMovies>dotnet new tool-manifest --force
The template "Dotnet local tool manifest file" was created successfully.
```

This created a new "dotnet-tools.json" file with this content although it didn't resolve the issue where the migration could take place in the UI.

```
{
  "version": 1,
  "isRoot": true,
  "tools": {}
}
```

I don't particularly like the "code first" approach so this was abandoned and a SQL Server Database Project was added to the solution instead.

### Certificate issues using a local database published from a SQL Server Database Project

Creating a new database on a local SQL Server instance using a SQL Server Database Project and updating the connection string in the application to use this rather than localdb gave an error:

```
An unhandled exception occurred while processing the request.
Win32Exception: The certificate chain was issued by an authority that is not trusted.
Unknown location

SqlException: A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction)
```

This offers a temporary solution of adding ```TrustServerCertificate=True``` to the connection string.

https://stackoverflow.com/questions/17615260/the-certificate-chain-was-issued-by-an-authority-that-is-not-trusted-when-conn

## TODO

Properly resolve the ```TrustServerCertificate=True``` issue, as this is only a development sandbox this is possibly a "fix never" issue.

## Original local DB connection string

```
"BlazorWebAppMoviesContext": "Server=(localdb)\\mssqllocaldb;Database=BlazorWebAppMoviesContext-c3a5fbd7-c1b2-4078-b26f-7338cdb431b9;Trusted_Connection=True;MultipleActiveResultSets=true"
```