using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.Demo.Cockpit;
using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;
//using Boo.Lang.Environments;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameObject prefab;

    public int[] playerArr = new int[4];

    int myNumber = 0;  // 나의 배열상 회원번호

    public GameObject[] spawnArray = new GameObject[4];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerArr.Length; i++)
        {
            playerArr[i] = 0;
        }

        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.CustomRoomProperties = new Hashtable() { { "Player", playerArr } };
        PhotonNetwork.CreateRoom("testRoom", roomOptions);

    }

    public override void OnJoinedRoom()
    {
        myNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        Hashtable myTable = PhotonNetwork.CurrentRoom.CustomProperties;
        playerArr = (int[])myTable["Player"];
        for (int i = 0; i < playerArr.Length; i++)
        {
            if (playerArr[i] == 0)
            {
                playerArr[i] = myNumber;
                break;
            }
        }
        photonView.RPC("UpdateTable", RpcTarget.All, playerArr);

        print("My ID :" + myNumber.ToString());

        for (int i = 0; i < playerArr.Length; i++)
        {
            if (playerArr[i] == myNumber)
            {
                GameObject player = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
                player.transform.position = spawnArray[i].transform.position;
                break;
            }
        }

    }

    [PunRPC]
    public void UpdateTable(int[] arr)     //수정된 테이블을 모든 플레이어들에게 동기화 시킴
    {
        Hashtable myTable = PhotonNetwork.CurrentRoom.CustomProperties;
        myTable["Player"] = arr;
        PhotonNetwork.CurrentRoom.SetCustomProperties(myTable);
        playerArr = arr;
    }

    void Update()
    {

    }

    /*public override void OnDisconnected(DisconnectCause cause)
    {

        Hashtable myTable = PhotonNetwork.CurrentRoom.CustomProperties;
        playerArr = (string[])myTable["Player"];
        playerArr[myNum] = "Null";

        photonView.RPC("UpdeateTable", RpcTarget.All, playerArr);
    }*/

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int otherPlayerNum = otherPlayer.ActorNumber;
            Hashtable myTable = PhotonNetwork.CurrentRoom.CustomProperties;
            playerArr = (int[])myTable["Player"];
            for (int i = 0; i < playerArr.Length; i++)
            {
                if (playerArr[i] == myNumber)
                {
                    playerArr[i] = 0;
                    break;
                }
            }
            photonView.RPC("UpdateTable", RpcTarget.All, playerArr);
        }
    }


    private void OnApplicationQuit()    // 라이프사이클이 모두 끝날때
    {
    }
}
