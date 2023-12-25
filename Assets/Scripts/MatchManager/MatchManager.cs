using System.Collections;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [Header("Mediator")]
    [SerializeField] private Mediator _mediator;
    [Header("Database")]
    [SerializeField] private Database _db;
    [Header("Side A and B")]
    [SerializeField] private OnBallEnterSideA _sideA;
    [SerializeField] private OnBallEnterSideB _sideB;
    [Header("Transform")]
    [SerializeField] private Transform _pointSpawm;
    [SerializeField] private Vector3 _boxSize = new Vector3(1f, 22f, 0.5f);
    [Header("Audio Source")]
    [SerializeField] private AudioSource _audioSource;

    private float _timerNumber = 60;

    private string _pointSideA;
    private string _pointSideB;
    private string _timer;
    private int _pointA;
    private int _pointB;
    private bool _isPaused;
    #region Unity Methods
    private void Start()
    {
        SpawmBall();
        _mediator.OnValueChangeSideA += ValueChangeSideA;
        _mediator.OnValueChangeSideB += ValueChangeSideB;
        _mediator.OnPlayWinnerSound += WinnerSound;
        _mediator.OnPlayDestroySound += DestroyBallSound;
    }

    private void Update()
    {
        HandleStringSideA();
        HandleStringSideB();
        HandleTimer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_pointSpawm.position, _boxSize);
    }
    #endregion

    #region Events
    private void ValueChangeSideB()
    {
        if (_isPaused) return;
        StartCoroutine(SpawmBallInSideB());
    }
    private void ValueChangeSideA()
    {
        if (_isPaused) return;
        StartCoroutine(SpawmBallInSideA());
    }
    private void WinnerSound()
    {
        _audioSource.PlayOneShot(_db.winnerM1);
    }
    private void DestroyBallSound()
    {
        _audioSource.PlayOneShot(_db.destroyBallM1);
    }
    #endregion

    #region Private Methods
    private void SpawmBall()
    {
        StartCoroutine(AwaitToSpawnBall());
    }
    private void HandleStringSideA()
    {
        _pointSideA = _sideA.GetPoints().ToString();
        _pointA = _sideA.GetPoints();
        _mediator.SetTextSideA(_pointSideB);
    }
    private void HandleStringSideB()
    {
        _pointSideB = _sideB.GetPoints().ToString();
        _pointB = _sideB.GetPoints();
        _mediator.SetTextSideB(_pointSideA);
    }
    private void HandleTimer()
    {
        _timerNumber -= Time.deltaTime;
        float minutes = Mathf.Floor(_timerNumber / 60);
        int seconds = (int)Mathf.Round(_timerNumber % 60f);

        minutes = Mathf.Clamp(minutes, 0, 5);
        seconds = Mathf.Clamp(seconds, 0, 60);

        TimeOut(minutes, seconds);
        string secondsString = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();        

        _timer = minutes.ToString() + ":" + secondsString;
        _mediator.SetTimer(_timer);
    }
    private void SetTheWinner()
    {        
        if (_pointA > _pointB)
        {
            Color colorPlayer = Color.HSVToRGB(44/360f, 79/100f, 96/100f);
            _mediator.SetWinner("LEFT SIDE WIN", colorPlayer);
        }
        else if(_pointA < _pointB)
        {
            Color colorPlayer = Color.HSVToRGB(129/360f, 68/100f, 75/100f);
            _mediator.SetWinner("RIGHT SIDE WIN", colorPlayer);
        }
        else
        {
            Color colorPlayer = Color.HSVToRGB(129/360f, 0f, 38/100f);
            _mediator.SetWinner("A TIE", colorPlayer);
        }        
    }
    private void TimeOut(float minutes, int senconds)
    {            
        if(minutes == 0 && senconds < 4)
        {
            if (!_audioSource.isPlaying && !_isPaused)
            {
                _audioSource.PlayOneShot(_db.timerOutM1);                
            }
        }
        if(minutes == 0 && senconds == 0)
        {
            _mediator.PauseSet();
            _isPaused = true;
            SetTheWinner();        
            _mediator.ChangePanels();
        }
    }
    private Vector3 GetSpawmObject()
    {
        Vector3 positinObject = _pointSpawm.position;

        float minY = positinObject.y - _boxSize.y / 2;
        float maxY = positinObject.y + _boxSize.y / 2;
        float randomY = Random.Range(minY, maxY);
        return new Vector3(positinObject.x, randomY, positinObject.z);        
    }
    private IEnumerator AwaitToSpawnBall()
    {
        yield return new WaitForSeconds(_db.timeToSpawm);
        GameObject ballInstace = Instantiate(_db.ballObject, GetSpawmObject(), Quaternion.identity);
        ballInstace.GetComponent<BallController>().StartMovement();
    }
    private IEnumerator SpawmBallInSideB()
    {        
        yield return new WaitForSeconds(_db.timeToSpawm);
        GameObject newBall = Instantiate(_db.ballObject, GetSpawmObject(), Quaternion.identity);
        newBall.GetComponent<BallController>().ChangeTarget(true);
        newBall.GetComponent<BallController>().StartMovement();
    }
    private IEnumerator SpawmBallInSideA()
    {
        yield return new WaitForSeconds(_db.timeToSpawm);
        GameObject newBall = Instantiate(_db.ballObject, GetSpawmObject(), Quaternion.identity);
        newBall.GetComponent<BallController>().ChangeTarget(false);
        newBall.GetComponent<BallController>().StartMovement();
    }
    #endregion
}
