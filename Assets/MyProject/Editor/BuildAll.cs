using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;


public class BuildAll
{

    [MenuItem("Build/Build Windows + Android")]
    public static void BuildWindowsAndAndroid()
    {
        string[] scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);

        // Android

        BuildPlayerOptions android = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "Builds/Android/Bruxas.apk",
            target = BuildTarget.Android,
            options = BuildOptions.None
        };
         
        BuildPipeline.BuildPlayer(android);

        // Windows

        BuildPlayerOptions windows = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "Builds/Windows/Bruxas.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(windows);
    }

}