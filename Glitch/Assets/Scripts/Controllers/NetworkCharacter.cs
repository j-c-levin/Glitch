using UnityEngine;
using System.Collections;

namespace Glitch
{
    public class NetworkCharacter : Photon.MonoBehaviour
    {
        private Vector3 correctPlayerPos;
        private Quaternion correctPlayerRot;

        // Update is called once per frame
        void Update()
        {
            if (!photonView.isMine)
            {
                transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, 0.3f);
                transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, 0.3f);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else
            {
                // Network player, receive data
                this.correctPlayerPos = (Vector3)stream.ReceiveNext();
                this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}
