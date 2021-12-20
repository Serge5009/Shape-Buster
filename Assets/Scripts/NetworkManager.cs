using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    List<RoomInfo> mpRooms = new List<RoomInfo>();
    bool isInGame = false;
    public bool isGameActive = false;

    [SerializeField]
    ScenesManager sManager;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void StartMP()
    {

        PhotonNetwork.NickName = "Serhii";
        PhotonNetwork.AutomaticallySyncScene = true;


        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        PhotonNetwork.JoinLobby();
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
        else if (mpRooms.Count > 0 && !isInGame)
        {
            JoinLastRoom();
        }
    }

    void CreateNewRoom()
    {
        string name = (mpRooms.Count + 1).ToString();
        RoomOptions options = new RoomOptions() { MaxPlayers = 2 };

        Debug.Log("Creating new room " + name);

        PhotonNetwork.CreateRoom(name, options);
    }

    void JoinLastRoom()
    {
        string name = mpRooms.Count.ToString();

        Debug.Log("Joining room " + name);

        PhotonNetwork.JoinRoom(name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)  //  This will initialize game for Room Master
    {
        Debug.Log("Player " + newPlayer.NickName + " joined the room!");
        StartGame();
    }

    public override void OnJoinedRoom()                         //  This will initialize game for second player
    {
        Debug.Log("Joined");
        int numPlayers = 0;
        isInGame = true;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            numPlayers++;
        }
        if (numPlayers == 2)
            StartGame();

    }


    void StartGame()
    {
        sManager.LoadRound();
        isGameActive = true;
    }
}
