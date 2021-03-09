using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleSystem { START, NAZGUL, HEROES, WON, LOST }
public enum Players { PLAYER1, PLAYER2 }



public class GameController : MonoBehaviour
{
    // ************** STEP LIST *****************
    // DONE: Check Physics.RayCast2D to check mouse click and object interaction
    // DONE: Organize classes for: Turn, Pieces
    // DONE: Need to create a matrix for the board
    // DONE: Need to map the positions 
    // DONE: Check turns
    // DONE: Fix TileSelected respawn
    // DONE: Destroy TileMovement gameObjects
    // DONE: Need to differentiate the pieces for each player, so Player 1 cant choose player 2 pieces
    // DONE: Implemented combat system
    // DONE: Add the Montain Doom position
    // DONE: Add the Eye element
    // DONE: Add the winning conditions for both players
    // DONE: Add sprites for each piece
    // DONE: Prepare email to Joe Roe about my project.The idea and which stage I am
    // TODO: Add Multiplayer system
    // TODO: Clean up code
    // TODO: Add Canvas for Player's turn
    // TODO: Add Scene for the main menu
    // TODO: Finish Tilemap
    // TODO: Add Audio
    // TODO: Add Music
    // TODO: Start Game Documentation
    // TODO: Review movement set for the pieces (specially hobbits)










    // ************** STEP LIST *****************
    //The game stages
    public BattleSystem turn;
    public Players player;

    //Heroes Pieces
    public GameObject _gandalf, aragorn, frodo, sam, merry, pippin, legolas, boromir, gimli;
    //Nazguls
    public GameObject witchKing, nazgul_1, nazgul_2, nazgul_3, nazgul_4, nazgul_5, nazgul_6, nazgul_7, nazgul_8;
    //Other elements
    public GameObject mountDoom;
    public GameObject theEye;

    //Mapping Boardgame positions X and Y
    private Dictionary<float, int> xBoard;
    private Dictionary<float, int> yBoard;


    //BoardPositions
    public GameObject[,] positions = new GameObject[14, 14];


    //Movement
    public GameObject objSelected;
    private Movement _movement;

    void Start()
    {

        //Board mapping
        xBoard = new Dictionary<float, int>();
        yBoard = new Dictionary<float, int>();


        turn = BattleSystem.START;
        _movement = GameObject.FindWithTag("Movement").GetComponent<Movement>();
        //Adding delay when changing the turn
        gameSetup();

        //Delay the beginning of the first player
        turn = BattleSystem.NAZGUL;
        nazgulTurn();
    }

    //Update is called once per frame
    void Update()
    {
        //TESTING IENUMERATOR IN THE MOVEMENT FUNCTION

        if (Input.GetMouseButtonDown(0) && turn == BattleSystem.NAZGUL && player == Players.PLAYER1)
        {
            //Tracing mouse position and returning GameObject
            Ray ray;
            RaycastHit hitdata;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitdata, 20))
            {

                objSelected = hitdata.transform.gameObject;

                //Checking if Piece selected belongs to player
                if(objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat" || objSelected.transform.GetChild(1).tag == "Mordor")
                {
                    //Debug.LogError(objSelected.ToString());
                    Debug.LogError("PLAYER 1 - NAZGUL");
                    _movement.clickObj(objSelected);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && turn == BattleSystem.HEROES && player == Players.PLAYER2)
        {
            // Tracing mouse position and returning GameObject 
            Ray ray;
            RaycastHit hitdata;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitdata, 20))
            {
                objSelected = hitdata.transform.gameObject;

                //Checking if Piece selected belongs to player
                if (objSelected.tag == "Tile_Movement" || objSelected.tag == "Tile_Combat" || objSelected.transform.GetChild(1).tag == "Heroes" )
                {
                    //Debug.LogError(objSelected.ToString());
                    Debug.LogError("PLAYER 1 - HEROES");
                    _movement.clickObj(objSelected);
                }
            }
        }


    }


    public void gameSetup()
    {
        loadBoardPositions();

        //Mapping pieces in the boardgame and Instiating GameObjects
        positions[xPos(_gandalf.transform), yPos(_gandalf.transform)] = Instantiate(_gandalf);
        positions[xPos(aragorn.transform), yPos(aragorn.transform)] = Instantiate(aragorn);
        positions[xPos(frodo.transform), yPos(frodo.transform)] = Instantiate(frodo);
        positions[xPos(sam.transform), yPos(sam.transform)] = Instantiate(sam);
        positions[xPos(merry.transform), yPos(merry.transform)] = Instantiate(merry);
        positions[xPos(pippin.transform), yPos(pippin.transform)] = Instantiate(pippin);
        positions[xPos(legolas.transform), yPos(legolas.transform)] = Instantiate(legolas);
        positions[xPos(boromir.transform), yPos(boromir.transform)] = Instantiate(boromir);
        positions[xPos(gimli.transform), yPos(gimli.transform)] = Instantiate(gimli);
        positions[xPos(witchKing.transform), yPos(witchKing.transform)] = Instantiate(witchKing);
        positions[xPos(nazgul_1.transform), yPos(nazgul_1.transform)] = Instantiate(nazgul_1);
        positions[xPos(nazgul_2.transform), yPos(nazgul_2.transform)] = Instantiate(nazgul_2);
        positions[xPos(nazgul_3.transform), yPos(nazgul_3.transform)] = Instantiate(nazgul_3);
        positions[xPos(nazgul_4.transform), yPos(nazgul_4.transform)] = Instantiate(nazgul_4);
        positions[xPos(nazgul_5.transform), yPos(nazgul_5.transform)] = Instantiate(nazgul_5);
        positions[xPos(nazgul_6.transform), yPos(nazgul_6.transform)] = Instantiate(nazgul_6);
        positions[xPos(nazgul_7.transform), yPos(nazgul_7.transform)] = Instantiate(nazgul_7);
        positions[xPos(nazgul_8.transform), yPos(nazgul_8.transform)] = Instantiate(nazgul_8);
        //Adding MountDoom and the Eye
        positions[xPos(mountDoom.transform), yPos(mountDoom.transform)] = Instantiate(mountDoom);
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

        // TODO:    Update UI Message
        StartCoroutine(displayMessage());

        // User will be able to click after here
        player = Players.PLAYER1;


    }


    public void heroesTurn()
    {
        // TODO:    Update UI Message
        StartCoroutine(displayMessage());

        // User will be able to click after here
        player = Players.PLAYER2;


    }



    IEnumerator displayMessage()
    {
        //TODO: Check turn and display players turn
        if (turn == BattleSystem.NAZGUL)
        {
            //TODO: Display Mordor Turn
            yield return new WaitForSeconds(2f);
        }
        else if (turn == BattleSystem.HEROES)
        {
            //TODO: Display LOTR Turn
            yield return new WaitForSeconds(2f);
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
    public int xPos(Transform obj)
    {
        Transform objXPosition = obj;
        int xPosition = xBoard[objXPosition.GetComponent<Transform>().position.y];
        return xPosition;
    }

    //Converts 'X' world position into [column]
    public int yPos(Transform obj)
    {
        Transform objYPosition = obj;
        int yPosition = yBoard[objYPosition.GetComponent<Transform>().position.x];
        return yPosition;
    }
}

