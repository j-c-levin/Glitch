using UnityEngine;
using System.Collections;
using Glitch;

public class ActionController : MonoBehaviour {

    PhotonView photonView;
    GameObject bullet;
    GameObject[] bulletArray;
    int pointer = 0;
    public float bulletSpeed;

    public PhotonView PhotonView
    {
        get
        {
            return photonView;
        }

        set
        {
            photonView = value;
        }
    }

    void Start()
    {
        bulletArray = new GameObject[20];
        PhotonView = GetComponent<PhotonView>();
        bullet = Resources.Load("Bullet_Test") as GameObject;
    }

    void Update()
    {
        if (PhotonView.isMine)
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 position = new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward;
                PhotonView.RPC("shoot", PhotonTargets.All, position, transform.rotation);
            }
    }

    [PunRPC]
    void shoot(Vector3 position, Quaternion rotation)
    {
        GameObject o = Instantiate(bullet, position, rotation) as GameObject;
        o.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        o.GetComponent<Bullet_Basic>().photonView = PhotonView;
        o.GetComponent<Bullet_Basic>().index = pointer;
        o.GetComponent<Bullet_Basic>().damage = 5;
        bulletArray[pointer] = o;
        pointer += 1;
        pointer %= bulletArray.Length;
    }

    [PunRPC]
    void destroyBullet(int index)
    {
        Destroy(bulletArray[index]);
    }

}
