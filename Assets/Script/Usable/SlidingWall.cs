using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SlidingWall : Usable
{
    public Transform endMarker;

    [HideInInspector]
    public Vector3 target;
    private Vector3 startPos;
    private Vector3 endPos;

    public float moveSpeed = 5f;


    private void Start()
    {
        target = startPos = transform.position;
        endPos = endMarker.position;
    }


    private void Update()
    {
        Vector3 deltaPos = target - transform.position;
        float moveThisFrame = moveSpeed * Time.deltaTime;
        if (moveThisFrame >= deltaPos.magnitude)
        {
            transform.position = target;
            return;
        }

        transform.position += deltaPos.normalized * moveThisFrame;
    }


    public override void Use()
    {
        target = (target == endPos) ? startPos : endPos;
    }


    public override void Rewind()
    {
        target = transform.position = startPos;
    }
}
