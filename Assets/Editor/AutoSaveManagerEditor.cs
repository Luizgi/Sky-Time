using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class AutoSaveManager : EditorWindow
{
    private static bool isSavingEnabled = true;
    [SerializeField] private static float autoSaveInterval = 300f; // Intervalo em segundos (5 minutos)

    static AutoSaveManager()
    {
        EditorApplication.playModeStateChanged += AutoSaveOnPlayModeChange;
    }

    private static void AutoSaveOnPlayModeChange(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            SaveScene();
        }
    }

    [MenuItem("AutoSave/Enable Auto Save")]
    private static void EnableAutoSave()
    {
        isSavingEnabled = true;
        EditorPrefs.SetBool("AutoSaveEnabled", isSavingEnabled);
        Debug.Log("Auto Save enabled");
    }

    [MenuItem("AutoSave/Disable Auto Save")]
    private static void DisableAutoSave()
    {
        isSavingEnabled = false;
        EditorPrefs.SetBool("AutoSaveEnabled", isSavingEnabled);
        Debug.Log("Auto Save disabled");
    }

    [MenuItem("AutoSave/Set Auto Save Interval")]
    private static void SetAutoSaveInterval()
    {
        autoSaveInterval = EditorPrefs.GetFloat("AutoSaveInterval", 300f);
        autoSaveInterval = EditorGUILayout.FloatField("Auto Save Interval (seconds)", autoSaveInterval);
        EditorPrefs.SetFloat("AutoSaveInterval", autoSaveInterval);
    }

    [MenuItem("AutoSave/Save Scene")]
    private static void SaveScene()
    {
        if (isSavingEnabled)
        {
            EditorSceneManager.SaveOpenScenes();
            Debug.Log("Scene saved");
        }
    }

    [MenuItem("AutoSave/Save All Scenes")]
    private static void SaveAllScenes()
    {
        if (isSavingEnabled)
        {
            EditorSceneManager.SaveOpenScenes();
            Debug.Log("All scenes saved");
        }
    }

    [MenuItem("AutoSave/Auto Save Now")]
    private static void AutoSaveNow()
    {
        if (isSavingEnabled)
        {
            SaveScene();
        }
    }

    private static void AutoSaveTick()
    {
        if (isSavingEnabled)
        {
            SaveScene();
            EditorApplication.delayCall += AutoSaveTick;
        }
    }

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        isSavingEnabled = EditorPrefs.GetBool("AutoSaveEnabled", true);
        autoSaveInterval = EditorPrefs.GetFloat("AutoSaveInterval", 300f);

        if (isSavingEnabled)
        {
            EditorApplication.delayCall += AutoSaveTick;
            Debug.Log("Auto Save initialized");
        }
    }
    [MenuItem("AutoSave/Open Auto Save Manager")]
    private static void OpenAutoSaveManager()
    {
        AutoSaveManager window = EditorWindow.GetWindow<AutoSaveManager>();
        window.Show();
    }
}
