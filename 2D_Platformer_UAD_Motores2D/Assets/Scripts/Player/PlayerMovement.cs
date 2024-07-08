using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] float _jumpForce = 10;
    [SerializeField] float _doubleJumpPower = 1.5f;
    [SerializeField] int _currentHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] int _startingHealth = 3;
    [SerializeField] bool _isDead;
    [SerializeField] bool _canDoubleJump;
    [SerializeField] bool _isGrounded;
    int _movementXDirection;

    [SerializeField] Rigidbody2D _rb;

    [SerializeField] FruitColector _fruitManager;
    [SerializeField] PlayerAnimation _playerAnim;
    [SerializeField] SimpleInputManager _inputManager;

    /*
    private void Awake()
    {
        _inputManager = GetComponent<SimpleInputManager>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _fruitManager = GetComponent<PlayerFruitManager>();
    } */

    void Start()
    {
        RestartStats();
    }

    void Update()
    {
        var inputRight = _inputManager.InputRightButton;
        var inputLeft = _inputManager.InputLeftButton;
        var inputJump = _inputManager.InputJumpButton;
        var inputAttack = _inputManager.InputAttack1Button;
        var inputDash = _inputManager.InputDashBtn;

        if (inputRight)
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Moving;
            _playerAnim._isFliped = false;
            _movementXDirection = 1;
        } else if (inputLeft)
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Moving;
            _playerAnim._isFliped = true;
            _movementXDirection = -1;
        } else
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Idle;
            _movementXDirection = 0;
        }

        if (inputJump)
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Jump;
            var currentJumpPower = _jumpForce;
            if (_isGrounded)
            {
            }
            else if (_canDoubleJump) {
                currentJumpPower = _jumpForce * _doubleJumpPower;
                _canDoubleJump = false;
            } else
            {
                return;
            }
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(new Vector2(0f, currentJumpPower), ForceMode2D.Impulse);
        }


    }

    private void FixedUpdate()
    {
        float __speedX = _movementXDirection * _moveSpeed;
        _rb.velocity = new Vector2(__speedX, _rb.velocity.y);
    }

    private void PlayerDied()
    {

    }

    public void HasColectedFruit()
    {
        _fruitManager.AddFruit();
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0) {
            PlayerDied();
        }
    }

    public void RestartStats()
    {
        _currentHealth = _startingHealth;
        _isDead = false;
    }
    public void Grounded()
    {
        _isGrounded = true;
        _canDoubleJump = true;
    }
    public void Ungrounded()
    {
        _isGrounded = false;
    }
}
