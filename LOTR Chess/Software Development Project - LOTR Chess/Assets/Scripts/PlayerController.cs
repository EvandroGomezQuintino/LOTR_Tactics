using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerController : MonoBehaviourPun
{
    // keep track of players
    public Player photonPlayer;
    public string[] piecesToSpawn;

    public List<GameController> pieces = new List<GameController>();


    public static PlayerController masterPlayer;
    public static PlayerController localPlayer;



    public void Start()
    {
        // Sendting 
        //photonView.RPC("Initialize",RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(0));
      
    }
     

    [PunRPC]
    void Initialize (Player player)
    {
        photonPlayer = player;

        if (player.IsMasterClient)
        {
            masterPlayer = this;
            
        }
        else
        {
            localPlayer = this;
        }
            
    }


    void spawnPieces(Player player)
    {
        if (player.IsMasterClient)
        {

        }

    }
}
