using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private int _sceneToChage;
    [SerializeField] private Animator _anim;
    private void Start()
    {
        ChangeSceneWithId(_sceneToChage);
    }
    public void ChangeSceneWithId(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        _anim.SetTrigger("ChangeScene");
        while(!operation.isDone)
        {
            yield return null;
        }
    }
}
