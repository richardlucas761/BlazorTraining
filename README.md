# BlazorTraining

Training in ASP.NET Core using Blazor

Starting with the instructions here:

https://dotnet.microsoft.com/en-us/learn/aspnet/blazor-tutorial/intro

There are some remarks in the Solution where things were changed from the sample code and longer form comments about the sample code and issues found here.

## Part 2 - Add and scaffold a model

https://learn.microsoft.com/en-gb/aspnet/core/blazor/tutorials/movie-database-app/part-2?view=aspnetcore-8.0&pivots=vs

Note this part of the URL, it's not clear if there is a difference here but see a later URL which is linked in the later instructions where this is explicitly for .NET 10 rather than ```view=aspnetcore-8.0```

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

## Part 3 - Learn about Razor components

https://learn.microsoft.com/en-gb/aspnet/core/blazor/tutorials/movie-database-app/part-3?view=aspnetcore-8.0&pivots=vs

And there is a .NET Core 10 version of this page here I should have been following, did I miss something else in an earlier page?

https://learn.microsoft.com/en-gb/aspnet/core/blazor/tutorials/movie-database-app/part-3?view=aspnetcore-10.0&pivots=vs&preserve-view=true#details-component

### Example code doesn't include the "counter" page in the sample code

A very minor observation, replacing the code in ```Components/Layout/NavMenu.razor``` with this from the page linked would remove the navigation link to the counter page?

```
<div class="top-row ps-3 navbar navbar-dark">
    <div class="container-fluid">
        <a class="navbar-brand" href="">Sci-fi Movies</a>
    </div>
</div>

<input type="checkbox" title="Navigation menu" class="navbar-toggler" />

<div class="nav-scrollable" onclick="document.querySelector('.navbar-toggler').click()">
    <nav class="nav flex-column">
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
                <span class="bi bi-house-door-fill-nav-menu" aria-hidden="true"></span> Home
            </NavLink>
        </div>

        <div class="nav-item px-3">
            <NavLink class="nav-link" href="weather">
                <span class="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Weather
            </NavLink>
        </div>

        <div class="nav-item px-3">
            <NavLink class="nav-link" href="movies">
                <span class="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Movies
            </NavLink>
        </div>
    </nav>
</div>
```

### Suggested refactoring on the Delete page is not valid

This part of the sample code ```disabled="@(movie is null)"``` had a suggested refactoring.

```
        <EditForm method="post" Model="movie" OnValidSubmit="DeleteMovie" FormName="delete" Enhance>
            <button type="submit" class="btn btn-danger" disabled="@(movie is null)">Delete</button> |
            <a href="/movies">Back to List</a>
        </EditForm>
```

But this just creates a new Component which is perhaps more readable but it's still complaining about the 'disabled' attribute.

![alt text](Images/InvalidRefactoring.png "Invalid Refactoring")

Something either in the VS IDE or one of the extensions I have installed isn't happy with this syntax, this suggested refactoring will be ignored.

## Part 4 - Work with a database

https://learn.microsoft.com/en-gb/aspnet/core/blazor/tutorials/movie-database-app/part-4?view=aspnetcore-10.0&pivots=vs

### Don't use VARCHAR(MAX) for everything! 😊

The suggested design uses VARCHAR(MAX) for the Title and Genre columns of the Movie database table.

IMDB suggests the longest Movie title is around 200 characters.

### Data/SeedData.cs suggests throwing a NullReferenceException

The Sonar Qube VS Extension reminds us this is bad practice so this was corrected.

## Part 5 - Add validation

https://learn.microsoft.com/en-gb/aspnet/core/blazor/tutorials/movie-database-app/part-5?view=aspnetcore-10.0&pivots=vs

Some of the validations suggested here were already added to the Movie model in earlier commits.

### Odd choices for validation?

Pedantic but ```MinimumLength = 3``` is an odd choice when there are movies whose name is a single letter, these could not be stored by this application.

If we only support these characters then the RegEx of ```[RegularExpression(@"^[A-Z]+[a-zA-Z()\s-]*$")]``` seems reasonable, but why only apply this to the Genre property, why not the Title too?

Applying the same RegEx to the Title does stop us from storing the π movie by Darren Aronofsky but stops HTML characters and script injection attempts.

```
public class Movie
{
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string? Title { get; set; }

    [Required]
    [StringLength(30)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z()\s-]*$")]
    public string? Genre { get; set; }
}
```

## Part 6 - Add search

https://learn.microsoft.com/en-gb/aspnet/core/blazor/tutorials/movie-database-app/part-6?view=aspnetcore-10.0&pivots=vs

### SQL Injection is prevented by something?

Specifying this query string does not result in the Movie table being truncated in the database which is good, but *what* is stopping this?

```https://localhost:7035/movies?titleFilter=%3Btruncate+table+movie%3B```

A search of:

```;truncate table movie;```

## TODO

Validation of the Movie at both the model and database level: 

- ~~Price should not be negative~~. Fixed by adding a Range Data Annotation -> https://stackoverflow.com/questions/20286290/dataannotation-for-checking-if-the-integer-is-not-a-negative-value
- Prevent HTML and script tags, the website doesn't render this ```jhrejghrejhgrh<b>bold</b><html><script>alert('hello')</script></html>``` or display an alert box but better to stop it being input in the first place to prevent any complications with this text later.

Properly resolve the ```TrustServerCertificate=True``` issue, as this is only a development sandbox this is possibly a "fix never" issue or might be covered later in the Blazor training instructions.

Multiple Genre for a Movie? Separate Combo Box with a list of existing Genre to choose from and the ability to ad-hoc create new ones. Update the Create and Edit pages to include this. New model and database table.

SQL Injection is prevented by *something*, perhaps it's "good enough" that it's prevented? Curious what is stopping this.

## Original local DB connection string

```
"BlazorWebAppMoviesContext": "Server=(localdb)\\mssqllocaldb;Database=BlazorWebAppMoviesContext-c3a5fbd7-c1b2-4078-b26f-7338cdb431b9;Trusted_Connection=True;MultipleActiveResultSets=true"
```