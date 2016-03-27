using UnityEngine;
using System.Collections;

namespace Glitch
{
    public class NetworkCharacter : Photon.MonoBehaviour
    {
        private Animator anim;
        private Rigidbody rigidBody;

        private Vector3 correctPlayerPos;
        private Quaternion correctPlayerRot;
        private Vector3 correctVelocity;
        private float moveForwards;

        private double m_LastNetworkDataReceivedTime;

        void Awake()
        {
            anim = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!photonView.isMine)
            {
                float pingInSeconds = (float)PhotonNetwork.GetPing() * 0.001f;
                float timeSinceLastUpdate = (float)(PhotonNetwork.time - m_LastNetworkDataReceivedTime);
                float totalTimePassed = pingInSeconds + timeSinceLastUpdate;

                transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, 0.3f) + (correctVelocity * totalTimePassed * Time.deltaTime);

                transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, 0.3f);

                anim.SetFloat("MoveForward", Mathf.Lerp(anim.GetFloat("MoveForward"), moveForwards, 0.3f));
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(rigidBody.velocity);
                stream.SendNext(anim.GetFloat("MoveForward"));
            }
            else
            {
                // Network player, receive data
                m_LastNetworkDataReceivedTime = info.timestamp;
                this.correctPlayerPos = (Vector3)stream.ReceiveNext();
                this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
                this.correctVelocity = (Vector3)stream.ReceiveNext();
                this.moveForwards = (float)stream.ReceiveNext();
            }
        }
    }
}
