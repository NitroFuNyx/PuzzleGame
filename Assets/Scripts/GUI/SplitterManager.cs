using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SpliterManager
{
    static SpliterManager()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (gameObject == null) return;

        var splitter = gameObject.GetComponent<Splitter>();
        if (splitter == null) return;

        splitter.tag = "EditorOnly";

        var styleState = new GUIStyleState() { textColor = splitter.TextColor };
        var style = new GUIStyle()
        {
            normal = styleState,
            fontStyle = FontStyle.Bold,
            alignment = splitter.TextAlignment
        };

        EditorGUI.DrawRect(selectionRect, splitter.BackgroundColor);
        EditorGUI.LabelField(selectionRect, splitter.name, style);
    }
}