using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject _mainMenu;
    public GameObject _options;
    public AudioClip _clickBtnSFX;
    public void StartGame()
    {
        SFXManager.Instance.PlaySFXClip(_clickBtnSFX, 1);
        MySceneManager.Instance.LoadScene("LevelScene");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void GotoOptions()
    {
        SFXManager.Instance.PlaySFXClip(_clickBtnSFX, 1);
        _mainMenu.SetActive(false);
        _options.SetActive(true);
    }

    public void GotoMainMenu()
    {
        SFXManager.Instance.PlaySFXClip(_clickBtnSFX, 1);
        _mainMenu.SetActive(true);
        _options.SetActive(false);
    }
}
