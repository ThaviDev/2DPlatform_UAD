using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpritesInAnimation
{
    public List<Sprite> _frames;
}
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    List<SpriteAnim> _anims;
    public enum myAnimations {Idle,Moving,Dying,Attack1,Jump,Run,Dash};
    [SerializeField]
    public myAnimations _animationsID;
    public myAnimations _prevAnimation; // No tocar esta variable fuera del script
    [SerializeField]
    SpriteRenderer _spriteRen;
    [SerializeField]
    PlayerMovement _player;

    [SerializeField]
    float _timePerFrame = 1;

    int _currentAnim = 0;
    int _currentFrame = 0;
    float _currentTime = 0;

    public bool _isFliped;
    [SerializeField] List<bool> _canLoop = new List<bool>();
    [SerializeField] List<bool> _canExit = new List<bool>();

    bool _isOnPermaAnim;


    private void Start()
    {
        UpdateAnimationBeingPlayed();
    }
    private void Update()
    {
        if (_prevAnimation != _animationsID && _canExit[_currentAnim] == true)
        {
            _prevAnimation = _animationsID;
            UpdateAnimationBeingPlayed();
            _isOnPermaAnim = false;
        }
        {
            _isOnPermaAnim = true;
        }

        _currentTime += Time.deltaTime;
        if (_currentTime >= _timePerFrame)
        {
            _currentTime = 0;
            _currentFrame++;
            if (_currentFrame >= _anims[_currentAnim]._frames.Count)
            {
                if (_canLoop[_currentAnim])
                {
                    _currentFrame = 0;
                } else
                {
                    _currentFrame = _anims[_currentAnim]._frames.Count;
                }
                if (_isOnPermaAnim == true && _canLoop[_currentAnim])
                {
                    _prevAnimation = _animationsID;
                    UpdateAnimationBeingPlayed();
                    _isOnPermaAnim = false;
                }
            }
        }
        _spriteRen.sprite = _anims[_currentAnim]._frames[_currentFrame];

        if (!_isFliped)
        {
            _spriteRen.flipX = false;
        } else
        {
            _spriteRen.flipX = true;
        }
    }
    private void UpdateAnimationBeingPlayed()
    {
        switch (_animationsID)
        {
            case myAnimations.Idle:
                _currentAnim = 0;
                break;
            case myAnimations.Moving:
                _currentAnim = 1;
                break;
            case myAnimations.Dying:
                _currentAnim = 2;
                break; 
            case myAnimations.Attack1:
                _currentAnim = 3;
                break;
            case myAnimations.Jump:
                _currentAnim = 4;
                break;
            case myAnimations.Run:
                _currentAnim = 5;
                break;
            case myAnimations.Dash:
                _currentAnim = 6;
                break;
        }
        if (_currentAnim >= _anims.Count)
        {
            Debug.LogWarning("La animacion no es disponible, se aplico la animacion 0");
            _currentAnim = 0;
        }
        _currentFrame = 0;
    }
}
