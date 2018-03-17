using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingWall : Usable
{
    [HideInInspector]
    public Transform target;
    public Transform startMarker;
    public Transform endMarker;

    public float moveSpeed = 2f;


    private void Start()
    {
        target = startMarker;
    }


    private void Update()
    {
        Vector3 deltaPos = target.position - transform.position;
        float moveThisFrame = moveSpeed * Time.deltaTime;
        if (moveThisFrame < deltaPos.magnitude)
        {
            transform.position = target.position;
            return;
        }

        transform.position += deltaPos.normalized * moveThisFrame;
    }


    public override void Use()
    {
        target = (target == endMarker) ? startMarker : endMarker;
    }
}
