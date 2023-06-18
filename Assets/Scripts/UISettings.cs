using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public Scrollbar mouseSensitivityScrollbar;
    public Scrollbar soundVolumeScrollbar;
    public ConfigManager configManager;
    private void Start()
    {
        configManager = GameObject.FindObjectOfType<ConfigManager>();
        mouseSensitivityScrollbar.onValueChanged.AddListener(configManager.UpdateMouseSensitivity);
        soundVolumeScrollbar.onValueChanged.AddListener(configManager.UpdateSoundVolume);
    }
}
