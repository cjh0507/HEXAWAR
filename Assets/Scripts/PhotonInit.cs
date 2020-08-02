using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PhotonInit : Photon.PunBehaviour {

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("MyFPS 1.0");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("No Room");
        PhotonNetwork.CreateRoom("MyRoom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Finish make a room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        StartCoroutine(this.CreatePlayer());
    }

    IEnumerator CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player",
                                    new Vector3(0, 1, 0),
                                    Quaternion.identity,
                                    0);
        yield return null;
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}