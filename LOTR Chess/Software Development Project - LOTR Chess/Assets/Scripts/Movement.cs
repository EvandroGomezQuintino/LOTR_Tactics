using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;

public class Movement : MonoBehaviourPun
{ 
    private int actions = 0;
    public GameController game;
    public Connection multiplayer;
    //public PathMoveSet moveSet;
    public PhotonView photonView;


    public HiddenSystem viewSystem;

    //Multiplayer
    //Saving new position
    public int newXPos;
    public int newYPos;
    //Saving old position
    public int xPrevPosition;
    public int yPrevPosition;


    //Tiles
    //public GameObject tileSelected;
    //public GameObject tileMovement;
    public GameObject tileCombat;

    //Selection
    private GameObject objSelected;

    //Recording Movement
    private GameObject previousPiece;

    public void Start()
    {
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        multiplayer = GetComponent<Connection>();

        viewSystem = GameObject.FindWithTag("GameController").GetComponent<HiddenSystem>();


    }



    public void clickObj(GameObject obj)
    {


        objSelected = obj;

        //Converting world position into game position
        newXPos = game.getRow(objSelected.transform);
        newYPos = game.getColumn(objSelected.transform);


        // Getting combat tiles and destroying them 
        GameObject[] combatTile = GameObject.FindGameObjectsWithTag("Tile_Combat");

        if (combatTile != null)
        {
            foreach (GameObject tile in combatTile)
            {
                Destroy(tile);
            }
        }

        // Deactivating tiles after moving a piece
        if (previousPiece != null)
        {
            foreach (Transform child in previousPiece.transform)
            {
                if (child.tag == "TileSelected" || child.tag == "MountDoom")
                {
                    child.gameObject.SetActive(false);

                }
                else if(child.tag == "Movement")
                {
                    foreach(Transform tile in child)
                    {
                        tile.gameObject.SetActive(false);
                    }
                }
            }
        }

        //Selecting pieces
        if (objSelected.tag == "Pieces" && game.pieceUsed(objSelected) == false)
        {

            // Loop checking children and executign properly actions
            foreach (Transform child in objSelected.transform)
            {
                //Showing up tile selected
                if (child.tag == "TileSelected")
                {
                    child.gameObject.SetActive(true);
                }

                //Showing up the tiles for movement
                if (child.tag == "Movement")
                {

                    foreach(Transform tile in child)
                    {
                        if (validMovement(tile))
                        {
                            int childXPosition = game.getRow(tile);
                            int childYPosition = game.getColumn(tile);

                            if(emptySpace(childXPosition, childYPosition))
                            {
                                tile.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            tile.gameObject.SetActive(false);
                        }
                    }
                                 
                
                
                }

                ////Displaying entrance to MountDoom
                if (objSelected.transform.GetChild(0).tag == "frodo")
                {

                    game.RingPointer.SetActive(true);
                    game.RingPointer.GetComponent<Animator>().SetTrigger("Frodo_Selected");
                }
                else
                {
                    game.RingPointer.SetActive(false);
                }
            }


            //Saving last piece clicked
            previousPiece = objSelected;
        }




        //Selecting Tiles for movement
        else if (objSelected.tag == "Tile_Movement" && previousPiece != null)
        {
            // Execute movement
            movePiece();



            //Multiplayer Mode
            if (game.gameModeMultiplayer == true)
            {

                // Update Vision
                viewSystem.checkVision();

                // Simply storing pieces positions and GameObject ID
                int[] positionList = new int[5];
                positionList[0] = xPrevPosition;
                positionList[1] = yPrevPosition;
                positionList[2] = newXPos;
                positionList[3] = newYPos;
                positionList[4] = previousPiece.GetPhotonView().ViewID;


                // Updating new position for other players
                photonView.RPC("updatePos", RpcTarget.Others, positionList);
                
            }

            // If Frodo reaches the MountDoom
            if (previousPiece.transform.GetChild(0).tag == "frodo" && newXPos == 9 && newYPos == 10)
            {
                // Displaying GameOver UI                
                game.gameOver(true);
                Debug.Log("END GAME");
            }
            // Increase actions per turn
            actions++;
        }

        // Selecting tile where enemy is present
        else if (objSelected.tag == "Tile_Combat")
        {
            
            //Getting enemy piece
            GameObject enemy = game.positions[newXPos, newYPos];

            // Execute movement
            movePiece();

            // Removing piece
            if (enemy.transform.GetChild(0).tag != "frodo")
            {
                Destroy(enemy);

                if (game.gameModeMultiplayer == true)
                    photonView.RPC("destroyPiece", RpcTarget.Others, enemy.GetPhotonView().ViewID);

            }

            // Frodo dying results in Nazgul victory
            else
            {
                Destroy(enemy);
                // Displaying GameOver UI
                game.gameOver(false);
            }

            //Increase actions per turn
            actions++;
        }




        // Checking if two actions were used per player
        if (actions == 2)
        {
            // Clearing list of pieces used this turn
            game.clearPieces();


            //Local Game GameMode
            if (!game.gameModeMultiplayer)
            {
                if (game.player == Players.PLAYER1)
                {
                    // Change turn
                    game.turn = BattleSystem.HEROES;
                    game.heroesTurn();

                }
                else if (game.player == Players.PLAYER2)
                {
                    // Deactive ring animation if Frodo was the last piece moved
                    game.RingPointer.SetActive(false);

                    // Change turn
                    game.turn = BattleSystem.NAZGUL;
                    game.nazgulTurn();

                }
            }

            //Multiplayer GameMode
            else
            {
                viewSystem.checkVision();
                //Changing Player's turn
                if (game.turn == BattleSystem.NAZGUL)
                {

                    

                    photonView.RPC("changeTurn", RpcTarget.All, game.turn);
                    //multiplayer.changeTurn(game.turn);


                }
                else if (game.turn == BattleSystem.HEROES)

                {
                    photonView.RPC("changeTurn", RpcTarget.All, game.turn);
                    //multiplayer.changeTurn(game.turn);


                }

            }
        
        // Reseting actions
        actions = 0;

        }
    }

    //Checking if Position is valid (inside the board)
    public bool validMovement(Transform obj)
    {
        if (obj.transform.position.x >= -6.5f && obj.transform.position.x <= 6.5f && obj.transform.position.y >= -6.5f && obj.transform.position.y <= 6.5f)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    //Move piece and Update BoardGame positions
    public void movePiece()
    {

        // Piece can't be used again this turn
        game.addPiece(previousPiece);

        // Getting Piece's position
        xPrevPosition = game.getRow(previousPiece.transform);
        yPrevPosition = game.getColumn(previousPiece.transform);

        //Set new Position
        previousPiece.transform.position = new Vector2(objSelected.transform.position.x, objSelected.transform.position.y);

        //Previous position is set to empty and new position is added
        game.positions[xPrevPosition, yPrevPosition] = null;
        game.positions[newXPos, newYPos] = previousPiece;


    }




    //****************** Multiplayer Functions ******************


    //Getting Players pieces positions
    [PunRPC]
    //Checking if position is empty
    public bool emptySpace(int xPos, int yPos)
    {
        int x = xPos;
        int y = yPos;

        //testing
        //Debug.LogError("POSITION X:" + x + " Y: " + y);
        //Debug.LogError("Empty Poistion:"+ game.positions[x, y]);

        // GameMode Local

        if (game.positions[x, y] == null || game.positions[x, y] == GameObject.FindGameObjectWithTag("MountDoom"))
        {
            return true;
        }
        //Avoiding piece to move to The Eye tile
        else if (game.positions[x, y] == GameObject.FindGameObjectWithTag("Eye"))
        {
            return false;
        }
        else
        {

            //Test
            //Debug.Log("Testing position " + game.positions[x, y].ToString());

            combatTile(x, y);
            return false;
        }


    }


    public void combatTile(int x, int y)
    {

        string tag_ObjSelected = objSelected.transform.GetChild(1).tag;
        string tag_enemy = game.positions[x, y].gameObject.transform.GetChild(1).tag;
        //GameObject enemy = game.positions[x, y].gameObject;
        float combatXPos = game.positions[x, y].gameObject.transform.position.x;
        float combatYPos = game.positions[x, y].gameObject.transform.position.y;

        if (tag_ObjSelected != tag_enemy)
        {
            //enemy.transform.GetChild(2).GetComponentInChildren<SpriteRenderer>().color = new Color("FF0000");

            Instantiate(tileCombat, new Vector2(combatXPos, combatYPos), Quaternion.identity);
        }



    }


    // Updating current status for all players
    [PunRPC]
    public void changeTurn(BattleSystem turn)
    {
        // Changing Player's turn
        if (turn == BattleSystem.NAZGUL)
        {
            game.turn = BattleSystem.HEROES;
            game.heroesTurnUI.SetActive(true);
            game.nazgulTurnUI.SetActive(false);
        }
        // Changing Player's turn
        else if (turn == BattleSystem.HEROES)
        {
            game.turn = BattleSystem.NAZGUL;
            game.nazgulTurnUI.SetActive(true);
            game.heroesTurnUI.SetActive(false);
        }
    }

    // Update Pieces Positions across all players
    [PunRPC]
    public void updatePos(int [] pos)
    {


        game.positions[pos[0], pos[1]] = null;
        //Debug.Log(game.positions[newXPos, newYPos].name);
        game.positions[pos[2], pos[3]] = PhotonView.Find(pos[4]).gameObject;



        //foreach (GameObject obj in game.positions)
        //{

        //    if (obj != null)
        //    {
        //        if (obj.name == PhotonView.Find(objID).name)
        //        {
        //            Debug.Log(obj.name + obj.GetComponent<PhotonView>().ViewID);


        //            game.positions[xPrevPosition, yPrevPosition] = null;
        //            //Debug.Log(game.positions[newXPos, newYPos].name);
        //            game.positions[newXPos, newYPos] = ;


        //        }

        //        //// Used for testing
        //        //if (obj.name == "witchKing")
        //        //{
        //        //    Debug.Log(obj.name + obj.transform.position.x + obj.transform.position.y);
        //        //}

        //    }
    }


    // Removing piece from game
    [PunRPC]
    public void destroyPiece(int pieceID)
    {
        // Finding piece 
       foreach(GameObject piece in game.positions)
        {
            if(piece != null)
            {
                if (piece == PhotonView.Find(pieceID).gameObject)
                    Destroy(piece);
            }
        }
    }






}