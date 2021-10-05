using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class DestrObjContainer : EditorWindow
{
    public GameObject Prefab;
    
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
        serializedObject.ApplyModifiedPropertiesWithoutUndo();

        if (GUILayout.Button("Spawn"))
        {
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        int undoID = Undo.GetCurrentGroup();
        
        foreach (Object o in Selection.objects)
        {
            if (o is GameObject g)
            {
                Object prefab = PrefabUtility.InstantiatePrefab(Prefab, g.transform);
                Undo.RegisterCreatedObjectUndo(prefab, "Created object");
            }
        }

        Undo.CollapseUndoOperations(undoID);
    }
}