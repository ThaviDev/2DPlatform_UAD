using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float _moveSpeed = 3;
    [SerializeField] float _normalMoveSpeed = 3;
    [SerializeField] float _slowMoveSpeed = 1;
    [SerializeField] float _jumpForce = 10;
    [SerializeField] float _doubleJumpPower = 1.5f;
    [SerializeField] float _dashForce = 10;
    bool _isDashing = false;
    bool _canDash = true;
    float _startingDashCooldown;
    [SerializeField] float _dashCooldown = 1;
    [SerializeField] int _currentHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] int _startingHealth = 3;
    [SerializeField] bool _isDead;
    [SerializeField] bool _canDoubleJump;
    [SerializeField] bool _isGrounded;
    int _movementXDirection;
    int _lastMovementDirection;

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
        _startingDashCooldown = _dashCooldown;
        RestartStats();
    }

    void Update()
    {
        var inputRight = _inputManager.InputRightButton;
        var inputLeft = _inputManager.InputLeftButton;
        var inputJump = _inputManager.InputJumpButton;
        var inputAttack = _inputManager.InputAttack1Button;
        var inputDash = _inputManager.InputDashBtn;
        var inputSlowed = _inputManager.InputSlowedBtn;

        if (inputRight || inputLeft)
        {
            if (inputSlowed)
            {
                _moveSpeed = _slowMoveSpeed;
                _playerAnim._animationsID = PlayerAnimation.myAnimations.Moving;
            }
            else
            {
                _moveSpeed = _normalMoveSpeed;
                _playerAnim._animationsID = PlayerAnimation.myAnimations.Run;
            }
            if (inputRight)
            {
                _playerAnim.FlipSprite(false);
                _movementXDirection = 1;
                _lastMovementDirection = _movementXDirection;
            } 
            if (inputLeft)
            {
                _playerAnim.FlipSprite(true);
                _movementXDirection = -1;
                _lastMovementDirection = _movementXDirection;
            }
        }
        else
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Idle;
            _movementXDirection = 0;
        }
        /*
        if (inputRight)
        {
            if (inputSlowed)
            {
                print("Estoy siendo despacio");
                _moveSpeed = _slowMoveSpeed;
                _playerAnim._animationsID = PlayerAnimation.myAnimations.Moving;
            }
            else
            {
                print("Estoy moviendome normal");
                _moveSpeed = _normalMoveSpeed;
                _playerAnim._animationsID = PlayerAnimation.myAnimations.Run;
            }
            _playerAnim.FlipSprite(false);
            _movementXDirection = 1;
            _lastMovementDirection = _movementXDirection;
        } else if (inputLeft)
        {
            if (inputSlowed)
            {
                _moveSpeed = _slowMoveSpeed;
                _playerAnim._animationsID = PlayerAnimation.myAnimations.Moving;
            }
            else
            {
                _moveSpeed = _normalMoveSpeed;
                _playerAnim._animationsID = PlayerAnimation.myAnimations.Run;
            }
            _playerAnim.FlipSprite(true);
            _movementXDirection = -1;
            _lastMovementDirection = _movementXDirection;
        } else
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Idle;
            _movementXDirection = 0;
        }*/

        if (inputJump)
        {
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
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Jump;
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(new Vector2(0f, currentJumpPower), ForceMode2D.Impulse);
        }

        if (inputDash && _canDash)
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Dash;
            _canDash = false;
            _isDashing = true;
            if (_lastMovementDirection < 0 && _dashForce > 0 || _lastMovementDirection > 0 && _dashForce < 0)
            {
                _dashForce = -_dashForce;
            }
        }
        if (!_canDash)
        {
            _dashCooldown -= Time.deltaTime;
            if (_dashCooldown <= 0)
            {
                _dashCooldown = _startingDashCooldown;
                _canDash = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            _rb.AddForce(new Vector2(_dashForce, 0f), ForceMode2D.Impulse);
            _isDashing = false;
            //_rb.velocity = new Vector2(_movementXDirection * _dashForce, _rb.velocity.y);
        } else if (_canDash)
        {
            float __speedX = _movementXDirection * _moveSpeed;
            _rb.velocity = new Vector2(__speedX, _rb.velocity.y);
        }
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
