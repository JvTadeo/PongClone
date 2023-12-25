using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Mediator", menuName = "Core/Mediator")]
public class Mediator : ScriptableObject
{
    //Text
    public delegate void OnTextSetDelegateSideA(string value);
    public event OnTextSetDelegateSideA OnTextSetSideA;
    public delegate void OnTextSetDelegateSideB(string value);
    public event OnTextSetDelegateSideB OnTextSetSideB;
    public delegate void OnTextSetDelegateTimer(string value);
    public event OnTextSetDelegateTimer OnTextSetTimer;
    public delegate void OnTextSetDelegateWinner(string value, Color winnerColor);
    public event OnTextSetDelegateWinner OnTextSetWinner;
    //Event
    public delegate void OnValueChangeDelegateSideA();
    public event OnValueChangeDelegateSideA OnValueChangeSideA;
    public delegate void OnValueChangeDelegateSideB();
    public event OnValueChangeDelegateSideB OnValueChangeSideB;
    public delegate void OnChangeDelegatePanels();
    public event OnChangeDelegatePanels OnChangePanels;
    public delegate void OnPauseSetDelegate();
    public event OnPauseSetDelegate OnPauseSet;
    public delegate void PlayWinnerSound();
    public event PlayWinnerSound OnPlayWinnerSound;
    public delegate void PlayDestroySound();
    public event PlayDestroySound OnPlayDestroySound;
    public void SetTextSideA(string text) { OnTextSetSideA?.Invoke(text); }
    public void SetTextSideB(string text) { OnTextSetSideB?.Invoke(text); }
    public void SetTimer(string text) { OnTextSetTimer?.Invoke(text); }
    public void SetWinner(string text, Color winnerColor) { OnTextSetWinner?.Invoke(text, winnerColor); }
    public void ChangePanels() { OnChangePanels?.Invoke(); }
    public void ChangeValueSideA() { OnValueChangeSideA?.Invoke(); }
    public void ChangeValueSideB() { OnValueChangeSideB?.Invoke(); }
    public void PauseSet() { OnPauseSet?.Invoke(); }
    public void PlayWinner() { OnPlayWinnerSound?.Invoke(); }
    public void PlayDestroy() {  OnPlayDestroySound?.Invoke(); }
}

