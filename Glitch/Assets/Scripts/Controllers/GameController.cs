using UnityEngine;
using System.Collections;
using Glitch;

public class GameController : Photon.PunBehaviour {

    GameObject myPlayer;
    GameObject[] spawnLocation;
    GameObject mainCamera;

	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings("0.1");
        spawnLocation = GameObject.FindGameObjectsWithTag("SpawnLocation");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room101", new RoomOptions(), new TypedLobby());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        myPlayer = PhotonNetwork.Instantiate("Test_Character", spawnLocation[0].transform.position, spawnLocation[0].transform.rotation, 0);
        mainCamera.GetComponent<Camera>().enabled = true;
        mainCamera.GetComponent<CameraFollow>().player = myPlayer.GetComponent<Transform>();
        myPlayer.GetComponent<RootMotionScript>().enabled = true;
        myPlayer.GetComponent<Rigidbody>().isKinematic = false;
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
