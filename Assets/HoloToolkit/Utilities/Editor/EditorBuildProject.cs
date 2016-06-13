using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class EditorBuildProject : MonoBehaviour {

    private static string UnityBuildAppFolder = "App";

    private static string MSBuildPath = @"C:\Program Files(x86)\MSBuild\14.0\Bin\";

    private static string NugetPath = @"c:\";


    [MenuItem("HoloToolkit/Build/UnityBuild", false)]
    public static void Build()
    {
        var res = BuildPipeline.BuildPlayer(new string[0], "App", BuildTarget.WSAPlayer, BuildOptions.None);
    }

    [MenuItem("HoloToolkit/Build/NugetRestore", false)]
    public static void NuGetRestore()
    {
        var foo = GetNuGetRestoreProcess();
        foo.Start();
    }

    [MenuItem("HoloToolkit/Build/Compile", false)]
    public static void RunBuild()
    {
        var proc = GetMsBuildProcess();
        proc.Start();
    }


    private static Process GetNuGetRestoreProcess()
    {
        var path = GetAppPath() + PlayerSettings.productName + "/project.json";
        Process foo2 = new Process();

        foo2.StartInfo.FileName = "nuget.exe";
        foo2.StartInfo.WorkingDirectory = NugetPath;
        foo2.StartInfo.Arguments = @"restore " + path;

        return foo2;
    }

    private static Process GetMsBuildProcess()
    {
        var path = GetAppPath() + PlayerSettings.productName + ".sln";
        return new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "msbuild.exe",
                WorkingDirectory = MSBuildPath,
                Arguments = path + @" /t:Rebuild /p:Configuration=Release;Platform=x86;AppxBundlePlatforms=x86;AppxBundle=Always",
            }
        };
    }

    private static string GetAppPath()
    {
        return string.Format("{0}/../{1}/", UnityEngine.Application.dataPath, UnityBuildAppFolder);
    }
}
