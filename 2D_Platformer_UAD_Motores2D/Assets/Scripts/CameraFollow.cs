using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float _followSpeed;
    [SerializeField]
    private Transform _transformTarget;

    void Start()
    {

    }
    private void FixedUpdate()
    {
        Vector3 newPosition = _transformTarget.position;
        //newPosition.y = _transformTarget.position.y + 0.8f;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, _followSpeed * Time.deltaTime);
    }
}
