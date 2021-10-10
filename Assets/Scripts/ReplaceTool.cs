using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReplaceTool : EditorWindow
{
    public GameObject Prefab;
    [MenuItem("Tools/Replace Objs")]
    
    public static void OpenWindow()
    {
        ReplaceTool window = (ReplaceTool)GetWindow(typeof(ReplaceTool));
        window.Show();
    }

    public void OnGUI()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Prefab"), new GUIContent("Prefab to spawn"));
        serializedObject.ApplyModifiedPropertiesWithoutUndo();

        if (GUILayout.Button("Replace"))
        {
            ReplaceObjs();
        }
    }

    void ReplaceObjs()
    {
        foreach (var o in Selection.objects)
        {
            if (o is GameObject g)
            {
                Undo.RegisterCompleteObjectUndo(g,"DestroyedObject");
                Object prefab = PrefabUtility.InstantiatePrefab(Prefab,g.transform.parent);
                ((GameObject) prefab).transform.position = g.transform.position;
                Undo.RegisterCreatedObjectUndo(prefab, "Created Object");
                Undo.DestroyObjectImmediate(g);
            }
        }
    }
}
