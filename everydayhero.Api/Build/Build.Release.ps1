# This script will build the project in release mode, then create the nuget package.  It will download nuget.exe if it doesn't exist.  
"--------------------------------------"
"Building and packaging RELEASE with Package"
"--------------------------------------"
$buildFile = "Release.msbuild"
$outputPath = "bin\Release\."
$msbuild = $Env:SystemRoot +"\Microsoft.Net\Framework64\v4.0.30319\msbuild.exe"
$options = "/p:VisualStudioVersion=12.0"
$nugetPath = "C:\Nuget"
$nugetFile = $nugetPath + "\nuget.exe"
$nugetServer = ".\Publish"
if((Test-Path $nugetPath) -eq $false)
{
	write-host "$nugetPath not found, creating" -foregroundcolor yellow
	New-Item $nugetPath -type directory | out-null
	write-host "$nugetPath created" -foregroundcolor green
}
if((Test-Path $nugetServer) -eq $false)
{
	write-host "$nugetServer not found, creating" -foregroundcolor yellow
	New-Item $nugetServer -type directory | out-null
	write-host "$nugetServer created.  Ensure your visual studio nuget settings point to this local source as a nuget source." -foregroundcolor green
}
if((Test-Path $nugetFile) -eq $false)
{
	write-host "$nugetFile not found, downloading" -foregroundcolor Yellow
	Invoke-WebRequest -Uri "https://www.nuget.org/nuget.exe" -OutFile $nugetFile 
	write-host "$nugetFile downloaded" -foregroundcolor Green
}
if(Test-Path $outputPath)
{
 	Remove-Item -Path $outputPath -Confirm:$false -Recurse:$true
}
write-host "Building solution"
Invoke-Expression ($msbuild + " " + $buildFile + " -verbosity:m " + $options) -ErrorAction:Stop
write-host "Running nuget package solution"
C:\Nuget\nuget.exe pack ..\everydayhero.Api.csproj -Prop Configuration=Release -outputdirectory $nugetServer 
write-host "Packaged nuget file and saved it to $nugetServer"
