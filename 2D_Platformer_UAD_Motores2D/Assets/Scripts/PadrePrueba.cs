using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadrePrueba : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _sprites = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite EscojerApariencia()
    {
        int _spriteIndex;
        _spriteIndex = Random.Range(0, _sprites.Count);
        return _sprites[_spriteIndex];
    }
}
