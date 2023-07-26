## Assumptions
I asumed that the id's returned by https://hacker-news.firebaseio.com/v0/beststories.json
are returned ordered by score in descending order. It's not documented but that's how it works.

## Possible Enchancements
If I had more time, I would considered also to do the following things:
1. Wrote Unit Tests
2. Wrote Stress Tests
3. Added possibility to limit the quantity of incoming requests. Now we could control overloading of the Hacker News API
by specifying batch size, which is good. But if we will have extreme load we might need to limit quantity of incoming requests as well.
For this purpose we can use Microsoft.AspNetCore.RateLimiting middleware.

## Running Application
From console: dotnet run --project (where project is a path to SantanderCodingTest\SantanderCodingTest.csproj)
From Visual Studio: Open project in Visual Studio, build solution, set CycodeBackendExercise as startup project and press ctrl+f5

## Launch Profiles
Application has three launch profiles specified in launchSettings.json

"IIS Express" - for running app using IIS Express server and
"https" - for running app as self-hosted application via https
"http" - for running app as self-hosted application via http'