using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class DestrObjContainer : EditorWindow
{
    public GameObject Prefab;
    public bool SelectSpawnedObjects = false;
    [MenuItem("Tools/Spawn DO's")]

    public static void OpenWindow()
    {
        DestrObjContainer window = (DestrObjContainer)GetWindow(typeof(DestrObjContainer));
        window.Show();
    }

    public void OnGUI()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Prefab"), new GUIContent("Prefab to spawn"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SelectSpawnedObjects"),
            new GUIContent("Select Spawned Obj?"));
        serializedObject.ApplyModifiedPropertiesWithoutUndo();

        if (GUILayout.Button("Spawn"))
        {
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        int undoID = Undo.GetCurrentGroup();
        List<Object> selectedObj = new List<Object>();
        foreach (Object o in Selection.objects)
        {
            if (!(o is GameObject g)) continue;
            Object prefab = PrefabUtility.InstantiatePrefab(Prefab, g.transform);
            Undo.RegisterCreatedObjectUndo(prefab, "Created object");
            selectedObj.Add(SelectSpawnedObjects ? prefab : o);
        }
        Selection.objects = selectedObj.ToArray();
        Undo.CollapseUndoOperations(undoID);
    }
}