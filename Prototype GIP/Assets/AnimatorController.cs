using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {

    public static AnimatorController instance;

    Transform myTrans;
    Animator myAnim;
    
	void Start ()
    {
        myTrans = transform;
        myAnim = GetComponent<Animator>();
        instance = this;
	}
	
    
	public void UpdateSpeed (float currentSpeed)
    {
        myAnim.SetFloat("Speed",currentSpeed);
    }

    public void UpdateIsGrounded(bool IsGrounded)
    {
        myAnim.SetBool("IsGrounded", IsGrounded);
    }
}
