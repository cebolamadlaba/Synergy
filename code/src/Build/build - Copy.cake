
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
var publishProjectFile ="../StandardBank.ConcessionManagement.UI/StandardBank.ConcessionManagement.UI.csproj";
//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("../StandardBank.ConcessionManagement.UI/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
    CleanDirectory(packageDir);
    CleanDirectory(packageTempRootDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("../../StandardBank.ConcessionManagement.sln");
});

Task("Install-NPM")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(()=>{
        ChocolateyInstall("nodejs");

    })  ;  

Task("Build")
    .IsDependentOn("Install-NPM")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("../../StandardBank.ConcessionManagement.sln", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("../../StandardBank.ConcessionManagement.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

//Task("Run-Unit-Tests")
//    .IsDependentOn("Build")
//    .Does(() =>
//{
//    VSTest("../**/bin/" + configuration + "/*Test.dll");
// //MSTest("C:\\source\\ForWomen\\Khd.Forwomen.Test\\bin\\Release\\Khd.Forwomen.Test.dll");
//});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Publish")
    //.IsDependentOn("Run-Unit-Tests")
	.IsDependentOn("Build")
    .Does(() => {
	    DotNetCorePublish(
            publishProjectFile,
            new DotNetCorePublishSettings()
            {
                Configuration = configuration,
                OutputDirectory = packageDir,
            });
    });

Task("Run-NPM-Global")
    .IsDependentOn("Publish")
    .Does(() => {
    
    
        var settings = new NpmInstallSettings();

        settings.Global = true;
        settings.Production = false;
        settings.LogLevel = NpmLogLevel.Error;

        settings.AddPackage("gulp@next");
        

        NpmInstall(settings);
    });


Task("Run-NPM")    
    .IsDependentOn("Run-NPM-Global")
    .Does(() => {
        var settings = new NpmInstallSettings();

        settings.LogLevel = NpmLogLevel.Error;
        settings.WorkingDirectory = packageDir.ToString();
        

        NpmInstall(settings);
    });

//Task("Run-Gulp")
//        .IsDependentOn("Run-NPM")
//        .Does(() => 
//        {
//           
//            // Executes gulp from a global installation (npm install -g gulp)
//            Gulp.Global.Execute(settings => settings.WithGulpFile(packageDir.ToString() + "/gulpfile.js"));
//           
//            // Executes gulp from a local installation (npm install gulp)
//           
//        });    

Task("Clean-nodemodules")
    .IsDependentOn("Run-NPM")
    .Does(() =>{
        CleanDirectory(packageDir + "/node_modules");
        var settings = new NpmInstallSettings();
        
        settings.Production = true;
        settings.LogLevel = NpmLogLevel.Error;
        settings.WorkingDirectory = packageDir.ToString();
         
        NpmInstall(settings);
    });


Task("Default")
    .IsDependentOn("Clean-nodemodules");
    
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);