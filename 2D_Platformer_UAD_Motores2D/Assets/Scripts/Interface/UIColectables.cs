using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIColectables : MonoBehaviour
{
    [SerializeField]
    TMP_Text _fruitText;
    [SerializeField]
    TMP_Text _livesText;

    public int _fruitCount;
    public int _livesCount;
    void Update()
    {
        _fruitText.text = _fruitCount.ToString();
        _livesText.text = _livesCount.ToString();
    }
}
