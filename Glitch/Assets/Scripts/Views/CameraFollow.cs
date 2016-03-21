using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

	Transform player;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}

	void Update ()
	{
		transform.position = new Vector3 (player.position.x + 4, transform.position
			.y, player.position.z);
	}
}
