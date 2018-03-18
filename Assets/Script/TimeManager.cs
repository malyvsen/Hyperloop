using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public GameObject player;
    public float loopStart = 0f;


    private void Awake()
    {
        instance = this;
    }


    public void ResetEverything()
    {
        loopStart = Time.time;

        foreach (Usable usable in Usable.usables)
        {
            usable.ResetTime();
        }

        foreach (Character character in Character.characters)
        {
            character.ResetTime();
        }

        GameObject newPlayer = Instantiate(player);
        Character newCharacter = newPlayer.GetComponent<Character>();
        newCharacter.captureKeys = true;
        newCharacter.timeline = new Timeline();
    }
}
