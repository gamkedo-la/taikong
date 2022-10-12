using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsHandler : MonoBehaviour
{
    private UIDocument uiDocument;
    
    private Slider volumeSlider;
    private Toggle muteCheckbox;
    
    // Start doesnt seem to allow this to work when shown a second time
    public void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        
        var rootElement = uiDocument.rootVisualElement;

        volumeSlider = rootElement.Q<Slider>("VolumeSlider");
        // volumeSlider.RegisterValueChangedCallback(OnVolumeSliderChanged);
        
        muteCheckbox = rootElement.Q<Toggle>("MuteCheckbox");
        // muteCheckbox.RegisterValueChangedCallback(OnMuteCheckboxChanged);
        
        var closeButton = rootElement.Q<Button>("CloseButton");
        closeButton.clickable.clicked += OnCloseButtonClicked;
        
        var saveButton = rootElement.Q<Button>("SaveButton");
        saveButton.clickable.clicked += OnSaveButtonClicked;

        SetValuesFromPreferences();
    }

    private void SetValuesFromPreferences()
    {
        volumeSlider.value = AudioSettingsManager.GetVolume();
        muteCheckbox.value = AudioSettingsManager.GetMuted();
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
        SaveSettingsToPreferences();
        HideSettings();
    }

    private void SaveSettingsToPreferences()
    {
        AudioSettingsManager.SaveSettings(muteCheckbox.value, volumeSlider.value);
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
