using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CN1Event : MonoBehaviour
{
    public Animator ballAnimator;

    public void IniciarAnimacion2BallLight(){
        ballAnimator.SetTrigger("Next1");
    } 
}
