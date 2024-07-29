using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAnim : MonoBehaviour
{
    public Animator _animator;
    [Range(0f, 10f)]
    public float _speed;
    public bool _dead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Blend",_speed);
        _animator.SetBool("Dead", _dead);
    }
}
