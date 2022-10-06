using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FreeMasonEditor : EditorWindow {

    [MenuItem("Freemason/Toolkit/Banner")]
    [MenuItem("Freemason/Toolkit/Camera/Detection Banner")]
    [MenuItem("Freemason/Toolkit/Camera/Timer")]
    [MenuItem("Freemason/Toolkit/Streaming/Image")]
    [MenuItem("Freemason/Toolkit/Streaming/Video")]
    private static void ShowToolkit() {
        var window = GetWindow<FreeMasonEditor>();
        window.titleContent = new GUIContent("Toolkit");
        window.Show();
    }

    [MenuItem("Freemason/Example/First Time")]
    [MenuItem("Freemason/Example/Version 1.0")]
    [MenuItem("Freemason/Example/Version 1.1.2")]
    private static void ShowWindow() {
        var window = GetWindow<FreeMasonEditor>();
        window.titleContent = new GUIContent("Example");
        window.Show();
    }

    [MenuItem("Freemason/Prefabs/Plane")]
    private static void ShowPrefabs() {
        var window = GetWindow<FreeMasonEditor>();
        window.titleContent = new GUIContent("Prefabs");
        window.Show();
    }
    private void OnGUI() {
        
    }
}