using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PhotonInit : Photon.PunBehaviour {

    void Awake()
    {
        // 포톤 네트워크에 버전별로 분리하여 접속한다
        PhotonNetwork.ConnectUsingSettings("HEXAWARzz");
    }

    // 로비에 입장시 호출되는 콜백함수
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    // 랜덤 룸 입장에 실패했을 때 호출되는 콜백함수
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("No Room");
        PhotonNetwork.CreateRoom("MyRoom");
    }

    // 룸을 생성완료 하였을 때 호출되는 콜백함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Finish make a room");
    }

    // 룸에 입장되었을 경우 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        StartCoroutine(this.CreatePlayer());
    }

    IEnumerator CreatePlayer()
    {
        PhotonNetwork.Instantiate("Cell",
                                    new Vector2(0, 0),
                                    Quaternion.identity,
                                    0);
        yield return null;
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}