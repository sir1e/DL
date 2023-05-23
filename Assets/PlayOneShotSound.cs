using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotSound : StateMachineBehaviour
{
    public AudioClip soundtoPlay;
    public float volume = 1f;
    public bool playEnter = true, playExit = false, playAfterDelay = false;

    public float playDelay = 0.25f;
    private float timeSinceEnter = 0;
    private bool DelayedSoundPlayed = false;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        if (playEnter)
            AudioSource.PlayClipAtPoint(soundtoPlay, animator.gameObject.transform.position, volume);

        timeSinceEnter = 0f;
        DelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !DelayedSoundPlayed)
        {
            timeSinceEnter += Time.deltaTime;
            if(timeSinceEnter > playDelay)
            {
                
                    AudioSource.PlayClipAtPoint(soundtoPlay, animator.gameObject.transform.position, volume);
                DelayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playExit)
            AudioSource.PlayClipAtPoint(soundtoPlay, animator.gameObject.transform.position, volume);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
