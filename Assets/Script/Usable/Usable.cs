using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Usable : MonoBehaviour
{
    public Transform lever;
    public virtual void Use()
    {
        throw new System.NotImplementedException("Base usable");
    }


    public static List<Usable> usables = new List<Usable>();

    private void Awake()
    {
        usables.Add(this);
    }


    public static Usable AtPosition(Vector3 pos)
    {
        return usables.FirstOrDefault(x => Vector3.Distance(x.lever.position, pos) < 1f);
    }
}
