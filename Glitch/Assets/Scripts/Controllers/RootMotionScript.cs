using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]

public class RootMotionScript : MonoBehaviour
{
	Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void OnAnimatorMove ()
	{
		float movement = (Mathf.Abs (Input.GetAxis ("Vertical")) > Mathf.Abs (Input.GetAxis ("Horizontal"))) ? Mathf.Abs (Input.GetAxis ("Vertical")) : Mathf.Abs (Input.GetAxis ("Horizontal"));

        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = new Vector3(-Input.GetAxis("Vertical") * 3, 0, Input.GetAxisRaw("Horizontal") * 3);

        animator.SetFloat ("MoveForward", movement);

        Vector3 lookPos = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        if (lookPos != Vector3.zero)
            transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 10);
    }
}