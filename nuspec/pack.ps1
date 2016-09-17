$sourceNugetExe = "http://nuget.org/nuget.exe"
$targetNugetExe = $PSScriptRoot + "\nuget.exe"

if (-Not(Test-Path ("nuget.exe")))
{
	Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
}

Set-Alias nuget $targetNugetExe -Scope Global -Verbose

del *.nupkg

nuget pack MvvmCross.Controls.Android.SectionedRecyclerView.nuspec -Symbols
nuget pack MvvmCross.Controls.IncrementalLoadingList.nuspec -Symbols

pause
