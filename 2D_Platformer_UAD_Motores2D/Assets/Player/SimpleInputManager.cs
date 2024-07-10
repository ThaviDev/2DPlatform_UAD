using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleInputManager : MonoBehaviour
{
    static PlayerInput _input;
    [SerializeField] PlayerInput _inputRef;
    //[SerializeField] private PlayerMotor _motor;

    bool rightBtn;
    bool leftBtn;
    bool jumpBtn;
    bool attack1Btn;
    bool dashBtn;
    bool slowedBtn;
    public bool InputRightButton
    {
        get { return rightBtn; }
    }

    public bool InputLeftButton
    {
        get { return leftBtn; }
    }

    public bool InputJumpButton
    {
        get { return jumpBtn; }
    }

    public bool InputAttack1Button
    {
        get { return attack1Btn; }
    }

    public bool InputDashBtn
    {
        get { return dashBtn; }
    }

    public bool InputSlowedBtn
    {
        get { return slowedBtn; }
    }

    private void Awake()
    {
        _input = _inputRef;
    }

    private void Update()
    {
        if (OnMoveRight())
        {
            rightBtn = true;
        } else
        {
            rightBtn = false;
        }
        if (OnMoveLeft())
        {
            leftBtn = true;
        } else
        {
            leftBtn = false;
        }
        if (OnJump())
        {
            jumpBtn = true;
        } else
        {
            jumpBtn = false;
        }
        if (OnAttack1())
        {
            attack1Btn = true;
        } else
        {
            attack1Btn = false;
        }
        if (OnDash())
        {
            dashBtn = true;
        } else
        {
            dashBtn = false;
        }
        if (OnSlowDown()){
            slowedBtn = true;
        } else
        {
            slowedBtn = false;
        }
    }

    public static bool OnMoveRight()
    {
        return _input.actions.FindAction("MoveRight").IsPressed();
        // .IsPressed(), .WasPressedThisFrame, .WasReleasedThisFrame
        // .ReadValue<Float>, .ReadValue<Vector2>
    }
    public static bool OnMoveLeft()
    {
        return _input.actions.FindAction("MoveLeft").IsPressed();
    }
    public static bool OnJump()
    {
        return _input.actions.FindAction("Jump").WasPressedThisFrame();
    }

    public static bool OnAttack1()
    {
        return _input.actions.FindAction("Attack1").WasPressedThisFrame();
    }

    public static bool OnDash()
    {
        return _input.actions.FindAction("Dash").WasPressedThisFrame();
    }

    public static bool OnSlowDown()
    {
        return _input.actions.FindAction("SlowDown").IsPressed();
    }

}
