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


    public void Rewind()
    {
        loopStart = Time.time;

        foreach (Usable usable in Usable.usables)
        {
            usable.Rewind();
        }

        foreach (Character character in Character.characters)
        {
            character.Rewind();
        }

        GameObject newPlayer = Instantiate(player);
        Character newCharacter = newPlayer.GetComponent<Character>();
    }
}
