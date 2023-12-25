using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private Database _db;
    [Header("Mediator")]
    [SerializeField] private Mediator _mediator;
    [Header("Rigibody")]
    [SerializeField] private Rigidbody _rb;
    [Header("AudioSource")]
    [SerializeField] private AudioSource _audioSource;
    [Header("Skins")]
    [SerializeField] private GameObject[] _skins;
    private bool _goToRight = false;
    private bool _isPause;
    private Vector3 _direction;    
    #region Unity Methods    
    private void Start()
    {        
        _mediator.OnPauseSet += PauseSet;
        ControlSkinObject();
    }    

    private void OnCollisionEnter(Collision collision)
    {
        _audioSource.PlayOneShot(_db.impactBall);
        if (collision.gameObject.layer == 3) HandleHeight();
        if (collision.gameObject.layer == 7) 
        { 
            HandleCustomMovementSpeed(collision.relativeVelocity.magnitude, collision.relativeVelocity.normalized.y, collision.transform.position.y); 
            //Debug.LogWarning("Y Do collider: " + collision.relativeVelocity.normalized); 
        }        

    }
    #endregion

    #region Events
    private void PauseSet()
    {
        PauseEverything();
    }
    #endregion

    #region Public Methods
    public void ChangeTarget(bool sideToGo)
    {
        _goToRight = sideToGo;
        if (_goToRight)
        {
            _direction = new Vector3(_db.ballSpeed, _rb.velocity.y);
        }
        else _direction = new Vector3((-1 * _db.ballSpeed), _rb.velocity.y);
        _rb.velocity = _direction;
    }    
    public void StartMovement()
    {        
        HandleDefaultMovement();
    }
    public void StartParticles(float axesX)
    {        
        SpawmParticles(axesX);
    }
    #endregion
    
    #region Private Methods
    private void HandleCustomMovementSpeed(float speed, float verticalSpeed, float yPositionObject)
    {
        _goToRight = !_goToRight;        
        if (_goToRight)
        {            
            if(verticalSpeed <= 0.1f && verticalSpeed >= -0.1f)
            {
                if(transform.position.y > yPositionObject)
                {
                    _direction = new Vector3(_db.ballSpeed, _db.ballSpeed /2);
                    //Debug.Log("Speed: " + speed + "| Rigibody: " + _rb.velocity.x + "| Total: " + (_rb.velocity.x + speed));                    
                }
                else
                {
                    _direction = new Vector3(_db.ballSpeed, (-1 * _db.ballSpeed) /2);
                    //Debug.Log("Speed: " + speed + "| Rigibody: " + _rb.velocity.x + "| Total: " + (_rb.velocity.x + speed));                    
                }
                _rb.velocity = _direction;
                return;
            }            
            _direction = new Vector3((_rb.velocity.x + speed), speed * verticalSpeed);
        }
        else
        {
            if (verticalSpeed <= 0.1f && verticalSpeed >= -0.1f)
            {
                if (transform.position.y > yPositionObject)
                {
                    _direction = new Vector3(-1 * _db.ballSpeed, _db.ballSpeed / 2);
                    //Debug.Log("Speed: " + speed + "| Rigibody: " + _rb.velocity.x + "| Total: " + (_rb.velocity.x + speed));                    
                }
                else
                { 
                    _direction = new Vector3(-1 * _db.ballSpeed, -1 * (_db.ballSpeed / 2));
                    //Debug.Log("Speed: " + speed + "| Rigibody: " + _rb.velocity.x + "| Total: " + (_rb.velocity.x + speed));                    
                }
                _rb.velocity = _direction;
                return;
            }            
            _direction = new Vector3(-1 * (_rb.velocity.x + speed), speed * verticalSpeed);            
        }
        _direction.x = Mathf.Clamp(_direction.x, _db.minBallSpeed, _db.maxBallSpeed);
        Debug.LogWarning(_direction.x);
        _rb.velocity = _direction;        
    }
    private void HandleDefaultMovement()
    {        
        _goToRight = !_goToRight;
        if (_goToRight)
        {
            _direction = new Vector3(_db.ballSpeed, _rb.velocity.y);
        }else _direction = new Vector3((-1 *_db.ballSpeed), _rb.velocity.y);
        _rb.velocity = _direction;
    }
    private void HandleHeight()
    {
        //Debug.LogWarning("Direction: " + _direction);
        //Debug.LogWarning("RB Velocity: " + _rb.velocity );
        if (transform.position.y < 1) _rb.velocity = new Vector3(_direction.x, 10);
        if (transform.position.y > 6) _rb.velocity = new Vector3(_direction.x, (-1 * 10));
    }    
    private void PauseEverything()
    {
        _direction = Vector3.zero;
        if (_rb != null) _rb.velocity = _direction;

    }
    private void SpawmParticles(float axesX)
    {
        Quaternion newRotation = Quaternion.identity;
        if(axesX < 0) newRotation.eulerAngles = new Vector3(0f, 90f, 0f);
        else newRotation.eulerAngles = new Vector3(0f, -90f, 0f);
        Instantiate(_db.ballDestroyPartycle, transform.position, newRotation);        
    }    
    private void ControlSkinObject()
    {
        switch (SceneManager.loadedSceneCount)
        {
            case 1:
                EnableSkin(0);
                break;
            case 2:
                EnableSkin(1);
                break;
            case 3:
                EnableSkin(2);
                break;
            default:
                break;
        }
    }
    private void EnableSkin(int index)
    {
        for(int i = 0; i < _skins.Length; i++)
        {
            if (_skins[index] != _skins[i] && _skins[index])
            {
                _skins[i].SetActive(false);
            }else _skins[index].SetActive(true);
        }
    }
    #endregion

}
