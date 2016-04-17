using UnityEngine;
using Glitch;

public class GameController : Photon.PunBehaviour
{
    UIController UIController;
    GameObject myPlayer;
    GameObject[] spawnLocation;
    GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        spawnLocation = GameObject.FindGameObjectsWithTag("SpawnLocation");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        UIController = GetComponent<UIController>();

        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room101", new RoomOptions(), new TypedLobby());
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("Failed to connect initially, retrying offline");
        PhotonNetwork.offlineMode = true;
        PhotonNetwork.CreateRoom("Offline");
    }

    public override void OnJoinedRoom()
    {
        respawn();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
    }

    public void respawn()
    {
        int location = Random.Range(0, spawnLocation.Length);
        myPlayer = PhotonNetwork.Instantiate("Test_Character", spawnLocation[location].transform.position, spawnLocation[location].transform.rotation, 0);

        mainCamera.GetComponent<Camera>().enabled = true;
        mainCamera.GetComponent<CameraFollow>().player = myPlayer.GetComponent<Transform>();

        myPlayer.GetComponent<RootMotionScript>().enabled = true;
        myPlayer.GetComponent<Rigidbody>().isKinematic = false;
        myPlayer.GetComponent<PlayerController>().Health = 10;
        myPlayer.GetComponent<PlayerController>().Controller = this;
    }

    public void addKill()
    {
        UIController.AddKill();
    }

    public void addDeath()
    {
        UIController.AddDeath();
    }

    public void changeControls(int style)
    {
        switch (style)
        {
            case 1:
                myPlayer.GetComponent<RootMotionScript>().controlScheme = 1;
                break;
            case 2:
                myPlayer.GetComponent<RootMotionScript>().controlScheme = 2;
                break;
            case 3:
                break;
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
