using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorKidButton : MonoBehaviour
{
    //We will get a reference to the game manager singleton.
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the game manager.
        gameManager = GameManager.singleton;

        if (gameManager != null)
        {
            //Add some listeners for the game endings.
            gameManager.goodEnding.AddListener(DisableMe);
            gameManager.badEnding.AddListener(DisableMe);
        }
        else
        {
            Debug.LogError("There is no game manager!");
        }

    }

    /// <summary>
    /// Cleanup.
    /// </summary>
    private void onDestroy()
    {
        gameManager.goodEnding.RemoveListener(DisableMe);
        gameManager.badEnding.RemoveListener(DisableMe);
    }

    /// <summary>
    /// This will hide this button.
    /// </summary>
    private void DisableMe()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// This is the function that will run on button press.
    /// </summary>
    public void PoorKidButtonClicked()
    {
        gameManager.PoorKidSelected();
    }
}
