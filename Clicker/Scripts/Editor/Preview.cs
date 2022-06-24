using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Icons))]
public class Preview : Editor
{
    Icons icons;
    Texture2D texture1;
    Texture2D texture2;

    public override void OnInspectorGUI()
    {
        icons = (Icons)target;
        GUILayout.BeginHorizontal();
            texture1 = AssetPreview.GetAssetPreview(icons.sprite);
            GUILayout.Label(texture1);
            texture2 = AssetPreview.GetAssetPreview(icons.unknowIcon);
            GUILayout.Label(texture2);
        GUILayout.EndHorizontal();
        DrawDefaultInspector();
    }
}
