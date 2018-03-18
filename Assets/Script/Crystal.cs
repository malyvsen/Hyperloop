using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Crystal : MonoBehaviour
{
    public static List<Crystal> crystals = new List<Crystal>();

    public float radius = 0.5f;
    public GameObject model;

    private bool _pickedUp = false;
    public bool pickedUp
    {
        get
        {
            return _pickedUp;
        }

        set
        {
            if (value == _pickedUp)
            {
                return;
            }

            _pickedUp = value;

            if (value)
            {
                // TODO: some animation one day?
                model.SetActive(false);
            }
        }
    }


    private void OnEnable()
    {
        crystals.Add(this);
    }


    private void OnDisable()
    {
        crystals.Remove(this);
    }


    private void Update()
    {
        if (ReachableFromPosition(Character.playerVessel.transform.position))
        {
            pickedUp = true;
        }
    }


    public bool ReachableFromPosition(Vector3 pos)
    {
        return Vector3.Distance(transform.position, pos) < radius;
    }


    public static bool allPickedUp
    {
        get
        {
            return crystals.All(x => x.pickedUp);
        }
    }
}
