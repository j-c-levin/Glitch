using UnityEngine;
using System.Collections;

namespace Glitch
{
    public class PlayerController : MonoBehaviour
    {
        int health;
        GameController controller;
        Bullet_Basic lastHit;

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
                if (health <= 0)
                {
                    lastHit.GetComponent<Bullet_Basic>().onKill();
                    GetComponent<PhotonView>().RPC("death", PhotonTargets.All);
                }
            }
        }

        public GameController Controller
        {
            get
            {
                return controller;
            }

            set
            {
                controller = value;
            }
        }

        [PunRPC]
        void death()
        {
            Destroy(this.gameObject);
            if (GetComponent<PhotonView>().isMine)
            {
                controller.addDeath();
                controller.respawn();
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            //Only trigger if the object is a bullet AND the player belongs to me
            if (collider.gameObject.layer == 10 && GetComponent<PhotonView>().isMine)
            {
                lastHit = collider.GetComponent<Bullet_Basic>();
                Health -= lastHit.damage;
            }
        }
    }
}
