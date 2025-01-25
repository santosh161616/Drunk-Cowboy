using UnityEditor;
using UnityEngine;
using Microsoft.Win32;

public class PlayerPrefsViewer : EditorWindow
{
    private string companyName;
    private string productName;
    private Vector2 scrollPostion;

    private string[] keys;
    private string[] values;
    private string[] types;

    [MenuItem("Tools/PlayerPrefs Viewer")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsViewer>("PlayerPrefs Viewer");
    }

    private void OnEnable()
    {
        companyName = Application.companyName;
        productName = Application.productName;

        LoadRegistryData();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("PlayerPrefs Viewer");
        EditorGUILayout.Space();

        companyName = EditorGUILayout.TextField(companyName);
        productName = EditorGUILayout.TextField(productName);

        if (GUILayout.Button("Referesh"))
        {
            LoadRegistryData();
        }
        EditorGUILayout.Space();


        if (keys != null && values != null)
        {
            scrollPostion = EditorGUILayout.BeginScrollView(scrollPostion);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Keys", GUILayout.Width(300));
            EditorGUILayout.LabelField($"Values", GUILayout.Width(300));
            EditorGUILayout.LabelField("Type", GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < keys.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (keys[i].Contains("_h"))
                {
                    string value = keys[i].Split("_h")[0];
                    EditorGUILayout.LabelField($"{value}", GUILayout.Width(300));
                }
                EditorGUILayout.LabelField(types[i], GUILayout.Width(35));
                EditorGUILayout.TextField($"{values[i]}", GUILayout.Width(300));

                if (GUILayout.Button("Delete", GUILayout.Width(80)))
                {
                    DeleteRegistryKey(keys[i]);
                    Debug.Log($"Deleted Key {keys[i]}");
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }
        else
        {
            Debug.Log($"No PlayerPrefs Data Found.");
        }

    }
    //private void LoadRegistryData()
    //{
    //    string subKeyPath = "Software\\Unity\\UnityEditor\\" + companyName + "\\" + productName;

    //    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath))
    //    {
    //        if (key != null)
    //        {
    //            keys = key.GetValueNames();
    //            values = new string[keys.Length];
    //            types = new string[keys.Length];

    //            for (int i = 0; i < keys.Length; i++)
    //            {
    //                if (keys[i].EndsWith("__type")) continue; // Skip type metadata keys
    //                values[i] = key.GetValue(keys[i])?.ToString() ?? "NA";

    //                // Determine type
    //                string typeKey = keys[i] + "__type";
    //                object typeValue = key.GetValue(typeKey);
    //                types[i] = typeValue?.ToString() ?? "string"; // Default to string if no type key exists
    //            }
    //        }
    //        else
    //        {
    //            keys = new string[0];
    //            values = new string[0];
    //            types = new string[0];
    //            Debug.Log("No Values Found!");
    //        }
    //    }
    //}
    private void LoadRegistryData()
    {
        string subKeyPath = "Software\\Unity\\UnityEditor\\" + companyName + "\\" + productName;

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath))
        {
            if (key != null)
            {
                keys = key.GetValueNames();
                values = new string[keys.Length];
                types = new string[keys.Length];

                int validCount = 0;

                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].EndsWith("__type")) continue; // Skip type metadata keys

                    string keyName = keys[i];
                    object rawValue = key.GetValue(keyName);

                    keys[validCount] = keyName;
                    values[validCount] = rawValue?.ToString() ?? "NA";
                    types[validCount] = InferType(rawValue); // Infer the type if no __type metadata is present
                    validCount++;
                }

                // Resize arrays to valid count
                System.Array.Resize(ref keys, validCount);
                System.Array.Resize(ref values, validCount);
                System.Array.Resize(ref types, validCount);
            }
            else
            {
                keys = new string[0];
                values = new string[0];
                types = new string[0];
                Debug.Log("No PlayerPrefs Data Found.");
            }
        }
    }

    private string InferType(object rawValue)
    {
        if (rawValue == null) return "string";

        string valueStr = rawValue.ToString();

        // Try parsing as int
        if (int.TryParse(valueStr, out _))
        {
            return "int";
        }

        // Try parsing as float
        if (float.TryParse(valueStr, out _))
        {
            return "float";
        }

        // Default to string
        return "string";
    }

    private void DeleteRegistryKey(string keyName)
    {
        string subKeyPath = "Software\\Unity\\UnityEditor\\" + companyName + "\\" + productName;
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath, writable: true))
        {
            if (key != null)
            {
                key.DeleteValue(keyName);
                key.DeleteValue(keyName + "__type", false); // Delete the type metadata (if exists)
                Debug.Log($"Deleted KeyName {keyName}");
            }
        }

    }

}
