using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Autosave : EditorWindow
{
    private bool autosaveEnabled = true;
    private double autosaveInterval = 300.0; // Tempo em segundos (padrão: 5 minutos)
    private double lastSaveTime;

    [MenuItem("Window/Autosave")]
    public static void ShowWindow()
    {
        GetWindow<Autosave>("Autosave");
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange stateChange)
    {
        if (stateChange == PlayModeStateChange.ExitingEditMode)
        {
            SaveScenes();
        }
    }

    private void OnInspectorUpdate()
    {
        if (autosaveEnabled && EditorApplication.timeSinceStartup - lastSaveTime > autosaveInterval)
        {
            SaveScenes();
            lastSaveTime = EditorApplication.timeSinceStartup;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Configurações de Autosave", EditorStyles.boldLabel);

        autosaveEnabled = EditorGUILayout.Toggle("Autosave Habilitado", autosaveEnabled);
        autosaveInterval = EditorGUILayout.DoubleField("Intervalo de Autosave (segundos)", autosaveInterval);

        if (GUILayout.Button("Salvar Agora"))
        {
            SaveScenes();
            lastSaveTime = EditorApplication.timeSinceStartup;
        }
    }

    private void SaveScenes()
    {
        int sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.isDirty)
            {
                EditorSceneManager.SaveScene(scene);
            }
        }

        Debug.Log("Cenas salvas automaticamente.");
    }
}
