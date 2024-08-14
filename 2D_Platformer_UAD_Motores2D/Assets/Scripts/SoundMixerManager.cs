using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;

    // Claves para PlayerPrefs
    private const string MasterVolumeKey = "MasterVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const string MusicVolumeKey = "MusicVolume";

    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _sFX_slider;
    [SerializeField] Slider _musicSlider;


    private static SoundMixerManager _instance;

    public static SoundMixerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Buscar una instancia existente en la escena.
                _instance = FindObjectOfType<SoundMixerManager>();

                if (_instance == null)
                {
                    // Crear un nuevo GameObject con el script adjunto si no se encuentra ninguna instancia.
                    GameObject singletonObject = new GameObject("SoundMixerManager");
                    _instance = singletonObject.AddComponent<SoundMixerManager>();

                    // Opcional: Evitar que el objeto sea destruido al cambiar de escena.
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que el objeto sea destruido al cambiar de escena.
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destruir instancias adicionales si ya existe una instancia.
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        LoadVolumeSettings();
    }
    private void LoadVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 0);  
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0);        
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0);    

        _masterSlider.value = masterVolume;
        _sFX_slider.value = sfxVolume;
        _musicSlider.value = musicVolume;

        _audioMixer.SetFloat("MasterVolume", masterVolume);
        _audioMixer.SetFloat("SFXVolume", sfxVolume);
        _audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        PlayerPrefs.Save();

        _audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();

        _audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();

        _audioMixer.SetFloat("MusicVolume", volume);
    }
    /*
    public void SetMasterVolume(float _volume)
    {
        _audioMixer.SetFloat("MasterVolume", _volume);
    }
    public void SetSFXVolume(float _volume)
    {
        _audioMixer.SetFloat("SFXVolume", _volume);
    }
    public void SetMusicVolume(float _volume)
    {
        _audioMixer.SetFloat("MusicVolume", _volume);
    }*/
}
