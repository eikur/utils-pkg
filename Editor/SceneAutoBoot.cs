#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneAutoBoot : EditorWindow
{
    static private bool _useBootScene;
    static private string _bootSceneAssetName;
    static private SceneAsset _sceneAsset;
    static public SceneField _sceneField;

    const string kUseBootSceneKey = "UseBootScene";
    const string kBootSceneNameKey = "BootSceneName";

    [MenuItem("Tools/Scene Auto Boot %h")]
    static void Init()
    {
        SceneAutoBoot window = (SceneAutoBoot)GetWindow(typeof(SceneAutoBoot));
        window.Show();
    }

    private void OnEnable()
    {
        Load();
        RefreshEditor();
    }

    void OnGUI()
    {
        _useBootScene = EditorGUILayout.BeginToggleGroup("Use Custom Boot Scene", _useBootScene);
        _sceneAsset = (SceneAsset)EditorGUILayout.ObjectField(_sceneAsset, typeof(SceneAsset), false, new GUILayoutOption []{ });


        EditorGUILayout.EndToggleGroup();
    }

    void OnLostFocus()
    {
        Save();
        RefreshEditor();
    }

    void OnDestroy()
    {
        Save();
        RefreshEditor();
    }

    [InitializeOnLoadMethod]
    public static void UseBaseSetupSceneFirst()
    {
        RefreshEditor();
    }

    static void RefreshEditor()
    {
        if (EditorPrefs.HasKey(kUseBootSceneKey) && EditorPrefs.GetBool(kUseBootSceneKey))
        {
            _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorPrefs.GetString(kBootSceneNameKey));
            EditorSceneManager.playModeStartScene = _sceneAsset;
        }
        else
        {
            EditorSceneManager.playModeStartScene = null;
        }
    }

    static void Save()
    {
        EditorPrefs.SetBool(kUseBootSceneKey, _useBootScene);

        string sceneNameToSave = _sceneAsset != null ? AssetDatabase.GetAssetPath(_sceneAsset) : "";
        EditorPrefs.SetString(kBootSceneNameKey, sceneNameToSave);
    }

    static void Load()
    {
        if (EditorPrefs.HasKey(kUseBootSceneKey))
        {
            _useBootScene = EditorPrefs.GetBool(kUseBootSceneKey);
        }
        if (EditorPrefs.HasKey(kBootSceneNameKey))
        {
            _bootSceneAssetName = EditorPrefs.GetString(kBootSceneNameKey);
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(_bootSceneAssetName);
            if (sceneAsset != null)
            {
                _sceneAsset = sceneAsset;
            }
        }
    }
}
#endif