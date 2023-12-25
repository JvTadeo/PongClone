using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class ChangeVideoSettings : MonoBehaviour
{
    [Header("Dropdown")]
    [SerializeField] private TMPro.TMP_Dropdown _resolutionOptions;
    [SerializeField] private TMPro.TMP_Dropdown _graphicsOptions;
    [Header("Checkbox")]
    [SerializeField] private Toggle _fullScreenToggle;
    [SerializeField] private Toggle _vSyncToogle;
    [Header("Slider")]
    [SerializeField] private Slider _masterVolume;
    [SerializeField] private Slider _sfxVolume;
    [SerializeField] private Slider _uiVolume;
    [SerializeField] private Slider _musicVolume;
    [Header("Mixers/Groups")]    
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private AudioMixerGroup _sfxGroupVolume;
    [SerializeField] private AudioMixerGroup _uiGroupVolume;
    [SerializeField] private AudioMixerGroup _musicGroupVolume;

    public SaveSettings _saveSettings;
    private Resolution[] _resolutions;
    #region Unity Methods    
    private void Awake()
    {
        _resolutions = Screen.resolutions;
        LoadAllSettings();
        GetAllResolutions();
    }
    #endregion

    #region Public Methods
    public void SetFullscreen(bool fullscreen)
    {
        _saveSettings._isFullscreen = fullscreen;
        SaveManager.SaveSettings(_saveSettings);
        Screen.fullScreen = _saveSettings._isFullscreen; 
    }
    public void SetVsync(bool setVsync)
    {
        _saveSettings._vSync = setVsync;
        SaveManager.SaveSettings(_saveSettings);
        if (_saveSettings._vSync) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;        
    }
    public void SetQuality(int index)
    {
        _saveSettings._indexQuality = index;
        SaveManager.SaveSettings(_saveSettings);
        QualitySettings.SetQualityLevel(_saveSettings._indexQuality);        
    }
    public void SetResolution(int index)
    {
        _saveSettings._indexResolution = index;
        SaveManager.SaveSettings(_saveSettings);
        Resolution resolution = _resolutions[_saveSettings._indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetMasterVolume(float volume)
    {
        _saveSettings._masterVolume = volume;
        SaveManager.SaveSettings(_saveSettings);
        _masterMixer.SetFloat("masterVolume", _saveSettings._masterVolume);
    }
    public void SetSfxVolume(float volume)
    {
        _saveSettings._sfxVolume = volume;
        SaveManager.SaveSettings(_saveSettings);
        _masterMixer.SetFloat("sfxVolume", _saveSettings._sfxVolume);
    }
    public void SetUIVolume(float volume)
    {
        _saveSettings._uiVolume = volume;
        SaveManager.SaveSettings(_saveSettings);
        _masterMixer.SetFloat("uiVolume", _saveSettings._uiVolume);
    }
    public void SetMusicVolume(float volume)
    {
        _saveSettings._musicVolume = volume;
        SaveManager.SaveSettings(_saveSettings);
        _masterMixer.SetFloat("musicVolume", _saveSettings._musicVolume);        
    }
    #endregion

    #region Private Methods
    private void GetAllResolutions()
    {
        _resolutionOptions.ClearOptions();
        List<string> options = new List<string>();

        for(int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);
        }        

        _resolutionOptions.AddOptions(options);
        _resolutionOptions.value = _saveSettings._indexResolution;
        _resolutionOptions.RefreshShownValue();
    }
    private void LoadAllSettings()
    {
        _saveSettings = SaveManager.Load();
        Resolution resolution = _resolutions[_saveSettings._indexResolution];

        //Fullscreen
        Screen.fullScreen = _saveSettings._isFullscreen;
        _fullScreenToggle.isOn = _saveSettings._isFullscreen;
        //Quality
        QualitySettings.SetQualityLevel(_saveSettings._indexQuality);
        _graphicsOptions.value = _saveSettings._indexQuality;
        //Resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //VSync
        if (_saveSettings._vSync) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
        _vSyncToogle.isOn = _saveSettings._vSync;
        //VOLUME
        _masterMixer.SetFloat("masterVolume", _saveSettings._masterVolume);
        _masterMixer.SetFloat("sfxVolume", _saveSettings._sfxVolume);
        _masterMixer.SetFloat("uiVolume", _saveSettings._uiVolume);
        _masterMixer.SetFloat("musicVolume", _saveSettings._musicVolume);
        //Slider - Value
        _masterVolume.value = _saveSettings._masterVolume;
        _sfxVolume.value = _saveSettings._sfxVolume;
        _uiVolume.value = _saveSettings._uiVolume;
        _musicVolume.value = _saveSettings._musicVolume;
    }
    #endregion
}
