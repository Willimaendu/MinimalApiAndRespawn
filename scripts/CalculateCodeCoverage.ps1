dotnet restore ..\MinimalApiAndRespawn.sln
dotnet build ..\MinimalApiAndRespawn.sln --no-restore --configuration release
dotnet test ..\tests\MinimalApiAndRespawn.Tests\MinimalApiAndRespawn.Tests.csproj --no-build --no-restore --configuration release --logger:trx -v minimal /p:CollectCoverage=true /p:ExcludeByAttribute=\"GeneratedCodeAttribute\" /p:Exclude=[*]*.Migrations.* /p:CoverletOutputFormat=opencover
dotnet tool update -g dotnet-reportgenerator-globaltool
reportgenerator "-reports:..\tests\**\*.opencover.xml" "-targetdir:coveragereport" -reporttypes:HTMLInline
.\coveragereport\index.html