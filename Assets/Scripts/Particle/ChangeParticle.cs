using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeParticle : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem[] _particles;

    void Start()
    {
        ControlParticleObject();
    }

    #region Private Methods
    private void ControlParticleObject()
    {
        switch (SceneManager.loadedSceneCount)
        {
            case 1:
                CheckArray(0);
                break;
            case 2:
                CheckArray(1);
                break;
            default:
                break;
        }
    }
    private void CheckArray(int objectIndex)
    {
        for(int i = 0; i < _particles.Length; i++)
        {
            if (_particles[objectIndex] != _particles[i])
            {
                _particles[i].gameObject.SetActive(false);
            }
            else
            {
                _particles[objectIndex].gameObject.SetActive(true);
            }
        }
    }
    #endregion
}
