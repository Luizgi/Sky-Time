using UnityEditor;
using UnityEngine;

public class CollisionTester : EditorWindow
{
    private GameObject selectedObject;

    [MenuItem("Window/Collision Tester")]
    public static void ShowWindow()
    {
        GetWindow<CollisionTester>("Collision Tester");
    }

    private void OnGUI()
    {
        selectedObject = EditorGUILayout.ObjectField("Object to Test", selectedObject, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Perform Collision Test"))
        {
            if (selectedObject != null)
            {
                Collider[] colliders = selectedObject.GetComponentsInChildren<Collider>();

                foreach (Collider collider in colliders)
                {
                    // Perform collision tests or visualization here
                    // This could include highlighting colliders, displaying collision information, etc.
                    Debug.Log("Colliding with: " + collider.name);
                }
            }
        }
    }
}
