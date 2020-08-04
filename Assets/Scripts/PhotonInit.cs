using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PhotonInit : Photon.PunBehaviour {

    private int st = 0;
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("hexawar");
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
        PhotonNetwork.Instantiate("CoreCell",
                                    new Vector3(0, 0, 0),
                                    Quaternion.identity,
                                    0);
        st += 1;
        yield return null;
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}