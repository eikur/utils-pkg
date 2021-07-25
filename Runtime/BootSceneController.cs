using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSceneController : MonoBehaviour
{
    [SerializeField] private SceneField _sceneToLoad;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadSceneAsync(_sceneToLoad.SceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // remove stuff visually or whatever :)
    }
}
