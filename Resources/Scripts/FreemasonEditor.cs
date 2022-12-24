using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CanEditMultipleObjects]
public class FreeMasonEditor : EditorWindow {

    [MenuItem("Freemason/Sign in")]
    private static void SignIn() {

    }

    [MenuItem("Freemason/Projects")]
    private static void ShowWindow  () {
        var projects = GetWindow<FreeMasonEditor>();
        projects.titleContent = new GUIContent("Projects");
        projects.Show();
    }

    private void OnGUI() {
        GUI.backgroundColor = Color.red;
        Projects.Id = EditorGUILayout.TextField("ID",Projects.Id);
        Projects.Name = EditorGUILayout.TextField("Name",Projects.Name);

        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Reset form project.",EditorStyles.boldLabel);
        if(GUILayout.Button("Reset" , GUILayout.Width(100), GUILayout.Height(30))) {
            Projects.Id = "0x2c4";
            Projects.Name = "Beeba";
        }
        EditorGUILayout.EndHorizontal();
    }
}

public class Projects {
    public static string Id {
        get {
            #if UNITY_EDITOR
            return EditorPrefs.GetString("ID","0x2c4");
            #else
            return false;
            #endif
        }
        set {
            #if UNITY_EDITOR
            EditorPrefs.SetString("ID",value);
            #endif
        }
    }

    public static string Name {
        get {
            #if UNITY_EDITOR
            return EditorPrefs.GetString("Name","Beeba");
            #else
            return false;
            #endif
        }
        set {
            #if UNITY_EDITOR
            EditorPrefs.SetString("Name",value);
            #endif
        }
    }
}