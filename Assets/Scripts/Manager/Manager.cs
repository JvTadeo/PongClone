using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager _singleton;

    #region Unity Methods
    private void Awake()
    {
        if(_singleton == null)
        {
            _singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Public Methods
    public void ChangeSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }    
    #endregion
}
