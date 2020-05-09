using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    //We will get a reference to the game manager singleton.
    private GameManager gameManager;

    [Tooltip("This is the text box for a good game ending.")]
    [SerializeField]
    private Text goodEndingText;

    [Tooltip("This is the text box for a bad game ending.")]
    [SerializeField]
    private Text badEndingText;

    [Tooltip("This is the button to end the game.")]
    [SerializeField]
    private Button quitButton;

    /// <summary>
    /// Cleanup.
    /// </summary>
    private void onDestroy()
    {
        gameManager.goodEnding.RemoveListener(GoodEnding);
        gameManager.badEnding.RemoveListener(BadEnding);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //Get a reference to the game manager.
        gameManager = GameManager.singleton;

        if (gameManager != null)
        {
            //Add some listeners for the game endings.
            gameManager.goodEnding.AddListener(GoodEnding);
            gameManager.badEnding.AddListener(BadEnding);
        }
        else
        {
            Debug.LogError("There is no game manager!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This is the good ending.  It runs when the good ending event from the game manager is invoked.
    /// </summary>
    private void GoodEnding()
    {
        //First show the text.
        //goodEndingText.SetActive(true);
        goodEndingText.gameObject.SetActive(true);

        //Next show the quit game button.
        quitButton.gameObject.SetActive(true);

        Debug.Log("End Game script good ending.");
    }

    /// <summary>
    /// This is the bad ending.  It runs when the bad ending event from the gamea manager is invoked.
    /// </summary>
    private void BadEnding()
    {
        //First show the text.
        badEndingText.gameObject.SetActive(true);

        //Next show the quit game button.
        quitButton.gameObject.SetActive(true);
        Debug.Log("End Game script bad ending.");
    }
}
