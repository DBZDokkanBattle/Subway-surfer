using UnityEngine;
using UnityEngine.UI;

public class SoundManagement : MonoBehaviour
{
    public Slider VolumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            LoadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 1);
            LoadVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume()
    {
        AudioListener.volume = VolumeSlider.value;
        SaveVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", VolumeSlider.value);
    }

    public void LoadVolume()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }
}
