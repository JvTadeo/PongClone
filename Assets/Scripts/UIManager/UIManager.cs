using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Mediator")]
    [SerializeField] private Mediator _mediator;
    [Header("Text Reference")]
    [SerializeField] private TMPro.TextMeshProUGUI _pointSideA;
    [SerializeField] private TMPro.TextMeshProUGUI _pointSideB;
    [SerializeField] private TMPro.TextMeshProUGUI _timer;
    [SerializeField] private TMPro.TextMeshProUGUI _textoToPlaceWinner;
    [Header("Image Reference")]
    [SerializeField] private Image _panelWinner;
    [Header("UI's")]
    [SerializeField] private GameObject _timerOut;
    [SerializeField] private GameObject _winObject;
    [SerializeField] private Animator _anim;

    private Coroutine _changePanels;
    #region Unity Methods
    private void Start()
    {
        _mediator.OnTextSetSideA += SideAPoints;
        _mediator.OnTextSetSideB += SideBPoints;
        _mediator.OnTextSetTimer += ControlTimer;
        _mediator.OnTextSetWinner += SetWinner;
        _mediator.OnChangePanels += ChangePanels;
    }

    private void ChangePanels()
    {                
        if(_changePanels == null )
        {
            _changePanels = StartCoroutine(ChangePanel());
        }
    }

    private void SetWinner(string value, Color winnerColor)
    {
        _textoToPlaceWinner.text = value;
        _panelWinner.color = winnerColor;
    }

    #endregion

    #region Events
    private void SideAPoints(string value)
    {
        _pointSideA.text = value;        
    }
    private void SideBPoints(string value)
    {
        _pointSideB.text = value;        
    }
    private void ControlTimer(string value)
    {
        _timer.text = value;
    }    
    #endregion
    #region Private Methods
    private IEnumerator ChangePanel()
    {
        // Ativa o primeiro objeto e desativa o segundo
        _timerOut.SetActive(true);
        _winObject.SetActive(false);

        // Aguarda o intervalo
        yield return new WaitForSeconds(2f);

        // Desativa o primeiro objeto e ativa o segundo
        _timerOut.SetActive(false);
        _winObject.SetActive(true);
        _mediator.PlayWinner();
        // Aguarda o intervalo
        yield return new WaitForSeconds(2f);
        _anim.SetTrigger("ChangeScene");
        _winObject.SetActive(false);
        Manager._singleton.ChangeSceneWithName("MainMenu");
    }
    #endregion
}
