#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveableObject), editorForChildClasses: true)]
[CanEditMultipleObjects]
public class SaveableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector
        DrawDefaultInspector();

        foreach (var t in targets)
        {
            SaveableObject saveable = (SaveableObject)t;

            EditorGUILayout.Space();

            // Show current ID
            EditorGUILayout.LabelField("Object ID:", saveable.ObjectID);

            // Button to generate new ID
            if (GUILayout.Button("Generate New ID"))
            {
                saveable.ObjectID = System.Guid.NewGuid().ToString();
                EditorUtility.SetDirty(saveable);
            }
        }
    }
}
#endif