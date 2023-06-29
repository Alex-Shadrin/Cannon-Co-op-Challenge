#if  UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEngine;

public class HelperUnityMenu : MonoBehaviour
{
    private const string MENU_NAME = "🚀🚀🚀🚀🚀🚀🚀🚀🚀/";
    // Add a menu item named "Do Something with a Shortcut Key" to MyMenu in the menu bar
    // and give it a shortcut (ctrl-g on Windows, cmd-g on macOS).
    [MenuItem(MENU_NAME+"Configure BuildSettings")]
    static void SortBuildSettings()
    {
        AddAllScenesToBuildSettings();
        var sortedScenes = EditorBuildSettings.scenes.OrderBy(s => s.path);
        EditorBuildSettings.scenes = sortedScenes.ToArray();
    }

    //[MenuItem("Custom/Add All Scenes to Build Settings")]
    private static void AddAllScenesToBuildSettings()
    {
        // Get all scene paths in the project
        string[] scenePaths = GetAllScenePaths();

        // Create an array to store the scene objects
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[scenePaths.Length];

        // Add each scene to the array
        for (int i = 0; i < scenePaths.Length; i++)
        {
            scenes[i] = new EditorBuildSettingsScene(scenePaths[i], true);
        }

        // Set the new array of scenes to the build settings
        EditorBuildSettings.scenes = scenes;

        Debug.Log("All scenes added to build settings.");
    }

    private static string[] GetAllScenePaths()
    {
        // Find all scene files in the project
        string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        string[] scenePaths = new string[guids.Length];

        // Convert the guids to scene paths
        for (int i = 0; i < guids.Length; i++)
        {
            scenePaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
        }

        return scenePaths;
    }
}

#endif