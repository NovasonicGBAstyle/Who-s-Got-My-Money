using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Basically, I just want one game manager, and it will be a singleton.
    public static GameManager singleton;

    //Create some events we'll use later.
    public UnityEvent goodEnding;
    public UnityEvent badEnding;

    private void Awake()
    {
        
        //Check basic singleton stuff.
        if (singleton == null)
        {
            singleton = this;
        }
        //Make sure no other instances are allowed to exist.
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        //I actually don't want this to persist throughout the game.
        //I actually want this to reload with every gameplay through.
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the user wants to quit.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    /// <summary>
    /// This will quit the game.  In a real game, it will quit.  In an editor run, it will stop.
    /// </summary>
    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    /// <summary>
    /// This is called by the rich button press in the game.  It will invoke the good ending event.
    /// </summary>
    public void RichKidSelected()
    {
        //Invoke the good ending.
        goodEnding.Invoke();
        Debug.Log("Rich Kid Selected.");
    }

    /// <summary>
    /// This is called by the poor kid button press in the game.  It will invke the bad ending event.
    /// </summary>
    public void PoorKidSelected()
    {
        //Invoke the bad ending.
        badEnding.Invoke();
        Debug.Log("Poor Kid Selected.");
    }
}
