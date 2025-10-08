Add your test projects inside this folder, e.g. MasrafProject.Tests.
Example command:
  dotnet new xunit -n MasrafProject.Tests -o MasrafProject.Tests
  dotnet add MasrafProject.Tests/MasrafProject.Tests.csproj reference MasrafProject/MasrafProject.Application/MasrafProject.Application.csproj
Then the CI will pick them up automatically.
