using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearPlatform : MonoBehaviour
{
    [SerializeField]
    float _destroyTime = 5;
    [SerializeField]
    float _apearTime = 5;
    [SerializeField]
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    Collider2D _collider;

    bool _canBeTouched = true;
    [SerializeField]
    float _timeToApear = 1;
    float _currentTimeToApear = 1;
    bool _inReapearProcess;

    void Start()
    {
        _currentTimeToApear = _timeToApear;
    }

    private void Update()
    {
        if (_inReapearProcess == true)
        {
            _currentTimeToApear -= Time.deltaTime;
        }
        if (_currentTimeToApear <= 0)
        {
            _inReapearProcess = false;
            StartCoroutine(PlatApear());
            _currentTimeToApear = _timeToApear;
        }
    }

    IEnumerator PlatDissapear()
    {
        float currentTime = 0;
        var color = _spriteRenderer.color;
        color.a = 1f;

        while (currentTime < _destroyTime)
        {
            currentTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, currentTime / _destroyTime);
            _spriteRenderer.color = color;
            yield return null;
            // yield return null Continua despues de un frame
        }
        _spriteRenderer.color = color;
        _collider.enabled = false;
        color.a = 0f;
    }

    IEnumerator PlatApear()
    {
        float currentTime = 0;
        var color = _spriteRenderer.color;
        color.a = 0f;
        _collider.enabled = true;

        while (currentTime < _apearTime)
        {
            currentTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, currentTime / _apearTime);
            _spriteRenderer.color = color;
            yield return null;
            // yield return null Continua despues de un frame
        }
        _spriteRenderer.color = color;
        color.a = 1f;
        _canBeTouched = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_canBeTouched == true)
        {
            StartCoroutine(PlatDissapear());
            _canBeTouched = false;
            _inReapearProcess = true;
        }
    }
}
