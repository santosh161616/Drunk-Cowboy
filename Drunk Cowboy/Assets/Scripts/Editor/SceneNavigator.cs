using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneNavigator : EditorWindow
{
    private Vector2 scrollPosition;
    [MenuItem("Tools/Scene Navigator", priority = 3)]
    public static void ShowWindow()
    {
        GetWindow<SceneNavigator>("Scene Navigator");
    }
    private void OnGUI()
    {
        // Display a bold label
        EditorGUILayout.LabelField("Active Scenes in Build Settings", EditorStyles.boldLabel);

        // Retrieve active scenes
        string[] activeScenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        // Display the count of active scenes
        EditorGUILayout.LabelField($"Count: {activeScenes.Length}", EditorStyles.label);

        // Button to log all active scenes
        if (GUILayout.Button("Log Active Scenes to Console"))
        {
            if (activeScenes.Length == 0)
            {
                Debug.Log("No active scenes found in Build Settings.");
            }
            else
            {
                Debug.Log("Active Scenes in Build Settings:");
                foreach (string scene in activeScenes)
                {
                    Debug.Log(scene);
                }
            }
        }

        // Additional Button: Refresh the Active Scenes
        if (GUILayout.Button("Refresh Active Scenes"))
        {
            Debug.Log("Refreshing active scenes...");
            Repaint();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Play", EditorStyles.boldLabel);
        string firstScenePath = EditorBuildSettings.scenes[0].path;
        string firstSceneName = System.IO.Path.GetFileNameWithoutExtension(firstScenePath);

        // Extract scene name from the path
        if (GUILayout.Button("Play Game"))
        {
            EditorSceneManager.OpenScene(firstScenePath);
            EditorApplication.isPlaying = true;
        }

        //EditorGUILayout.Space();
        EditorGUILayout.LabelField("Scenes", EditorStyles.boldLabel);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(500)); // Adjust height as needed
        foreach (string scene in activeScenes)
        {
            if (GUILayout.Button(scene, GUILayout.ExpandWidth(true)))
            {
                OpenScene(scene);
                Debug.Log($"Scene Selected: {scene}");
            }
        }
        EditorGUILayout.EndScrollView();
    }

    public void OpenScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
            Debug.Log($"Opened Scen {scenePath}");
        }
    }

}
