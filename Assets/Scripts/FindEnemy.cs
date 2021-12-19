using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FindEnemy : MonoBehaviourPunCallbacks
{
    List<RoomInfo> mpRooms = new List<RoomInfo>();
    bool isInGame = false;

    public void StartMP()
    {

        PhotonNetwork.NickName = "Serhii";
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");


    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        mpRooms = roomList;

        Debug.Log(mpRooms.Count);

        if (mpRooms.Count == 0)
        {
            CreateNewRoom();
        }
        else if(mpRooms.Count > 0 && !isInGame)
        {
            JoinLastRoom();
        }
    }

    void CreateNewRoom()
    {
        string name = (mpRooms.Count + 1).ToString();
        RoomOptions options = new RoomOptions() { MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(name, options);
        isInGame = true;

    }

    void JoinLastRoom()
    {
        string name = mpRooms.Count.ToString();

        PhotonNetwork.JoinRoom(name);
    }

}
