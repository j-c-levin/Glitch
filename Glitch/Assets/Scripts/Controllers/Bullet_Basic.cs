using UnityEngine;
using ExitGames.Client.Photon;

public class Bullet_Basic : MonoBehaviour
{
    PhotonView photonView;
    public int index;
    public int damage;

    public void setPhotonView(PhotonView photonView)
    {
        this.photonView = photonView;
    }

    public void onKill()
    {
        photonView.RPC("killConfirmed", PhotonTargets.All);
        destroyBullet();
    }

    public void destroyBullet()
    {
        photonView.RPC("destroyBullet", PhotonTargets.All, index);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8)
            destroyBullet();
    }
}
