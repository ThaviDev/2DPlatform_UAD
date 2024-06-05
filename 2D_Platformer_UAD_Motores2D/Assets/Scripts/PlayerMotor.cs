using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private float _gmoveSpeed = 3; // Movimiento General
    [SerializeField]
    private float _jumpForce = 10; // Fuerza de salto
    private float _currentSpeed; // Movimiento Presente
    private float _axisX;// El valor X del jugador
    private Rigidbody2D rb;
    private SpriteRenderer _sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentSpeed = _gmoveSpeed;
        _sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }
    }

    private void FixedUpdate()
    {
        //Axis de movimiento
        _axisX = Input.GetAxisRaw("Horizontal");
        float __speedX = _axisX * _currentSpeed;
        rb.velocity = new Vector2(__speedX, rb.velocity.y);

        if (_axisX < 0)
        {
            _sprite.flipX = true;
        }
        if (_axisX > 0)
        {
            _sprite.flipX = false;
        }

        //Vector3 mov = new Vector3(_axisX, 0f, 0f);
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, movementSpeedIG * Time.deltaTime );
        //rb.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y) + mov, _currentSpeed * Time.deltaTime));

        //rb.velocity = new Vector3(_axisX * _currentSpeed, 0f, 0f);
        //rb.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y) + mov, _currentSpeed * Time.deltaTime));
    }
}
