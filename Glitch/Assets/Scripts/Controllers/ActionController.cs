using UnityEngine;
using System.Collections;
using Glitch;

public class ActionController : MonoBehaviour
{

    PhotonView photonView;
    GameObject bullet;
    GameObject[] bulletArray;
    int pointer = 0;
    public float bulletSpeed;
    Vector3 colourSerialized;
    float min = 0f;
    float max = 1f;

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
        Color myColour = new Color(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        colourSerialized = new Vector3(myColour.r, myColour.g, myColour.b);
    }

    void Update()
    {
        if (PhotonView.isMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                fire();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Color myColour = new Color(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
                colourSerialized = new Vector3(myColour.r, myColour.g, myColour.b);

                fire();
            }
        }
    }

    void fire()
    {
        Vector3 position = new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward + (transform.right * 0.15f);

        PhotonView.RPC("shoot", PhotonTargets.All, position, transform.rotation, colourSerialized);
    }

    [PunRPC]
    void shoot(Vector3 position, Quaternion rotation, Vector3 colour)
    {
        GameObject o = Instantiate(bullet, position, rotation) as GameObject;
        o.GetComponent<Renderer>().material.SetColor("_Color",new Color(colour.x, colour.y, colour.z));
        o.GetComponent<Renderer>().material.SetColor("_EmissionColor",new Color(colour.x, colour.y, colour.z));
        o.GetComponent<Light>().color = new Color(colour.x, colour.y, colour.z);
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
