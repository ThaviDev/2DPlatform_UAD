using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _sprites = new List<Sprite>();
    private SpriteRenderer _spriteRenderer;
    private int _spriteIndex;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteIndex = Random.Range(0,_sprites.Count);
        _spriteRenderer.sprite = _sprites[_spriteIndex];
    }
}
