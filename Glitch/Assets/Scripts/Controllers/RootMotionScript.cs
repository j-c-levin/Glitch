using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]

public class RootMotionScript : MonoBehaviour
{
	Animator animator;
    public int controlScheme = 1;
    float speed = 10f;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void OnAnimatorMove ()
	{
        switch (controlScheme)
        {
            case 1:
                wasdAndShoot();
                break;
            case 2:
                wasdAndMouseLook();
                break;
        }
    }

    void wasdAndShoot()
    {
        float movement = (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Horizontal"))) ? Mathf.Abs(Input.GetAxis("Vertical")) : Mathf.Abs(Input.GetAxis("Horizontal"));

        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = new Vector3(-Input.GetAxis("Vertical") * 3, 0, Input.GetAxisRaw("Horizontal") * 3);

        animator.SetFloat("MoveForward", movement);

        Vector3 lookPos = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        if (lookPos != Vector3.zero)
            transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * speed);
    }

    void wasdAndMouseLook()
    {
        float movement = (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Horizontal"))) ? Mathf.Abs(Input.GetAxis("Vertical")) : Mathf.Abs(Input.GetAxis("Horizontal"));

        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = new Vector3(-Input.GetAxis("Vertical") * 3, 0, Input.GetAxisRaw("Horizontal") * 3);

        animator.SetFloat("MoveForward", movement);

        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }
}