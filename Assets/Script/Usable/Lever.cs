using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Lever : MonoBehaviour
{
    public GameObject reachable, unreachable;
    public float radius = 0.5f;
    public List<Usable> usables = new List<Usable>();


    public static List<Lever> levers = new List<Lever>();


    private void OnEnable()
    {
        levers.Add(this);
    }


    private void OnDisable()
    {
        levers.Remove(this);
    }


    private void Update()
    {
        bool isReachable = ReachableFromPosition(Character.playerVessel.transform.position);
        reachable.SetActive(isReachable);
        unreachable.SetActive(!isReachable);
    }


    public void Use()
    {
        foreach(Usable usable in usables)
        {
            usable.Use();
        }
    }


    public bool ReachableFromPosition(Vector3 pos)
    {
        return Vector3.Distance(transform.position, pos) < radius;
    }


    public static IEnumerable<Lever> AtPosition(Vector3 pos)
    {
        return levers.Where(x => x.ReachableFromPosition(pos));
    }


    public static void UseFromPosition(Vector3 pos)
    {
        foreach(Lever lever in AtPosition(pos))
        {
            lever.Use();
        }
    }
}
