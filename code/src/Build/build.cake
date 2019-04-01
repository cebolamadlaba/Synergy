
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Npm"
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.Gulp"
#tool "Microsoft.TestPlatform"

// Possible Issues:
// Build.ps1 is not digitally signed - Run "Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass" in cmd prompt;
// Task.("Publish") "The Target WebPublish does not exist in the project";
// - Copy this folder "C:\Program Files (x86)\MSBuild\Microsoft\VisualStudio\v10.0\Web" to "v14.0" or "v15.0" folder;
// - Copy this folder "C:\Program Files (x86)\MSBuild\Microsoft\VisualStudio\v10.0\WebApplications" to "v14.0" or "v15.0" folder;
// Or
// - Package Manager Console: Install-Package MSBuild.Microsoft.VisualStudio.Web.targets -Version 14.0.0.3

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
/////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var packageDir = "../Build/package";
var packageTempRootDir = "../Build/packageTempRootDir";
//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("../StandardBank.ConcessionManagement.UI/bin") + Directory(configuration);
var publishProjectFile ="../StandardBank.ConcessionManagement.UI/StandardBank.ConcessionManagement.UI.csproj";
var solutionfile = "../../StandardBank.ConcessionManagement.sln";
var npmPackageFolder = "../src/StandardBank.ConcessionManagement.UI/";
var nodeModulesFolder = "../src/StandardBank.ConcessionManagement.UI/node_modules";
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(nodeModulesFolder);
    CleanDirectory(buildDir);
    CleanDirectory(packageDir);
    CleanDirectory(packageTempRootDir); 
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionfile);
});

Task("Run-NPM")    
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() => {
        var settings = new NpmInstallSettings();

        settings.LogLevel = NpmLogLevel.Error;
        settings.WorkingDirectory = npmPackageFolder.ToString();

        NpmInstall(settings);
    }); 

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Publish")
    .IsDependentOn("Run-NPM")
    .Does(() => {
	    DotNetCorePublish(
            publishProjectFile,
            new DotNetCorePublishSettings()
            {
                Configuration = configuration,
                OutputDirectory = packageDir,
            });
    });
   
Task("Default")
    .IsDependentOn("Publish");
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);