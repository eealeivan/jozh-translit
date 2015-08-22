$path = Split-Path -Path $MyInvocation.MyCommand.Path
$syncProjectsPath = $path + "\sync-projects.ps1"

# Normal projects
$projectsPath = "'" + $path + "\..\Src\JoZhTranslit" + "'"
$projectsParams = @(
  "-projectsPath", $projectsPath, 
  "-sourceProject", "JoZhTranslit.csproj")
Invoke-Expression "& `"$syncProjectsPath`" $projectsParams"

# Test projects
$testProjectsPath = "'" + $path + "\..\Src\JoZhTranslit.Tests" + "'"
$testProjectsParams = @(
  "-projectsPath", $testProjectsPath, 
  "-sourceProject", "JoZhTranslit.Tests.csproj",
  "-syncReferences")
Invoke-Expression "& `"$syncProjectsPath`" $testProjectsParams"