using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Lever : MonoBehaviour
{
    public GameObject reachable, unreachable;
    private Animator reachableAnimator, unreachableAnimator;
    private SkinnedMeshRenderer reachableRenderer, unreachableRenderer;
    const string stateAnimBool = "State";


    public float radius = 0.5f;
    public List<Usable> usables = new List<Usable>();


    public static List<Lever> levers = new List<Lever>();


    private void Awake()
    {
        reachableAnimator = reachable.GetComponent<Animator>();
        unreachableAnimator = unreachable.GetComponent<Animator>();

        reachableRenderer = reachable.GetComponentInChildren<SkinnedMeshRenderer>();
        unreachableRenderer = unreachable.GetComponentInChildren<SkinnedMeshRenderer>();
    }


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
        reachableRenderer.enabled = isReachable;
        unreachableRenderer.enabled = !isReachable;
    }


    public void Use()
    {
        reachableAnimator.SetBool(stateAnimBool, !reachableAnimator.GetBool(stateAnimBool));
        unreachableAnimator.SetBool(stateAnimBool, !unreachableAnimator.GetBool(stateAnimBool));
        foreach (Usable usable in usables)
        {
            usable.Use();
        }
    }


    public bool isReachable
    {
        get
        {
            return ReachableFromPosition(Character.playerVessel.transform.position);
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
