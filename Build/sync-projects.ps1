Param(
  [string]$projectsPath,
  [string]$sourceProject
)

Write-Host -ForegroundColor Green ("Start sync project files. " + $sourceProject + " is used as base.")

$sourceXml = [xml](Get-Content ($projectsPath + "\" + $sourceProject))
$sourceCompileIncludes = $sourceXml.Project.ItemGroup.Compile.Include

$targetProjects = (Get-ChildItem –Path $projectsPath | Where-Object {$_.Extension -eq ".csproj"}).Name
foreach($targetProject in $targetProjects) {
  if($targetProject -ne $sourceProject) {
    Write-Host -ForegroundColor Green ("Sync " + $targetProject)
	
	$targetProjectPath = $projectsPath + "\" + $targetProject;
	$targetXml = [xml](Get-Content $targetProjectPath)
	$targetNamespace = $targetXml.Project.xmlns
	$targetCompileXml = $targetXml.Project.ChildNodes.Item(5)
	
	$targetCompileXml.RemoveAll()
	foreach($sourceCompileInclude in $sourceCompileIncludes) {	  
	  $targetCompileIncludeXml = $targetXml.CreateElement('Compile', $targetNamespace) # namespace is needed to avoid adding an empty xmlns attribute
	  $targetCompileIncludeXml.SetAttribute('Include', $sourceCompileInclude)
	  $targetCompileXml.AppendChild($targetCompileIncludeXml)
	}
	
	$targetXml.Save($targetProjectPath)
  }
}