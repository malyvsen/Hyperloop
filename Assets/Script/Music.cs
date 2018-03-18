using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> clips = new List<AudioClip>();

    public static Music instance;


    private void Awake()
    {
        instance = this;
    }


    public void Rewind()
    {
        int timeBefore = source.timeSamples;
        source.clip = clips[Game.instance.currentLoop % clips.Count];
        source.timeSamples = timeBefore;
        source.Play();
    }
}
