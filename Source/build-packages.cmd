dotnet build ./MicroPermissions/MicroPermissions.csproj -c Release
dotnet build ./MicroPermissions.AspNetCore/MicroPermissions.AspNetCore.csproj -c Release
dotnet build ./MicroPermissions.DataAccess/MicroPermissions.DataAccess.csproj -c Release
nuget pack ./MicroPermissions/.nuspec
nuget pack ./MicroPermissions.AspNetCore/.nuspec
nuget pack ./MicroPermissions.DataAccess/.nuspec

if not exist "../packages" (
	mkdir "../packages"
)

move ./MicroPermissions*.nupkg ../packages/
