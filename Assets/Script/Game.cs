using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Game : MonoBehaviour
{
    public static Game instance;

    public GameObject originalPlayer;
    public float loopStart = 0f;
    public int currentLoop = 0;


    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if (Crystal.allPickedUp)
        {
            // we won!
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentIndex >= 0)
            {
                if (currentIndex + 1 < SceneManager.sceneCount)
                {
                    // there is a scene to be loaded
                    SceneManager.LoadScene(currentIndex + 1);
                }
                else
                {
                    Debug.Log("Win, but no more scenes to load");
                }
            }
            else
            {
                Debug.Log("Win, but scene not placed in build settings");
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rewind();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentIndex >= 0)
            {
                SceneManager.LoadScene(currentIndex);
            }
            else
            {
                Debug.Log("Please add the scene to build settings before trying to reset");
            }
            return;
        }
    }


    public void Rewind()
    {
        currentLoop++;
        loopStart = Time.time;

        foreach (Usable usable in Usable.usables)
        {
            usable.Rewind();
        }

        foreach (Character character in Character.characters)
        {
            character.Rewind();
        }

        Instantiate(originalPlayer);

        Music.instance.Rewind();
    }
}
