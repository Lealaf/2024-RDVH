
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerVolumeSetter : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        UpdateSliderValues();
    }

    void UpdateSliderValues()
    {
        masterSlider.value = AudioManager.Instance.GetMasterLevel();
        musicSlider.value = AudioManager.Instance.GetMusicLevel();
        sfxSlider.value = AudioManager.Instance.GetSFXLevel();
    }

    public void SetMasterLevel(float sliderValue)
    {
        AudioManager.Instance.SetMasterLevel(sliderValue);
    }

    public void SetSFXLevel(float sliderValue)
    {
        AudioManager.Instance.SetSFXLevel(sliderValue);
    }

    public void SetMusicLevel(float sliderValue)
    {
        AudioManager.Instance.SetMusicLevel(sliderValue);
    }

    void OnEnable()
    {
        UpdateSliderValues();
    }
}