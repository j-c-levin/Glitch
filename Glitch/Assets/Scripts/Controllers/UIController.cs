using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class UIController : Photon.PunBehaviour
{

    public Text scoreboard;
    string scoreText;

    public override void OnJoinedRoom()
    {
        Hashtable properties = new Hashtable();
        properties.Add("Name", PhotonNetwork.playerList.Length);
        properties.Add("Kill", 0);
        properties.Add("Death", 0);
        PhotonNetwork.player.SetCustomProperties(properties);
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        updateScores();
    }

    void updateScores()
    {
        scoreText = "";
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.customProperties["Name"] == null)
                continue;

            string name = player.customProperties["Name"].ToString();
            string kill = player.customProperties["Kill"].ToString();
            string death = player.customProperties["Death"].ToString();

            scoreText += name;
            scoreText += "\n";
            scoreText += kill;
            scoreText += " / ";
            scoreText += death;
            scoreText += "\n\n";
        }
        scoreboard.text = scoreText;
    }

    public void AddKill()
    {
        Hashtable properties = PhotonNetwork.player.customProperties;
        properties["Kill"] = (int)properties["Kill"] + 1;
        PhotonNetwork.player.SetCustomProperties(properties);
    }

    public void AddDeath()
    {
        Hashtable properties = PhotonNetwork.player.customProperties;
        properties["Death"] = (int)properties["Death"] + 1;
        PhotonNetwork.player.SetCustomProperties(properties);
    }
}
