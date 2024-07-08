using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SimpleInputManager _inputManager;

    private void Update()
    {
        var inputJump = _inputManager.InputJumpButton;

        if (inputJump)
        {
            _animator.SetBool("Restart",true);
        } else
        {
            _animator.SetBool("Restart", false);
        }
    }
}
