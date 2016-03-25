using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    void Start()
    {

    }

    void Update()
    {
        if (player != null)
            transform.position = new Vector3(player.position.x + 4, transform.position
                .y, player.position.z);
    }
}
