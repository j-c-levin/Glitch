using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class RootMotionScript : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        animator.SetFloat("MoveForward", Input.GetAxis("Vertical"));

        Vector3 newPosition = transform.position;
        newPosition.z += animator.GetFloat("MoveForwardSpeed") * Time.deltaTime;
        transform.position = newPosition;
    }
}