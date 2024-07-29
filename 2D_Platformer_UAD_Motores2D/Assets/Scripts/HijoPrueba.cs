using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HijoPrueba : PadrePrueba
{
    [SerializeField]
    Sprite _mySprite;
    void Start()
    {
        _mySprite = base.EscojerApariencia();
        
    }

    void Update()
    {
        
    }
}
