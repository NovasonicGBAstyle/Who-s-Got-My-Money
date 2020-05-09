using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableVolumeControl : MonoBehaviour
{
    //This is the audio source that will be manipulated by this script.
    private AudioSource _audio;

    //This is the interactable we will be working with.
    private XRBaseInteractable _interactor;

    //Just a bool to make sure we can actually play sounds.
    private bool playable = true;

    //This will determine if we need to play sound or not.
    private bool listen = false;

    //This is the volume we are at.
    private float volume = 0.0f;

    [Tooltip("This is the max volume for the sound when this object should be heard.")]
    [SerializeField]
    private float maxVolume = 1.0f;

    [Tooltip("This is the change in volume over time.  Value should be between 0 and 1.")]
    [SerializeField]
    private float volumeChange = 0.01f;



    /// <summary>
    /// Setup for the class.
    /// </summary>
    private void Awake()
    {
        //Set the audio source.
        _audio = GetComponent<AudioSource>();

        //Make sure we have audio.
        if (_audio == null)
        {
            //Set playable to false so we don't try to do things.
            playable = false;
            Debug.LogError("No AudioSource found for interactable volume control.");
        }

        //Set the interactable
        _interactor = GetComponent<XRBaseInteractable>();

        //Make sure we have one.
        if (_interactor == null)
        {
            //Set playable to false so we don't try to do things.
            playable = false;
            Debug.LogError("No XRBaseInteractable found for interactable volume control.");
        }

        if (playable)
        {
            //We are going to set some listeners to change the volume when events happen.
            _interactor.onHoverEnter.AddListener(IncreaseSound);
            _interactor.onHoverExit.AddListener(DecreaseSound);
        }

    }

    /// <summary>
    /// Cleanup.
    /// </summary>
    private void OnDestroy()
    {
        if (playable)
        {
            _interactor.onHoverEnter.RemoveListener(IncreaseSound);
            _interactor.onHoverExit.RemoveListener(DecreaseSound);
        }
    }

    /// <summary>
    /// Update is called once per frame.  It will mostly be used to determine if we are changing volume.
    /// </summary>
    void Update()
    {
        //Check to see if we need to play sound.
        //We are checking for playable first to make sure we have everything we need.
        if(playable && listen)
        {
            //Check volume level.  If it's not yet to max volume, increase it.
            if(volume < maxVolume)
            {
                volume += volumeChange;
                _audio.volume = volume;
            }
        }
        else if(playable && !listen)
        {
            //Check volume level.  If it's still audible, decrease volume.
            if(volume > 0.0f)
            {
                volume -= volumeChange;
                _audio.volume = volume;
            }
        }
    }

    /// <summary>
    /// This is the listener that runs when we've been hovered on.
    /// </summary>
    /// <param name="interactor"></param>
    private void IncreaseSound(XRBaseInteractor interactor)
    {
        //Set the listen value to true.
        listen = true;
    }

    /// <summary>
    /// This is the listener that runs when we are no longer hovered over.
    /// </summary>
    /// <param name="interactor"></param>
    private void DecreaseSound(XRBaseInteractor interactor)
    {
        //Set listen to false.
        listen = false;
    }
}