using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePlayerAnimator : MonoBehaviour
{


    //-- SOURCE ------------------------------------------------------

    public AnimatorSource animatorSource;

    
    public void PlayAnimation(string animationIdentifier)
    {

        //animatorSource

        animatorSource.ChangeAnimations(animationIdentifier);
    }
}
