using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    bool _isHorizontal = false;
    [SerializeField]
    float _speed = 5;
    [SerializeField]
    float _limit = 5;
    [SerializeField]
    float _waitTime = 1;

    float _currentWaitTime;
    bool _isWaiting = false;
    float _waitCooldown = 0f;
    Vector3 _startPos;
    Vector3 _initialDir;
    float _elapseTime;

    void Start()
    {
        _currentWaitTime = _waitTime;
        _startPos = transform.position;
        _elapseTime = 0;
    }

    void Update()
    {
        if (_isHorizontal)
        {
            _initialDir = Vector3.left;
            VerticalMovement();
        } else
        {
            _initialDir = Vector3.up;
            HorizontalMovement();
        }

        if (_isWaiting == true)
        {
            _waitCooldown = 1f - _speed / 10;
            _currentWaitTime -= Time.deltaTime;
            if (_currentWaitTime <= 0f)
            {
                _isWaiting = false;
            }
        }
        if (_isWaiting == false)
        {
            _currentWaitTime = _waitTime;
            _elapseTime += Time.deltaTime;
            transform.position = _startPos + (_initialDir * Mathf.Sin(_elapseTime * _speed) * _limit);
            if (_waitCooldown > 0)
            {
                _waitCooldown -= Time.deltaTime;
            }
        }
        //print("" + _waitCooldown);
        //print("Limite:"+_startPos + _limit);
        //print("Posision actual:"+_startPos + transform.position.x);
        /*
        if (_dirRight == true)
        {
            transform.position += new Vector3(_speed, 0f, 0f) * Time.deltaTime;
            if (transform.position.x >= _limitRight + _startPos.x)
            {
                _dirRight = false;
            }
        }
        if (_dirRight == false) {
            transform.position += new Vector3(-_speed, 0f, 0f) * Time.deltaTime;
            if (transform.position.x <= _limitLeft + _startPos.x)
            {
                _dirRight = true;
            }
        }
        */
    }
    void HorizontalMovement()
    {
        if (transform.position.x >= _startPos.x + _limit - 0.001f && _waitCooldown <= 0f ||
            transform.position.x <= _startPos.x - _limit + 0.001f && _waitCooldown <= 0f)
        {
            _isWaiting = true;
        }

    }
    void VerticalMovement()
    {
        if (transform.position.y >= _startPos.y + _limit - 0.001f && _waitCooldown <= 0f ||
            transform.position.y <= _startPos.y - _limit + 0.001f && _waitCooldown <= 0f)
        {
            _isWaiting = true;
        }
    }
}
