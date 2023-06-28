using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFadeRemove : StateMachineBehaviour
{
    public float fadeTime = 7f;
    private float timeElapsed = 0f;
    SpriteRenderer SpriteRenderer;
    GameObject ObjTORemove;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        SpriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = SpriteRenderer.color;
        ObjTORemove = animator.gameObject;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = timeElapsed + Time.deltaTime;
        float AlphaTime = startColor.a * (1 - timeElapsed / fadeTime);

        SpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, AlphaTime);
        if(timeElapsed > fadeTime)
        {
            Destroy(ObjTORemove);
        }
    }

  
}
