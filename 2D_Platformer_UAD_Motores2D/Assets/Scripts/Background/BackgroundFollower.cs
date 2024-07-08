using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    [SerializeField] Transform _cam;
    [Range(1.2f,10)]
    [SerializeField] float _paralaxFactor;
    [SerializeField] float maxXLimit = 5;
    [SerializeField] float minXLimit = -5;

    [SerializeField] float _teleportDistance = 9.6f;

    Vector3 _initialOffset;

    private void Start()
    {
        _initialOffset = transform.position;
    }

    private void Update()
    {
        Vector3 myTransPos = transform.position;
        //Vector3 targetPosition = _cam.position + _initialOffset;
        transform.position = new Vector3((_cam.transform.position.x / _paralaxFactor) + _initialOffset.x, _cam.transform.position.y + _initialOffset.y);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);

        if (transform.position.x < minXLimit + _cam.transform.position.x)
        {
            _initialOffset.x += _teleportDistance;
        }
        if (transform.position.x > maxXLimit + _cam.transform.position.x)
        {
            _initialOffset.x -= _teleportDistance;
        }
        //print(maxXLimit + _cam.transform.position.x);
    }
}
