using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [Header("Toggle")]
    //public Toggle fpsToggle30;
    //public Toggle fpsToggle60;
    //public Toggle fpsToggleunlimited;
    //public Toggle vSyncToggle;
    [Header("Slider")]
    public Slider renderDistanceSlider;
    [Header("Text")]
    public Text renderDistanceText;
    [Header("DropDown")]
    public Dropdown dd_resolution;
    public Dropdown dd_antiAlias;
    public Dropdown dd_shadow;
    [Header("Other")]
    public Light mainLight;
    public GameObject cameraObj;
    public float minSettingsValue = 10;
    public float maxSettingsValue = 100;
    public AudioMixer audioMixer;
    Resolution[] resolutions;

    private void Awake()
    {
        //fpsToggleunlimited.isOn = true;
        //fpsToggle30.isOn = false;
        //fpsToggle60.isOn = false;
        //Application.targetFrameRate = -1;
   
    }

    private void Start()
    {
        ResolutionSetup();
    }
    public void SetSoundVolume(float volume)
    {
        Debug.Log("Set Sound");
        audioMixer.SetFloat("SoundVolume",volume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    //public void FpsLimit30()
    //{
    //    if (Application.targetFrameRate != 30)
    //    {
    //        Application.targetFrameRate = 30;
    //    }
    //    fpsToggle60.isOn = false;
    //    fpsToggleunlimited.isOn = false;
    //}
    //public void FpsLimit60()
    //{
    //    if (Application.targetFrameRate != 60)
    //    {
    //        Application.targetFrameRate = 60;
    //    }
    //    fpsToggle30.isOn = false;
    //    fpsToggleunlimited.isOn = false;
    //}

    //public void FpsUnlimited()
    //{
    //    if (Application.targetFrameRate != -1) // -1 is unlimited default value
    //    {
    //        Application.targetFrameRate = -1;
    //    }
    //    fpsToggle30.isOn = false;
    //    fpsToggle60.isOn = false;
    //}

    //public void VsyncSwitch(bool ticked)
    //{
    //    if (ticked)
    //    {
    //        QualitySettings.vSyncCount = 1;
    //    }
    //    else
    //        QualitySettings.vSyncCount = 0;

    //    vSyncToggle.SetIsOnWithoutNotify(ticked);
    //}

    public void ResolutionSetup()
    {
        resolutions = Screen.resolutions;
        dd_resolution.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        dd_resolution.AddOptions(options);
        dd_resolution.value = currentResolutionIndex;
        dd_resolution.RefreshShownValue();
    }
}
