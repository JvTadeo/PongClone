using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private Menu[] _menus;
    [Header("Panel")]
    [SerializeField] private Menu[] _panels;
    #region Unity Methods
    #endregion

    #region Public Methods
    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < _menus.Length; i++)
        {
            if(menuName == _menus[i]._menuName)
            {
                _menus[i].gameObject.SetActive(true);
            }
            else
            {
                _menus[i].gameObject.SetActive(false);
            }
        }
    }
    public void OpenPanel(string panelName)
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            if (panelName == _panels[i]._menuName)
            {
                _panels[i].gameObject.SetActive(true);
            }
            else
            {
                _panels[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
