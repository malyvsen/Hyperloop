using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Action
{
    public float time = 0f;

    public enum Type
    {
        none,

        moveRight,
        stopRight,
        moveLeft,
        stopLeft,
        useObject
    }
    public Type type = Type.none;

    public bool executed = false;


    public static Type fromInput
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return Type.moveRight;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                return Type.stopRight;
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return Type.moveLeft;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                return Type.stopLeft;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                return Type.useObject;
            }

            return Type.none;
        }
    }
}