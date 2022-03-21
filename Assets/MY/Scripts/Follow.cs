using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform trnTarget;
    public float followSpeed = 3;

    private void Awake()
    {
        K.follow = this;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, trnTarget.position, Time.deltaTime);
    }
}
