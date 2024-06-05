using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasablePlatform : MonoBehaviour
{
    public Transform _pTransform; // Player Transform
    [SerializeField]
    Collider2D _collider;
    [SerializeField]
    float _yOffset;

    void FixedUpdate()
    {
        if (_pTransform.transform.position.y - _yOffset < transform.position.y)
        {
            _collider.enabled = false;
        } else
        {
            _collider.enabled = true;
        }
    }
}
