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
    [SerializeField] int _currentHealth = 3;
    [SerializeField] int _maxHealth = 10;
    [SerializeField] int _startingHealth = 3;
    [SerializeField] bool _isDead;
    [SerializeField] bool _canDoubleJump;
    [SerializeField] bool _isGrounded;
    int _movementXDirection;
    int _lastMovementDirection;
    [SerializeField] Vector2 _lastCheckPointPos;

    [SerializeField] AudioClip _jumpSound;
    [SerializeField] AudioClip _doubleJumpSound;
    [SerializeField] AudioClip _dashSound;
    [SerializeField] AudioClip _pauseSound;
    [SerializeField] AudioClip _eatFruitSound;
    [SerializeField] AudioClip _bulletSound;

    [SerializeField] Rigidbody2D _rb;

    [SerializeField] FruitColector _fruitManager;
    [SerializeField] PlayerAnimation _playerAnim;
    [SerializeField] SimpleInputManager _inputManager;
    [SerializeField] PauseManager _pauseManager;
    [SerializeField] GameObject _pBullet;
    [SerializeField] UIColectables _uIInfo;

    /*
    private void Awake()
    {
        _inputManager = GetComponent<SimpleInputManager>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _fruitManager = GetComponent<PlayerFruitManager>();
    } */

    void Start()
    {
        _lastCheckPointPos = new Vector2(0f, 0f);
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
        var inputPause = _inputManager.InputPauseBtn;

        _uIInfo._livesCount = _currentHealth;

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
                SFXManager.Instance.PlaySFXClip(_jumpSound, 1);
            }
            else if (_canDoubleJump) {
                SFXManager.Instance.PlaySFXClip(_doubleJumpSound, 1);
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
            SFXManager.Instance.PlaySFXClip(_dashSound, 1);
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

        if (inputAttack)
        {
            _playerAnim._animationsID = PlayerAnimation.myAnimations.Attack1;
            StartCoroutine(SpawnBullet());
        }

        if (inputPause)
        {
            SFXManager.Instance.PlaySFXClip(_pauseSound, 1);
            if (_pauseManager.isPaused)
            {
                _pauseManager.Resume();
            } else
            {
                _pauseManager.Pause();
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

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(0.25f);
        SFXManager.Instance.PlaySFXClip(_bulletSound, 1);

        GameObject _myBul = Instantiate(_pBullet, this.transform.position,this.transform.rotation);
        BulletMotor _bulMot = _myBul.GetComponent<BulletMotor>();
        if (_movementXDirection > 0)
        {
            _bulMot._dirIsRight = true;
        } else if (_movementXDirection < 0)
        {
            _bulMot._dirIsRight = false;
        } else if (_lastMovementDirection > 0)
        {
            _bulMot._dirIsRight = true;
        } else if (_lastMovementDirection < 0)
        {
            _bulMot._dirIsRight = false;
        }
    }

    public void PlayerDied()
    {
        _playerAnim._animationsID = PlayerAnimation.myAnimations.Dying;
        _uIInfo._livesCount = _currentHealth;
        _pauseManager.GameOver();
        Destroy(this);
    }

    public void PlayerWon()
    {
        _pauseManager.Victory();
        Destroy(this);
    }

    public void HasColectedFruit()
    {
        SFXManager.Instance.PlaySFXClip(_eatFruitSound, 1);
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
        } else
        {
            transform.position = _lastCheckPointPos;
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
    
    public void TouchCheckPoint(GameObject _flag)
    {
        _lastCheckPointPos = _flag.transform.position;
    }
}
