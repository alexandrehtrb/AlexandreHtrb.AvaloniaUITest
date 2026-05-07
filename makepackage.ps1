Get-ChildItem -Include bin,obj,TestResults -Recurse | Remove-Item -Force -Recurse -ErrorAction Ignore
dotnet restore --nologo --verbosity quiet
dotnet build --no-restore --configuration Release --nologo --verbosity quiet
dotnet pack ./AlexandreHtrb.AvaloniaUITest/AlexandreHtrb.AvaloniaUITest.csproj --nologo --verbosity quiet --configuration Release
[void](([XML]$nugetCsprojXml = Get-Content ./AlexandreHtrb.AvaloniaUITest/AlexandreHtrb.AvaloniaUITest.csproj))
$versionName = $nugetCsprojXml.Project.PropertyGroup.PackageVersion
$filePath = "./AlexandreHtrb.AvaloniaUITest/bin/Release/AlexandreHtrb.AvaloniaUITest.${versionName}.nupkg"
Write-Host "Package generated at ${filePath}" -ForegroundColor DarkGreen
