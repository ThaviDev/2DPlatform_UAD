using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotor : MonoBehaviour
{
    public bool _dirIsRight;
    [SerializeField] float _selfDestructTime;
    [SerializeField] float _speed;
    float _dir;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, _selfDestructTime);
    }
    private void Update()
    {
        if (_dirIsRight)
        {
            _dir = 1;
        } else
        {
            _dir = -1;
        }
        float _speedAndDir = _speed * _dir;

        _rb.velocity = new Vector2(_speedAndDir, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Enemy = collision.GetComponent<EnemyMotor>();
        if (Enemy != null)
        {
            Enemy.GetDamaged();
            Destroy(this.gameObject);
        }
    }
}
