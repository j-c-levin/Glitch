using UnityEngine;
using System.Collections;

namespace Glitch
{
    public class PlayerController : MonoBehaviour
    {
        int health;
        GameController controller;

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
                    controller.respawn();
                    GetComponent<PhotonView>().RPC("death", PhotonTargets.All);
                }
            }
        }

        void Update()
        {

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
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.layer == 10 && GetComponent<PhotonView>().isMine)
            {
                Health -= collider.GetComponent<Bullet_Basic>().damage;
                collider.GetComponent<Bullet_Basic>().destroy();
            }
        }
    }
}
