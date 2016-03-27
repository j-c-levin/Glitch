using UnityEngine;
using System.Collections;

public class Bullet_Basic : MonoBehaviour
{
    public PhotonView photonView;
    public int index;
    public int damage;

    public void destroy()
    {
        photonView.RPC("destroyBullet", PhotonTargets.All, index);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8)
            destroy();
    }
}
