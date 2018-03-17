using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    [HideInInspector]
    public Timeline timeline = new Timeline();
    public bool captureKeys = false;
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


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (captureKeys)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }

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
                    goingRight = true;
                    rightPrecedes = true;
                    break;
                case Action.Type.stopRight:
                    goingRight = false;
                    break;
                case Action.Type.moveLeft:
                    goingLeft = true;
                    rightPrecedes = false;
                    break;
                case Action.Type.stopLeft:
                    goingLeft = false;
                    break;
                case Action.Type.useObject:
                    Usable usable = Usable.AtPosition(transform.position);
                    if (usable != null)
                    {
                        usable.Use();
                    }
                    break;
            }
            currentAction.executed = true;
        }


        rigidbody.velocity = new Vector3(velocityHorizontal, rigidbody.velocity.y, 0f);
    }
}
