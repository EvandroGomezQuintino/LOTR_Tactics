﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Adding library for Connection
using Photon.Pun;
using Photon.Realtime;






public class GameConnection : MonoBehaviourPunCallbacks
{

    public string PlayerName;
    public GameController game;




    // Testing Connection
    public Text chatlog;
    // ---------------------------------    Starting Connection    ------------------------------------------- //
    private void Awake()
    {

        if(GameObject.Find("BackGround").GetComponent<DontDestroy>().localGameMode == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            chatlog.text += "\nConnecting to the server";

            // Setup Connection
            PlayerName = PhotonNetwork.LocalPlayer.NickName;
            // Connecting using Photon settings
            PhotonNetwork.ConnectUsingSettings();

            game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }

    }

    // -------------------------------------    Checking Connection    --------------------------------------- //

    public override void OnConnectedToMaster()
    {


        chatlog.text += "Connected!";

        if(PhotonNetwork.InLobby == false)
        {
            chatlog.text += "\n Entering Lobby";
            PhotonNetwork.JoinLobby();
        }


    }
    // -------------------------------------    Accessing Lobby    --------------------------------------- //

    public override void OnJoinedLobby()
    {
        chatlog.text += "\n Entered Lobby!";
        chatlog.text += "\n Entering Room LOTR";
        
        PhotonNetwork.JoinRoom("LOTR");

    }

    // -------------------------------------    Creating Room    --------------------------------------- //
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        chatlog.text += "\n Error entering the room" + message + "+ | codigo" + returnCode;

        if (returnCode == ErrorCode.GameDoesNotExist)
        {
            chatlog.text += "\n Creating Room";

            // Settings for our room
            RoomOptions roomSettings = new RoomOptions { MaxPlayers = 2 };
            // Creating room
            PhotonNetwork.CreateRoom("LOTR", roomSettings, null);
            PhotonNetwork.LocalPlayer.NickName = "PLAYER1";
            PlayerName = PhotonNetwork.LocalPlayer.NickName;
  

        }


    }


    // -------------------------------------    Accessing Room --------------------------------------- //
    public override void OnJoinedRoom()
    {
        chatlog.text += PlayerName + " entered Room!";

        if (PhotonNetwork.LocalPlayer.IsMasterClient == false)
        {

           
           game.positions[game.xPos(game.GAMECONNECTION.transform),game.yPos(game.GAMECONNECTION.transform)] = PhotonNetwork.Instantiate("CONNECTIONTest", new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
        }
        else
        {
            game.positions[game.xPos(game.GAMECONNECTION2.transform), game.yPos(game.GAMECONNECTION2.transform)] = PhotonNetwork.Instantiate("witchKing", new Vector3(-1.5f, 1.5f, 0), Quaternion.identity);
        }

    }
    // -------------------------------------    New Player  --------------------------------------- //
        public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        newPlayer.NickName = "PLAYER2";
        chatlog.text += newPlayer.NickName + " entered the room";
    }


    // -------------------------------------    Player Leaving Room  --------------------------------------- //
    public override void OnPlayerLeftRoom(Player otherPlayer)
    { 
        chatlog.text = "Player " + otherPlayer.NickName + " left the room.";
    }

    // -------------------------------------    Host leave room  --------------------------------------- //

    public override void OnLeftRoom()
    {
        chatlog.text = "Host left the room!";
    }

    // -------------------------------------    Retuning Connection Error  --------------------------------------- //

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        chatlog.text = "Error to connect: " + errorInfo.Info;
    }





}
