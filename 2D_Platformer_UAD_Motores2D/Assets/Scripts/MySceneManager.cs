using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private static MySceneManager _instance;

    public static MySceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Buscar una instancia existente en la escena.
                _instance = FindObjectOfType<MySceneManager>();

                if (_instance == null)
                {
                    // Crear un nuevo GameObject con el script adjunto si no se encuentra ninguna instancia.
                    GameObject singletonObject = new GameObject("MySceneManager");
                    _instance = singletonObject.AddComponent<MySceneManager>();

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
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
