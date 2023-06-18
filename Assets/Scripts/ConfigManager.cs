using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public float mouseSensibility;
    public float soundVolume = 1f;
    private CameraController CC;
    // Start is called before the first frame update
    void Start()
    {
        mouseSensibility = CC.sensitivy;
    }

    // Update is called once per frame
    public void UpdateMouseSensitivity(float newValue)
    {
        mouseSensibility = newValue;
    }

    public void UpdateSoundVolume(float newValue)
    {
        soundVolume = newValue;
        //Apply new vol of sound of audio of game
        //Audio.Listener.volume = soundVolume;
    }


}
