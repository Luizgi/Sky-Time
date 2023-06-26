using UnityEditor;
using UnityEngine;

public class TaskManager : EditorWindow
{

    private SerializedObject serializedObject;
    private SerializedProperty tasksProperty;
    private SerializedProperty notesProperty;

    public TaskField[] fields = new TaskField[0];

    private Vector2 scrollPosition;

    private GUIStyle textAreaStyle;

    [MenuItem("Window/Task Manager")]
    public static void ShowWindow()
    {
        GetWindow<TaskManager>("Task Manager");
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        tasksProperty = serializedObject.FindProperty("tasks");
        notesProperty = serializedObject.FindProperty("notes");

        textAreaStyle = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true,
            stretchHeight = true
        };
    }

    private void OnGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(tasksProperty, true);
        EditorGUILayout.PropertyField(notesProperty, true);

        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(10);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var field in fields)
        {
            GUILayout.Label(field.title, EditorStyles.boldLabel);

            foreach (var shortcut in field.shortcuts)
            {
                GUILayout.Label("- " + shortcut);
            }

            GUILayout.Space(10);
        }

        GUILayout.EndScrollView();
    }

    [System.Serializable]
    public class TaskField
    {
        public string title;
        public string[] shortcuts;
    }

    public string[] tasks;
    public string[] notes;
}
