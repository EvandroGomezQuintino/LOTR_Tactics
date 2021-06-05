using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Adding library for Connection
using Photon.Pun;
using Photon.Realtime;




public class Connection : MonoBehaviourPunCallbacks
{

    public string PlayerName;
    public GameController game;
    public HiddenSystem hiddenSys;
    public PhotonView photonView;

    private BattleSystem turn;
    public Players player;


    // Setting player (nazgulPlayer == LocalPlayer // heroePlayer == newPlayer)
    public Player nazgulPlayer;
    public Player heroePlayer;



    // Testing Connection
    public Text chatlog;
    // ---------------------------------    Starting Connection    ------------------------------------------- //
    private void Awake()
    {

        //--------------------- TESTING MULTIPLAYER ---------------------//

        //chatlog.text += "\nConnecting to the server";

        // Setup Connection
        PlayerName = PhotonNetwork.LocalPlayer.NickName;
        // Connecting using Photon settings
        PhotonNetwork.ConnectUsingSettings();

        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        hiddenSys = game.GetComponent<HiddenSystem>();

        photonView = GetComponent<PhotonView>();


    }

    // -------------------------------------    Checking Connection    --------------------------------------- //

    public override void OnConnectedToMaster()
    {


        //chatlog.text += "Connected!";

        if(PhotonNetwork.InLobby == false)
        {
            //chatlog.text += "\n Entering Lobby";
            PhotonNetwork.JoinLobby();
        }


    }
    // -------------------------------------    Accessing Lobby    --------------------------------------- //

    public override void OnJoinedLobby()
    {
        //chatlog.text += "\n Entered Lobby!";
        //chatlog.text += "\n Entering Room LOTR";
        
        PhotonNetwork.JoinRoom("LOTR");

    }

    // -------------------------------------    Creating Room    --------------------------------------- //
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //chatlog.text += "\n Error entering the room" + message + "+ | codigo" + returnCode;

        if (returnCode == ErrorCode.GameDoesNotExist)
        {
            //chatlog.text += "\n Creating Room";

            // Settings for our room
            RoomOptions roomSettings = new RoomOptions { MaxPlayers = 2 };
            // Creating room
            PhotonNetwork.CreateRoom("LOTR", roomSettings, null);
            PhotonNetwork.LocalPlayer.NickName = "PLAYER1";
            PlayerName = PhotonNetwork.LocalPlayer.NickName;
            nazgulPlayer = PhotonNetwork.LocalPlayer;

            //TEST
            player = Players.PLAYER1;

        }


    }


    // -------------------------------------    Accessing Room --------------------------------------- //
    public override void OnJoinedRoom()
    {
        //chatlog.text += PlayerName + " entered Room!";

        
       
        // Instantiate pieces as per Player (Host == Nazgul / newPlayer == Heroes)
        if (PhotonNetwork.LocalPlayer.IsMasterClient == false)
        {
           //Setting Player 
           game.player = Players.PLAYER2;

           //game.turn = BattleSystem.START_HEROE;

           heroePlayer = PhotonNetwork.PlayerList[1];

            //Adding Pieces
           game.positions[game.getRow(game.gandalf.transform),game.getColumn(game.gandalf.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/gandalf", new Vector3(game.gandalf.transform.position.x, game.gandalf.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.aragorn.transform),game.getColumn(game.aragorn.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/aragorn", new Vector3(game.aragorn.transform.position.x, game.aragorn.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.frodo.transform),game.getColumn(game.frodo.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/frodo", new Vector3(game.frodo.transform.position.x, game.frodo.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.sam.transform),game.getColumn(game.sam.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/sam", new Vector3(game.sam.transform.position.x, game.sam.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.merry.transform),game.getColumn(game.merry.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/merry", new Vector3(game.merry.transform.position.x, game.merry.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.pippin.transform),game.getColumn(game.pippin.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/pippin", new Vector3(game.pippin.transform.position.x, game.pippin.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.legolas.transform),game.getColumn(game.legolas.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/legolas", new Vector3(game.legolas.transform.position.x, game.legolas.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.boromir.transform),game.getColumn(game.boromir.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/boromir", new Vector3(game.boromir.transform.position.x, game.boromir.transform.position.y, 0), Quaternion.identity);
           game.positions[game.getRow(game.gimli.transform),game.getColumn(game.gimli.transform)] = PhotonNetwork.Instantiate("Pieces/Heroes/gimli", new Vector3(game.gimli.transform.position.x, game.gimli.transform.position.y, 0), Quaternion.identity);




            //Adding opponents pieces positions
            game.positions[game.getRow(game.witchKing.transform), game.getColumn(game.witchKing.transform)] = game.witchKing;
            game.positions[game.getRow(game.nazgul_1.transform), game.getColumn(game.nazgul_1.transform)] = game.nazgul_1;
            game.positions[game.getRow(game.nazgul_2.transform), game.getColumn(game.nazgul_2.transform)] = game.nazgul_2;
            game.positions[game.getRow(game.nazgul_3.transform), game.getColumn(game.nazgul_3.transform)] = game.nazgul_3;
            game.positions[game.getRow(game.nazgul_4.transform), game.getColumn(game.nazgul_4.transform)] = game.nazgul_4;
            game.positions[game.getRow(game.nazgul_5.transform), game.getColumn(game.nazgul_5.transform)] = game.nazgul_5;
            game.positions[game.getRow(game.nazgul_6.transform), game.getColumn(game.nazgul_6.transform)] = game.nazgul_6;
            game.positions[game.getRow(game.nazgul_7.transform), game.getColumn(game.nazgul_7.transform)] = game.nazgul_7;
            game.positions[game.getRow(game.nazgul_8.transform), game.getColumn(game.nazgul_8.transform)] = game.nazgul_8;

        

        }
        else
        {
            //Setting Player 
            game.player = Players.PLAYER1;

            //game.turn = BattleSystem.START_NAZGUL;

            nazgulPlayer = PhotonNetwork.PlayerList[0];

            //Adding Pieces
            game.positions[game.getRow(game.witchKing.transform), game.getColumn(game.witchKing.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/witchKing", new Vector3(game.witchKing.transform.position.x, game.witchKing.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_1.transform), game.getColumn(game.nazgul_1.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul1", new Vector3(game.nazgul_1.transform.position.x, game.nazgul_1.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_2.transform), game.getColumn(game.nazgul_2.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul2", new Vector3(game.nazgul_2.transform.position.x, game.nazgul_2.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_3.transform), game.getColumn(game.nazgul_3.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul3", new Vector3(game.nazgul_3.transform.position.x, game.nazgul_3.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_4.transform), game.getColumn(game.nazgul_4.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul4", new Vector3(game.nazgul_4.transform.position.x, game.nazgul_4.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_5.transform), game.getColumn(game.nazgul_5.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul5", new Vector3(game.nazgul_5.transform.position.x, game.nazgul_5.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_6.transform), game.getColumn(game.nazgul_6.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul6", new Vector3(game.nazgul_6.transform.position.x, game.nazgul_6.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_7.transform), game.getColumn(game.nazgul_7.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul7", new Vector3(game.nazgul_7.transform.position.x, game.nazgul_7.transform.position.y, 0), Quaternion.identity);
            game.positions[game.getRow(game.nazgul_8.transform), game.getColumn(game.nazgul_8.transform)] = PhotonNetwork.Instantiate("Pieces/Nazgul/nazgul8", new Vector3(game.nazgul_8.transform.position.x, game.nazgul_8.transform.position.y, 0), Quaternion.identity);


            //Adding opponents pieces positions
            game.positions[game.getRow(game.gandalf.transform), game.getColumn(game.gandalf.transform)] = game.gandalf;
            game.positions[game.getRow(game.aragorn.transform), game.getColumn(game.aragorn.transform)] = game.aragorn;
            game.positions[game.getRow(game.frodo.transform), game.getColumn(game.frodo.transform)] = game.frodo;
            game.positions[game.getRow(game.sam.transform), game.getColumn(game.sam.transform)] = game.sam;
            game.positions[game.getRow(game.merry.transform), game.getColumn(game.merry.transform)] = game.merry;
            game.positions[game.getRow(game.pippin.transform), game.getColumn(game.pippin.transform)] = game.pippin;
            game.positions[game.getRow(game.legolas.transform), game.getColumn(game.legolas.transform)] = game.legolas;
            game.positions[game.getRow(game.boromir.transform), game.getColumn(game.boromir.transform)] = game.boromir;
            game.positions[game.getRow(game.gimli.transform), game.getColumn(game.gimli.transform)] = game.gimli;



        }

    }
    // -------------------------------------    New Player  --------------------------------------- //
        public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        newPlayer.NickName = "PLAYER2";
        //chatlog.text += newPlayer.NickName + " entered the room";

        






    }


    // -------------------------------------    Player Leaving Room  --------------------------------------- //
    public override void OnPlayerLeftRoom(Player otherPlayer)
    { 
        //chatlog.text = "Player " + otherPlayer.NickName + " left the room.";
    }

    // -------------------------------------    Host leave room  --------------------------------------- //

    public override void OnLeftRoom()
    {
        //chatlog.text = "Host left the room!";
    }

    // -------------------------------------    Retuning Connection Error  --------------------------------------- //

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        //chatlog.text = "Error to connect: " + errorInfo.Info;
    }


    private void Update()
    {
        
    }


   

    // Getters ans Setters
    public bool pieceSelected(GameObject selection)
    {
        if (selection.GetComponent<PhotonView>().IsMine)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
