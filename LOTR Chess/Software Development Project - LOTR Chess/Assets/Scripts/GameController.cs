using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public enum BattleSystem { START, NAZGUL, HEROES, GAMEOVER, START_NAZGUL, START_HEROE}
public enum Players { PLAYER1, PLAYER2 }



public class GameController : MonoBehaviour
{ 

    // ************** STEP LIST *****************
    //The game stages
    [Header("State Machine")]
    public BattleSystem turn;
    public Players player;

    //Game Mode
    public bool gameModeMultiplayer;
           
    [Header("Pieces")]
    //Heroes Pieces
    public GameObject gandalf;
    public GameObject aragorn, frodo, sam, merry, pippin, legolas, boromir, gimli;
    //Nazguls
    public GameObject witchKing, nazgul_1, nazgul_2, nazgul_3, nazgul_4, nazgul_5, nazgul_6, nazgul_7, nazgul_8;

    [Header("Pieces_Used")]
    // Contain pieces used in the turn
    List<GameObject> piecesUsed = new List<GameObject>();

    [Header("Other Boardgame elements")]
    // Eye - Nazghul forces can use
    public GameObject theEye;
    // Ring Pointer (where Frodo must reach)
    public GameObject RingPointer;

    //Mapping Boardgame positions X and Y
    private Dictionary<float, int> xBoard;
    private Dictionary<float, int> yBoard;


    //BoardPositions
    public GameObject[,] positions = new GameObject[14, 14];

    [Header("UI")]
    //UI Turn
    public GameObject nazgulTurnUI;
    public GameObject heroesTurnUI;
    public GameObject gameOverUI;


    public HiddenSystem view;

    //Movement
    public GameObject objSelected;
    private Movement _movement;
    public Connection multiplayer;

    void Start()
    {



        //Board mapping
        xBoard = new Dictionary<float, int>();
        yBoard = new Dictionary<float, int>();
        turn = BattleSystem.START;

        // Assigning Scripts
        _movement = GameObject.FindWithTag("Movement").GetComponent<Movement>();
        multiplayer = GameObject.FindWithTag("Multiplayer").GetComponent<Connection>();

        view = this.GetComponent<HiddenSystem>();

        // DELETE FOLLOWING CODE
        // USED WHILE DEVELOPMING THE GAME
        //GameObject.Find("Multiplayer").SetActive(false);
        //gameModeMultiplayer = false;


        //UNCOMMENT THE FOLLOWING CODE
        //USED TO CHECK IF GAME IS LOCAL OR MULTIPLAYER *AFTER* THE MAIN MENU SCENE

        // Checking GameMode selected on MainMenu (Local or Multiplayer)
        if (GameObject.Find("BackGround").GetComponent<DontDestroy>().localGameMode == true)
        {
            GameObject.Find("Multiplayer").SetActive(false);
            gameModeMultiplayer = false;

        }
        else
        {
            gameModeMultiplayer = true;
            //multiplayer = GameObject.FindWithTag("Multiplayer").GetComponent<Connection>();


        }

        //Adding delay when changing the turn
        gameSetup();

        //Delay the beginning of the first player
        turn = BattleSystem.NAZGUL;
        nazgulTurn();
    }

    
    //Update is called once per frame
    void Update()
    {
        
        // Capturing the mouse click
        if (Input.GetMouseButtonDown(0))
            {

            //Registering click position
            Ray ray;
            RaycastHit hitdata;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hitdata, 20))
                {   
                    objSelected = hitdata.transform.gameObject;
                    
                    //Checking if a piece or tile was clicked
                    if (objSelected.tag == "Pieces" || objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat") 
                    {
                        pieceSelection(objSelected);
                    }
                }
                            
            }

        



        //if (Input.GetMouseButtonDown(0) && turn == BattleSystem.NAZGUL && player == Players.PLAYER1)
        //{
        //    //Tracing mouse position and returning GameObject
        //    Ray ray;
        //    RaycastHit hitdata;

        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hitdata, 20))
        //    {

        //        objSelected = hitdata.transform.gameObject;

        //        //Checking if Piece selected belongs to player
        //        if (objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat" || /*multiplayer.pieceSelected(objSelected) == true*/ objSelected.transform.GetChild(1).tag == "Mordor")
        //        {
        //            //Debug.LogError(objSelected.ToString());
        //            //Debug.LogError("PLAYER 1 - NAZGUL");
        //            _movement.clickObj(objSelected);
        //        }
        //    }
        //}
        //else if (Input.GetMouseButtonDown(0) && turn == BattleSystem.HEROES && player == Players.PLAYER2)
        //{
        //    // Tracing mouse position and returning GameObject 
        //    Ray ray;
        //    RaycastHit hitdata;

        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hitdata, 20))
        //    {
        //        objSelected = hitdata.transform.gameObject;


        //        //Checking if Piece selected belongs to player
        //        if (objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat" || objSelected.transform.GetChild(1).tag == "Heroes")
        //        {
        //            //Debug.LogError(objSelected.ToString());
        //            //Debug.LogError("PLAYER 1 - HEROES");
        //            _movement.clickObj(objSelected);
        //        }
        //    }
        //}


    }


    public void gameSetup()
    {
        loadBoardPositions();



        if (!gameModeMultiplayer)
        {
            //Mapping pieces in the boardgame and Instiating GameObjects
            positions[getRow(gandalf.transform), getColumn(gandalf.transform)] = Instantiate(gandalf);
            positions[getRow(aragorn.transform), getColumn(aragorn.transform)] = Instantiate(aragorn);
            positions[getRow(frodo.transform), getColumn(frodo.transform)] = Instantiate(frodo);
            positions[getRow(sam.transform), getColumn(sam.transform)] = Instantiate(sam);
            positions[getRow(merry.transform), getColumn(merry.transform)] = Instantiate(merry);
            positions[getRow(pippin.transform), getColumn(pippin.transform)] = Instantiate(pippin);
            positions[getRow(legolas.transform), getColumn(legolas.transform)] = Instantiate(legolas);
            positions[getRow(boromir.transform), getColumn(boromir.transform)] = Instantiate(boromir);
            positions[getRow(gimli.transform), getColumn(gimli.transform)] = Instantiate(gimli);
            positions[getRow(witchKing.transform), getColumn(witchKing.transform)] = Instantiate(witchKing);
            positions[getRow(nazgul_1.transform), getColumn(nazgul_1.transform)] = Instantiate(nazgul_1);
            positions[getRow(nazgul_2.transform), getColumn(nazgul_2.transform)] = Instantiate(nazgul_2);
            positions[getRow(nazgul_3.transform), getColumn(nazgul_3.transform)] = Instantiate(nazgul_3);
            positions[getRow(nazgul_4.transform), getColumn(nazgul_4.transform)] = Instantiate(nazgul_4);
            positions[getRow(nazgul_5.transform), getColumn(nazgul_5.transform)] = Instantiate(nazgul_5);
            positions[getRow(nazgul_6.transform), getColumn(nazgul_6.transform)] = Instantiate(nazgul_6);
            positions[getRow(nazgul_7.transform), getColumn(nazgul_7.transform)] = Instantiate(nazgul_7);
            positions[getRow(nazgul_8.transform), getColumn(nazgul_8.transform)] = Instantiate(nazgul_8);


            


        }
        
        //Adding Eye
        positions[7,13] = Instantiate(theEye);





        //TESTING x,y POSITIONS
        //Debug.LogError("X:" + xPos(_gandalf));
        //Debug.LogError("Y:" + yPos(_gandalf));
        //Debug.LogError("-----------------");
        //Debug.LogError("X:" + xPos(legolas));
        //Debug.LogError("Y:" + yPos(legolas));
        //Debug.LogError("-----------------");
        //Debug.LogError("X:" + xPos(nazgul_4));
        //Debug.LogError("Y:" + yPos(nazgul_4));
        //Debug.LogError("-----------------");
        //Debug.LogError("X:" + xPos(witchKing));
        //Debug.LogError("Y:" + yPos(witchKing));
        //Debug.LogError(positions[2, 2].name);
        //Debug.LogError(positions[1, 1].name);
        //Debug.LogError(positions[1, 2].name);
        //Debug.LogError(positions[0, 13].name);
        //Debug.LogError(positions[1, 12].name);
        //Debug.LogError(positions[2, 11].name);

    }


    // Mordor Player turn
    public void nazgulTurn()
    {
        // Changing UI
        nazgulTurnUI.SetActive(true);
        heroesTurnUI.SetActive(false);

        // If Local GameMode, Player1 is set here
        if (!gameModeMultiplayer)
        {
            player = Players.PLAYER1;
        }


    }


    public void heroesTurn()
    {
        
        // Changing UI
        heroesTurnUI.SetActive(true);
        nazgulTurnUI.SetActive(false);
        

        // If Local GameMode, Player1 is set here
        if (!gameModeMultiplayer)
        {
            player = Players.PLAYER2;
        }


    }


    // Loading dictionaries with World Position
    private void loadBoardPositions()
    {
        for (int i = 0; i < 14; i++)
        {
            xBoard.Add(6.5f - i, i);
            yBoard.Add(-6.5f + i, i);


        }
    }

    //ATTENTION!
    // The Trnasform.Position is different for the board position which uses [row][column]
    // This methods conver the world position 'Y' into [row], so the game is based on the board position.

    //Converts 'Y' world position into [row]
    public int getRow(Transform obj)
    {
        Transform objXPosition = obj;
        int xPosition = xBoard[objXPosition.GetComponent<Transform>().position.y];
        return xPosition;
    }

    //Converts 'X' world position into [column]
    public int getColumn(Transform obj)
    {
        Transform objYPosition = obj;
        int yPosition = yBoard[objYPosition.GetComponent<Transform>().position.x];
        return yPosition;
    }

    // Return piece based on coordenates X,Y
    public GameObject returnPiece(int x, int y)
    {
        GameObject piece = positions[x, y];
        if (piece != null)
            return piece;
        else
            return null;
    }



    //Invoking GameOver UI and buttons
    public void gameOver(bool victory)
    {
        // True = Heroes won
        // False = Nazgul won

        // Setting turn and blocking further player movements
        turn = BattleSystem.GAMEOVER;

        if (victory)
        {
         
            // Displaying GameOver UI and options
            gameOverUI.SetActive(true);
            gameOverUI.transform.GetChild(0).gameObject.SetActive(true);

         
        }
        else
        {
            // Displaying GameOver UI and options
            gameOverUI.SetActive(true);
            gameOverUI.transform.GetChild(1).gameObject.SetActive(true);

        }


    }




   
    public void pieceSelection(GameObject obj_selected)
    {
        // Getting object clicked
        objSelected = obj_selected;

        //Multiplayer Mode
        if (gameModeMultiplayer)
        {
           
            if (turn == BattleSystem.NAZGUL && player == Players.PLAYER1 || turn == BattleSystem.HEROES && player == Players.PLAYER2 /*&& multiplayer.nazgulControoler(objSelected)==true*/)
            {
                    
                //Checking if Piece selected belongs to player
                if (objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat" || multiplayer.pieceSelected(objSelected) == true)
                {
                    //Executing Movement
                    _movement.clickObj(objSelected);
                }
            }
        }

        //Local Mode
        else
        {
            // Checking if Player matches his turn
            if (turn == BattleSystem.NAZGUL && player == Players.PLAYER1)
            {
                //Checking if Piece selected belongs to player
                if (objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat" || objSelected.transform.GetChild(1).tag == "Mordor")
                {
                    //Executing Movement
                    _movement.clickObj(objSelected);
                }
            }

            // Checking if Player matches his turn
            else if ( turn == BattleSystem.HEROES && player == Players.PLAYER2)
            {
                //Checking if Piece selected belongs to player
                if (objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat" || objSelected.transform.GetChild(1).tag == "Heroes" )
                {
                    //Executing Movement
                    _movement.clickObj(objSelected);
                }
            }

        }
   
    }


    // Adding pieces to the list
    public void addPiece(GameObject obj)
    {
        piecesUsed.Add(obj);
    }

    // Clearing list
    public void clearPieces()
    {
        piecesUsed.Clear();
    }

    // Checking if piece is present in the list
    public bool pieceUsed(GameObject obj)
    {
        if (piecesUsed.Contains(obj))
            return true;
        else
            return false;
    }


}

