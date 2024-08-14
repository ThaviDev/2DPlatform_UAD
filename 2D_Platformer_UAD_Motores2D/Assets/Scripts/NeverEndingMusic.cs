using UnityEngine;

public class NeverEndingMusic : MonoBehaviour
{
    private static NeverEndingMusic _instance;

    public static NeverEndingMusic Instance
    {
        get
        {
            if (_instance == null)
            {
                // Buscar una instancia existente en la escena.
                _instance = FindObjectOfType<NeverEndingMusic>();

                if (_instance == null)
                {
                    // Crear un nuevo GameObject con el script adjunto si no se encuentra ninguna instancia.
                    GameObject singletonObject = new GameObject("NeverEndingMusic");
                    _instance = singletonObject.AddComponent<NeverEndingMusic>();

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
    }
}
