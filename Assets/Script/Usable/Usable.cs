using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Usable : MonoBehaviour
{
    public static List<Usable> usables = new List<Usable>();


    private void OnEnable()
    {
        usables.Add(this);
    }


    private void OnDisable()
    {
        usables.Remove(this);
    }



    public virtual void Use()
    {
        throw new System.NotImplementedException("Base usable");
    }


    public virtual void Rewind()
    {
        throw new System.NotImplementedException("Base usable reset");
    }
}
