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
    public enum myAnimations { Idle, Moving, Dying, Attack1, Jump, Run, Dash };
    [SerializeField]
    public myAnimations _animationsID;
    public myAnimations _prevAnimation; // No tocar esta variable fuera del script
    [SerializeField]
    SpriteRenderer _spriteRen;
    [SerializeField]
    PlayerMovement _player;
    bool _isPlaying;



    int _currentAnim = 0;
    int _currentFrame = 0;
    float _currentTime = 0;

    [SerializeField] List<float> _timePerFrame = new List<float>();
    [SerializeField] List<bool> _canLoop = new List<bool>();
    [SerializeField] List<bool> _canExit = new List<bool>();
    [SerializeField] List<int> _animPriority = new List<int>();

    bool _isOnPermaAnim;


    private void Start()
    {
        UpdateAnimationBeingPlayed();
    }
    private void Update()
    {
        if (_prevAnimation != _animationsID && _canExit[_currentAnim] == true)
        {
            _isPlaying = true;
            ChangeAnim();
        }
        {
            _isOnPermaAnim = true;
        }

        _currentTime += Time.deltaTime;
        if (_currentTime >= _timePerFrame[_currentAnim])
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
                    _isPlaying = false;
                }
                if (_isOnPermaAnim == true && _canLoop[_currentAnim])
                {
                    ChangeAnim();
                }
            }
        }
        if (_isPlaying)
        {
            _spriteRen.sprite = _anims[_currentAnim]._frames[_currentFrame];
        }
    }

    private void ChangeAnim()
    {
        _prevAnimation = _animationsID;
        UpdateAnimationBeingPlayed();
        _isOnPermaAnim = false;
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

    public void FlipSprite(bool fliped)
    {
        if (fliped)
        {
            _spriteRen.flipX = true;
        }
        else {
            _spriteRen.flipX = false;
        }
    }
}
