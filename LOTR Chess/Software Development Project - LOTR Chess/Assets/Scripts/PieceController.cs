using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{

    public bool pieceVisible;




    public void Start()
    {
        pieceVisible = true;
    }



    public void Update()
    {
        if(pieceVisible == false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
            GetComponent<SpriteRenderer>().enabled = true;
    }



    // Set piece visible
    public void visible()
    {
        pieceVisible = true;
    }

    // Set piece invisible
    public void nOTvisible()
    {
        pieceVisible = false;
    }



}
