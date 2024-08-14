using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager _instance;

    [SerializeField] private AudioSource _audioSourceObj;

    public static SFXManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Buscar una instancia existente en la escena.
                _instance = FindObjectOfType<SFXManager>();

                if (_instance == null)
                {
                    // Crear un nuevo GameObject con el script adjunto si no se encuentra ninguna instancia.
                    GameObject singletonObject = new GameObject("SFXManager");
                    _instance = singletonObject.AddComponent<SFXManager>();

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

    public void PlaySFXClip(AudioClip _clip, float _volume)
    {
        AudioSource _audioSource = Instantiate(_audioSourceObj,this.gameObject.transform.position,Quaternion.identity);
        _audioSource.clip = _clip;
        _audioSource.volume = _volume;
        //audioSource.outputAudioMixerGroup
        _audioSource.Play();

        float _clipLenght = _audioSource.clip.length;
        Destroy(_audioSource.gameObject,_clipLenght);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
