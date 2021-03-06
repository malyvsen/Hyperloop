﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    public static List<Character> characters = new List<Character>();

    private static Character _playerVessel;
    public static Character playerVessel
    {
        get
        {
            return _playerVessel;
        }

        set
        {
            if (value == _playerVessel)
            {
                return;
            }

            if (_playerVessel != null)
            {
                _playerVessel.player.SetActive(false);
                _playerVessel.clone.SetActive(true);
            }

            _playerVessel = value;

            _playerVessel.player.SetActive(true);
            _playerVessel.clone.SetActive(false);
            _playerVessel.timeline = new Timeline();
        }
    }

    public bool isPlayerVessel
    {
        get
        {
            return this == playerVessel;
        }


        set
        {
            if (value)
            {
                playerVessel = this;
                return;
            }

            throw new System.NotSupportedException("Setting isPlayerVessel to false");
        }
    }


    [HideInInspector]
    public Timeline timeline = new Timeline();


    public GameObject player, clone;
    private Animator playerAnimator, cloneAnimator;
    const string walkAnimBool = "Walk";

    public Animator animator
    {
        get
        {
            return isPlayerVessel ? playerAnimator : cloneAnimator;
        }
    }


    public float moveSpeed = 5f;

    private bool goingRight = false;
    private bool goingLeft = false;
    private bool rightPrecedes = false;


    public float velocityHorizontal
    {
        get
        {
            if (rightPrecedes)
            {
                if (goingRight)
                {
                    return moveSpeed;
                }

                if (goingLeft)
                {
                    return -moveSpeed;
                }
            }

            if (goingLeft)
            {
                return -moveSpeed;
            }

            if (goingRight)
            {
                return moveSpeed;
            }

            return 0f;
        }
    }


    private new Rigidbody rigidbody;
    private Vector3 startPos;


    private void Awake()
    {
        startPos = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer);

        playerAnimator = player.GetComponent<Animator>();
        cloneAnimator = clone.GetComponent<Animator>();
    }


    private void OnEnable()
    {
        characters.Add(this);
        isPlayerVessel = true;
    }


    private void OnDisable()
    {
        characters.Remove(this);
    }


    private void Update()
    {
        if (isPlayerVessel)
        {
            Action.Type actionMaybe = Action.fromInput;
            if (actionMaybe != Action.Type.none)
            {
                timeline.AddCurrentAction(actionMaybe);
            }
        }

        Action currentAction = timeline.currentAction;
        if (currentAction != null)
        {
            switch (currentAction.type)
            {
                case Action.Type.moveRight:
                    transform.forward = Vector3.right;
                    animator.SetBool(walkAnimBool, true);
                    goingRight = true;
                    rightPrecedes = true;
                    break;
                case Action.Type.stopRight:
                    animator.SetBool(walkAnimBool, false);
                    goingRight = false;
                    break;
                case Action.Type.moveLeft:
                    transform.forward = Vector3.left;
                    animator.SetBool(walkAnimBool, true);
                    goingLeft = true;
                    rightPrecedes = false;
                    break;
                case Action.Type.stopLeft:
                    animator.SetBool(walkAnimBool, false);
                    goingLeft = false;
                    break;
                case Action.Type.useObject:
                    Lever.UseFromPosition(transform.position);
                    break;
            }
            currentAction.executed = true;
        }


        rigidbody.velocity = new Vector3(velocityHorizontal, rigidbody.velocity.y, 0f);
    }


    public void Rewind()
    {
        transform.position = startPos;
        timeline.Rewind();
    }
}
