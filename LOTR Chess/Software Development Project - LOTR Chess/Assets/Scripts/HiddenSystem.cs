using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HiddenSystem : MonoBehaviourPun
{
    public GameController game;


    public List<GameObject> nazgulList = new List<GameObject>();
    public List<GameObject> heroesList = new List<GameObject>();

    public List<GameObject> visionArea = new List<GameObject>();


    public Movement move;


    public void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        move = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();
       


    }

    [PunRPC]
    public void checkVision()
    {
        //Updating piece's list
        updateLists();


        if (game.turn == BattleSystem.NAZGUL )
        {

            // Checking all heroes inside nazgul vision area
            updateVisionArea();

            // Looping through heroes
            foreach(GameObject heroe in heroesList)
            {

                PieceController pieceControl = heroe.GetComponent<PieceController>();

                // For each heroe in nazgul vision area, set visible
                if (visionArea.Contains(heroe))
                {
                    pieceControl.visible();
                }
                // Set not visiible
                else
                    pieceControl.nOTvisible();

            }


        }

        else if (game.turn == BattleSystem.HEROES )
        {

            // Checking all heroes inside nazgul vision area
            updateVisionArea();

            // Looping through heroes
            foreach (GameObject nazgul in nazgulList)
            {

                PieceController pieceControl = nazgul.GetComponent<PieceController>();

                // For each heroe in nazgul vision area, set visible
                if (visionArea.Contains(nazgul))
                {
                    pieceControl.visible();
                }
                // Set not visiible
                else
                    pieceControl.nOTvisible();

            }



        }


    }


    public void updateVisionArea()
    {
        // Reseting vision area
        visionArea.Clear();

        if(game.turn == BattleSystem.NAZGUL)
        {
            // get all heroes in the visionarea
            foreach(GameObject nazgul in nazgulList)
            {

                foreach (Transform child in nazgul.transform)
                {
                    foreach (GameObject heroe in heroesList)
                    {
                        int heroeRow = game.getRow(heroe.transform);
                        int heroeColumn = game.getColumn(heroe.transform);
                        PieceController pieceControl = heroe.GetComponent<PieceController>();


                        if (child.tag == "Vision")
                        {

                            foreach (Transform tile in child)
                            {

                                if (move.validMovement(tile))
                                {
                                    int visionRow = game.getRow(tile.transform);
                                    int visionColumn = game.getColumn(tile.transform);

                                    // Checking if heroe is inside the area vision
                                    if (visionRow == heroeRow && visionColumn == heroeColumn)
                                    {
                                        // Adding to list
                                        visionArea.Add(heroe);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        else if (game.turn == BattleSystem.HEROES)
        {
            // get all heroes in the visionarea
            foreach (GameObject heroe in heroesList)
            {

                foreach (Transform child in heroe.transform)
                {
                    foreach (GameObject nazgul in nazgulList)
                    {
                        int heroeRow = game.getRow(nazgul.transform);
                        int heroeColumn = game.getColumn(nazgul.transform);
                        PieceController pieceControl = nazgul.GetComponent<PieceController>();


                        if (child.tag == "Vision")
                        {

                            foreach (Transform tile in child)
                            {

                                if (move.validMovement(tile))
                                {
                                    int visionRow = game.getRow(tile.transform);
                                    int visionColumn = game.getColumn(tile.transform);

                                    // Checking if heroe is inside the area vision
                                    if (visionRow == heroeRow && visionColumn == heroeColumn)
                                    {
                                        // Adding to list
                                        visionArea.Add(nazgul);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (GameObject nazgul in nazgulList)
            {

                PieceController pieceControl = nazgul.GetComponent<PieceController>();

                // For each heroe in nazgul vision area, set visible
                if (visionArea.Contains(nazgul))
                {
                    pieceControl.visible();
                }
                // Set not visiible
                else
                    pieceControl.nOTvisible();

            }


        }

    }



    void updateLists()
    {
        nazgulList.Clear();
        heroesList.Clear();
        
        // Adding nazgul pieces to a list
        foreach (GameObject piece in game.positions)
        {

            if(piece != null && piece.transform.tag != "Eye")
            {
                if (piece.transform.GetChild(0).tag == "nazgul")
                    nazgulList.Add(piece);
                
                else if (piece.transform.GetChild(1).tag == "Heroes")
                    heroesList.Add(piece);
            }

        }

    }

}
