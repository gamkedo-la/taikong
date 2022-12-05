using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;

    private UIDocument uiDocument;
    private Toggle muteCheckbox;
    private Slider volumeSlider;
    private Slider sfxSlider;
    private Slider aimSlider;
    
    // Start doesnt seem to allow this to work when shown a second time
    public void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        
        var rootElement = uiDocument.rootVisualElement;

        muteCheckbox = rootElement.Q<Toggle>("MuteCheckbox");
        volumeSlider = rootElement.Q<Slider>("VolumeSlider");
        sfxSlider = rootElement.Q<Slider>("SFXSlider");
        aimSlider = rootElement.Q<Slider>("ControlSensitivity");
        
        var closeButton = rootElement.Q<Button>("CloseButton");
        closeButton.clickable.clicked += OnCloseButtonClicked;
        
        var saveButton = rootElement.Q<Button>("SaveButton");
        saveButton.clickable.clicked += OnSaveButtonClicked;

        SetValuesFromPreferences();
    }

    private void SetValuesFromPreferences()
    {
        muteCheckbox.value = PlayerPrefs.GetFloat("MASTER_VOLUME") == -80.0f ? true : false;
        volumeSlider.value = Mathf.Pow(10, (PlayerPrefs.GetFloat("MUSIC_VOLUME") / 20f));
        sfxSlider.value = Mathf.Pow(10, (PlayerPrefs.GetFloat("SFX_VOLUME") / 20f));
        aimSlider.value = PlayerPrefs.GetFloat("SENSITIVITY");
    }
    
    // private void OnMuteCheckboxChanged(ChangeEvent<bool> evt)
    // {
    //     throw new NotImplementedException();
    // }
    //
    //
    // private void OnVolumeSliderChanged(ChangeEvent<int> evt)
    // {
    //     throw new System.NotImplementedException();
    // }

    private void OnSaveButtonClicked()
    {
        Debug.Log(volumeSlider.value);
        SaveSettingsToPreferences();
        HideSettings();
    }

    private void SaveSettingsToPreferences()
    {
        // AudioSettingsManager.SaveSettings(muteCheckbox.value, volumeSlider.value, sfxSlider.value);
        PlayerPrefs.SetFloat("MASTER_VOLUME", muteCheckbox.value ? -80.0f : 0.0f);
        PlayerPrefs.SetFloat("MUSIC_VOLUME", 20f * Mathf.Log10(volumeSlider.value));
        PlayerPrefs.SetFloat("SFX_VOLUME", 20f * Mathf.Log10(sfxSlider.value));
        PlayerPrefs.SetFloat("SENSITIVITY", aimSlider.value);

        masterMixer.SetFloat("MasterVol", muteCheckbox.value ? -80.0f : 0.0f);
        masterMixer.SetFloat("MusicVol", 20f * Mathf.Log10(volumeSlider.value));
        masterMixer.SetFloat("SFXVol", 20f * Mathf.Log10(sfxSlider.value));
    }

    private void OnCloseButtonClicked()
    {
        HideSettings();
    }

    private void HideSettings()
    {
        uiDocument.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // m_Button.clickable.clicked -= OnClicked;
        // m_Toggle.UnregisterValueChangedCallback(OnToggleValueChanged);
    }
}
