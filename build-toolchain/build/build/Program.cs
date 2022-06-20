using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.Test;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

public class BuildContext : FrostingContext
{
    public bool SkipClean { get; set; }
    public string OutputDir { get; set; }
    public string BasePath { get; set; }
    public string MsBuildConfiguration { get; set; }

    public string Target { get; set; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        SkipClean = context.Arguments.HasArgument("skip-clean");
        OutputDir =
            context.Arguments.HasArgument("output-dir")
            ? context.Arguments.GetArgument("output-dir")
            : "./out";

        MsBuildConfiguration =
            context.Arguments.HasArgument("configuration")
            ? context.Arguments.GetArgument("configuration")
            : "Release";

        BasePath =
       context.Arguments.HasArgument("base-path")
       ? context.Arguments.GetArgument("base-path")
       : "./";
        if (!BasePath.EndsWith("/"))
        {
            BasePath += "/";
        }

        Target = context.Arguments.GetArgument("target");
        context.Log.Information(Target);
    }
}

[TaskName("Clean")]
public class CleanTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (context is BuildContext c && c.SkipClean == false)
        {
            context.CleanDirectory(c.OutputDir);
        }
    }
}

[TaskName("Build")]
public class BuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var csprojs = context.GetFiles(context.BasePath + "**/*.csproj");
        foreach (var csproj in csprojs)
        {
            //Build all projects under basepath 
            context.DotNetBuild(csproj.FullPath, new DotNetBuildSettings
            {
                Configuration = context.MsBuildConfiguration,
                OutputDirectory = context.OutputDir
            });
        }

    }
}

[TaskName("Test")]
public class TestTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var csprojs = context.GetFiles(context.BasePath + "test/*.csproj");
        foreach (var csproj in csprojs)
        {

            context.DotNetTest(csproj.FullPath, new DotNetTestSettings
            {
                Configuration = context.MsBuildConfiguration,
                NoBuild = context.Target == "All", //Can skip build when using all target
            });
        }
    }
}


[TaskName("All")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(BuildTask))]
[IsDependentOn(typeof(TestTask))]
public class AllTask : FrostingTask
{

}



[TaskName("Default")]
public class DefaultTask : FrostingTask
{
    public override void Run(ICakeContext context)
    {
        context.Log.Information(@"Please specify a task to run.
        Available Tasks are:
        -Build: Builds the project
        -Test: Tests the project
        -Clean: Cleans the output directory
        -All: Cleans,Builds and Tests the project");
    }
}