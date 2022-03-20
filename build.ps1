[CmdletBinding()]
param (
	[bool]
    [Parameter(Mandatory)]
    [ValidateNotNullOrEmpty()]
    $UseLocalBuild
)

Write-Information "UseLocalBuild: $UseLocalBuild"

if ($UseLocalBuild)
{
	dotnet restore `
		./HomeAssignment.WebApi/HomeAssignment.WebApi.csproj
		
	dotnet build `
		./HomeAssignment.WebApi/HomeAssignment.WebApi.csproj `
		-c Release `
		-o ./build
		
	dotnet publish `
		./HomeAssignment.WebApi/HomeAssignment.WebApi.csproj `
		-c Release `
		-o ./publish

	
	docker build `
		--tag 'local/home-assignment' `
		--file './DockerfileLocalBuild' `
	.
}
else
{
	docker build `
		--tag 'local/home-assignment' `
		--file './Dockerfile' `
	.
}
