using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMotor : MonoBehaviour
{
    [SerializeField] Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var _player = collision.GetComponent<PlayerMovement>();
        if (_player != null)
        {
            _player.TouchCheckPoint(this.gameObject);
            _anim.SetTrigger("TouchCheck");
        }
    }
}
