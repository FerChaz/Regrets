using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePlayerAudio : MonoBehaviour
{
    //-- SOURCE ------------------------------------------------------

    public FXSource FXSource;

    public AudioClip[] audioClipsList;


    public void ReproduceFX(string clipIdentifier)
    {

        foreach(AudioClip audioclip in audioClipsList)
        {
            if (audioclip.name == clipIdentifier)
            {
                FXSource.ReproduceClip(audioclip);
                return;
            }
        }
    }
}
