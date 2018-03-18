using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cam : MonoBehaviour
{
    public GameObject originalTarget;
    public float followSpeed = 4f;
    public float lookSpeed = 2f;

    private Vector3 offset;


    private void OnEnable()
    {
        offset = transform.position - originalTarget.transform.position;
    }



    private void Update()
    {
        Vector3 targetPos = Character.playerVessel.transform.position + offset;
        float followParam = 1f - Mathf.Pow(0.5f, Time.deltaTime * followSpeed);
        transform.position = Vector3.Lerp(transform.position, targetPos, followParam);

        Vector3 targetLook = (Character.playerVessel.transform.position - transform.position).normalized;
        float lookParam = 1f - Mathf.Pow(0.5f, Time.deltaTime * lookSpeed);
        transform.forward = Vector3.Lerp(transform.forward, targetLook, lookParam);
    }
}
