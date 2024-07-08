using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SimpleInputManager _inputManager;

    void Update()
    {
        var _inputWalk = _inputManager.InputRightButton;
        var _inputDash = _inputManager.InputDashBtn;
        var _inputAttack1 = _inputManager.InputAttack1Button;

        if (_inputWalk)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }

        if (_inputDash)
        {
            _animator.SetBool("IsDead", true);
        }
        else
        {
            _animator.SetBool("IsDead", false);
        }

        if (_inputAttack1)
        {
            _animator.SetBool("IsAttacking", true);
        }
        else
        {
            _animator.SetBool("IsAttacking", false);
        }
    }
}
