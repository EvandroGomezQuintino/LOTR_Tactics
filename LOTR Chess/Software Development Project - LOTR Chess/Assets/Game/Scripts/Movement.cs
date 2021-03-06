using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private int actions = 0;
    public GameController game;
    //public PathMoveSet moveSet;

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
     



    }



    public void clickObj(GameObject obj)
    {
        objSelected = obj;
        //Converting world position into game position
        int objXposition = game.xPos(objSelected.transform);
        int objYposition = game.yPos(objSelected.transform);
        Debug.LogError("X "+objXposition);
        Debug.LogError("Y "+objYposition);



        // Getting combat tiles and destroying them 
        GameObject[] combatTile = GameObject.FindGameObjectsWithTag("Tile_Combat");

        if(combatTile != null)
        {
           foreach(GameObject tile in combatTile)
            {
                Destroy(tile);
            }
        }

        if (previousPiece != null)
        {
            foreach(Transform child in previousPiece.transform)
            {
                if(child.tag == "TileSelected" || child.tag == "Tile_Movement")
                {
                    child.gameObject.SetActive(false);
                    foreach(Transform child2 in child.transform)
                    {
                        child2.gameObject.SetActive(false);
                        foreach (Transform child3 in child2.transform)
                        {
                            child3.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        //Selecting pieces
        if (objSelected.tag == "Pieces")
            {

                // Loop checking children and executign properly actions
                foreach(Transform child in objSelected.transform)
                {
                    //Showing up tile selected
                    if(child.tag == "TileSelected")
                    {
                        child.gameObject.SetActive(true);
                    }
                    //Showing up the tiles for movement
                    if (child.tag =="Tile_Movement")
                    {


                    //Checking with position is inside the board
                    if (validMovement(child))
                    {
                        //Getting children position in relation to the board
                        int childXPosition = game.xPos(child);
                        int childYPosition = game.yPos(child);

                        //Children are dividided by levels. A Upper level is parent to a lower level.
                        //Showing up tile movements if tile is empty
                        if (emptySpace(childXPosition, childYPosition))
                        {
                            child.gameObject.SetActive(true);

                            foreach (Transform child2 in child.transform)
                            {
                                
                                if (validMovement(child2))
                                {
                                    childXPosition = game.xPos(child2);
                                    childYPosition = game.yPos(child2);

                                    //Checking second level of children
                                    if (emptySpace(childXPosition, childYPosition))
                                    {
                                        child2.gameObject.SetActive(true);

                                        //Checking third level of children
                                        foreach (Transform child3 in child2.transform)
                                        {
  
                                            if (validMovement(child3))
                                            {
                                                childXPosition = game.xPos(child3);
                                                childYPosition = game.yPos(child3);

                                                //Testing                                                    
                                                //Debug.LogError("Position X" + childXPosition + "Y:" + childYPosition);


                                                if (emptySpace(childXPosition, childYPosition))
                                                {
                                                    child3.gameObject.SetActive(true);
                                                }
                                                else
                                                {
                                                    child3.gameObject.SetActive(false);
                                                }

                                            }
                                            else
                                            {
                                                child3.gameObject.SetActive(false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            //    //Testing printing positions
            //    foreach(GameObject testt in game.positions)
            //{
            //    if(testt != null)
            //    {
            //        Debug.LogError(testt.ToString() + "GB name : " +game.positions[9,4]);
            //    }

            //}


                //Saving last piece clicked
                previousPiece = objSelected;
            }

        //Selecting Tiles for movement
        else if (objSelected.tag == "Tile_Movement" && previousPiece != null)
            {

            int xPrevPosition = game.xPos(previousPiece.transform);
            int yPrevPosition = game.yPos(previousPiece.transform);

            //Testing
            //Debug.LogError("Previous: "+previousPiece.ToString());
            //Debug.LogError("ObjSelected:"+objSelected.ToString());


            //TODO: User selects a Tile_Movement
            //TODO: previous Position[x,y] is set to null
            //TODO: new Position[x,y] is set
            //TODO: TileSelected and TileMovement are destroyed
            //TODO: Action is increased

            //Set new Position
            previousPiece.transform.position = new Vector2(objSelected.transform.position.x,objSelected.transform.position.y);

            //Clean Movement
            //cleanPathMovement(previousPiece.transform);

            //Previous position is set to empty and new position is added
            game.positions[xPrevPosition, yPrevPosition] = null;
            game.positions[objXposition, objYposition] = previousPiece;

            // If Frodo reaches the MountDoom
            if (previousPiece.transform.GetChild(0).tag == "frodo" && objXposition == 9 && objYposition == 10)
            {
                // Loading Scene showing that Heroes Won
                SceneManager.LoadScene("HeroesWon", LoadSceneMode.Single);

            }

            actions++;
            Debug.LogError("ACTIONS:" + actions);


        }

        // Selecting tile where enemy is present
        else if(objSelected.tag == "Tile_Combat")
        {
            // Board position for the Tile_Combat selected
            int BoardxPos = game.xPos(objSelected.transform);
            int BoardyPos = game.yPos(objSelected.transform);

            // Original board position for the piece in movement
            int PieceOriginalXpos = game.xPos(previousPiece.transform);
            int PieceOriginalypos = game.yPos(previousPiece.transform);

            //Getting enemy piece
            GameObject enemy = game.positions[BoardxPos, BoardyPos];
            
            //Moving piece
            previousPiece.transform.position = new Vector2(objSelected.transform.position.x, objSelected.transform.position.y);
            
            //Updating board array with movement
            game.positions[BoardxPos, BoardyPos] = previousPiece;
            game.positions[PieceOriginalXpos, PieceOriginalypos] = null;

            //Fininishing combat action
            if(enemy.transform.GetChild(0).tag !="frodo")
            {
                Destroy(enemy);
            }
            // Frodo dying results in Mordors victory
            else
            {
                SceneManager.LoadScene("NazgulWon", LoadSceneMode.Single);
            }

            actions++;
        }




        // Checking if two actions were used per player
        if (actions == 2)
        {
            if (game.player == Players.PLAYER1)
            {
                game.turn = BattleSystem.HEROES;
                game.heroesTurn();
                actions = 0;
            }
            else if (game.player == Players.PLAYER2)
            {
                game.turn = BattleSystem.NAZGUL;
                game.nazgulTurn();
                actions = 0;
            }
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

    //Checking if position is empty
    public bool emptySpace(int xPos, int yPos)
    {
        int x = xPos;
        int y = yPos;

        //testing
        //Debug.LogError("POSITION X:" + x + " Y: " + y);
        //Debug.LogError("Empty Poistion:"+ game.positions[x, y]);

        if (game.positions[x,y] == null || game.positions[x,y] == GameObject.FindGameObjectWithTag("MountDoom"))
        {
            return true;
        }
        else
        {

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

}
