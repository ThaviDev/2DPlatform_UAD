using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpriteAnim
{
    public List<Sprite> _frames;
}
public class SimpleAnimation : MonoBehaviour
{
    [SerializeField]
    List<SpriteAnim> _anims;
    [SerializeField]
    float _timePerFrame = 1;

    int _currentAnim = 0;
    int _currentFrame = 0;
    float _currentTime = 0;

    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _anims[0]._frames[0];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentAnim++;
            if (_currentAnim >= _anims.Count)
            {
                _currentAnim = 0;
            }
            _currentFrame = 0;
        }
        _currentTime += Time.deltaTime;
        if (_currentTime >= _timePerFrame)
        {
            _currentTime = 0;
            _currentFrame++;
            if (_currentFrame >= _anims[_currentAnim]._frames.Count)
            {
                _currentFrame = 0;
            }
        }
        _spriteRenderer.sprite = _anims[_currentAnim]._frames[_currentFrame];
    }
}
