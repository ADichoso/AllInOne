using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    #region Singleton
    public static SoundController sharedInstance;

    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of SoundController has been detected");
        }
    }
    #endregion

    public AudioSource SFXSource;

    public AudioClip buttonClick;
    public void playSound(AudioClip clip, bool isLoop)
    {
        if(SFXSource.clip != null) SFXSource.Stop();

        SFXSource.clip = clip;

        SFXSource.loop = isLoop;

        SFXSource.Play();
    }

    public void OnButtonClick()
    {
        playSound(buttonClick, false);
    }
}
