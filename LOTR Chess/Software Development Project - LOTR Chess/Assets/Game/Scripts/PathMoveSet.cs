//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PathMoveSet : MonoBehaviour
//{

//    public GameController game;
//    public Movement mv;

//    public GameObject tileMovement;

//    public void Start()
//    {
//        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
//        mv = GameObject.FindWithTag("Movement").GetComponent<Movement>();
//    }
//    public void piecePath(GameObject obj)
//    {
//        GameObject objSelected = obj;
//        int xBoardPos;
//        int yBoardPos;

//        if ( objSelected.transform.GetChild(0).tag == "gandalf" || objSelected.transform.GetChild(0).tag == "witchking") 
//        {
//            xBoardPos = game.xPos(objSelected);
//            yBoardPos = game.yPos(objSelected);

//            //RIGHT-UP
//            if (mv.validMovement(xBoardPos-1, yBoardPos + 1) && mv.emptySpace(xBoardPos-1, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y+1), Quaternion.identity);

//            }
//            //2xRIGHT-UP
//            if (mv.validMovement(xBoardPos - 2, yBoardPos + 2) && mv.emptySpace(xBoardPos - 2, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);

//            }
//            //RIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 1) && mv.emptySpace(xBoardPos, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //2xRIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 2) && mv.emptySpace(xBoardPos, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //RIGHT-DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos + 1) && mv.emptySpace(xBoardPos + 1, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);

//            }
//            //2xRIGHT-DOWN
//            if (mv.validMovement(xBoardPos + 2, yBoardPos + 2) && mv.emptySpace(xBoardPos + 2, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);

//            }
//            //DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos) && mv.emptySpace(xBoardPos + 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }
//            //2xDOWN
//            if (mv.validMovement(xBoardPos + 2, yBoardPos) && mv.emptySpace(xBoardPos + 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);
//            }
//            //DOWN-LEFT
//            if (mv.validMovement(xBoardPos + 1, yBoardPos-1) && mv.emptySpace(xBoardPos + 1, yBoardPos-1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x-1, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }
//            //2xDOWN-LEFT
//            if (mv.validMovement(xBoardPos + 2 , yBoardPos - 2) && mv.emptySpace(xBoardPos + 2, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);
//            }

//            //LEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 1) && mv.emptySpace(xBoardPos, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //2xLEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 2) && mv.emptySpace(xBoardPos, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //UP-LEFT
//            if (mv.validMovement(xBoardPos-1, yBoardPos - 1) && mv.emptySpace(xBoardPos-1, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y+1), Quaternion.identity);
//            }
//            //2xUP-LEFT
//            if (mv.validMovement(xBoardPos - 2, yBoardPos - 2) && mv.emptySpace(xBoardPos - 2, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);
//            }
//            //UP
//            if (mv.validMovement(xBoardPos - 1, yBoardPos) && mv.emptySpace(xBoardPos - 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//            //2xUP
//            if (mv.validMovement(xBoardPos - 2, yBoardPos) && mv.emptySpace(xBoardPos - 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);
//            }

//        }


//        else if (objSelected.transform.GetChild(0).tag == "gimli"|| objSelected.transform.GetChild(0).tag == "boromir")
//        {
//            xBoardPos = game.xPos(objSelected);
//            yBoardPos = game.yPos(objSelected);

//            //RIGHT-UP
//            if (mv.validMovement(xBoardPos - 1, yBoardPos + 1) && mv.emptySpace(xBoardPos - 1, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);

//            }
//            //RIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 1) && mv.emptySpace(xBoardPos, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //RIGHT-DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos + 1) && mv.emptySpace(xBoardPos + 1, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);

//            }
//            //DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos) && mv.emptySpace(xBoardPos + 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }
//            //DOWN-LEFT
//            if (mv.validMovement(xBoardPos + 1, yBoardPos - 1) && mv.emptySpace(xBoardPos + 1, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }

//            //LEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 1) && mv.emptySpace(xBoardPos, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //UP-LEFT
//            if (mv.validMovement(xBoardPos - 1, yBoardPos - 1) && mv.emptySpace(xBoardPos - 1, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//            //UP
//            if (mv.validMovement(xBoardPos - 1, yBoardPos) && mv.emptySpace(xBoardPos - 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//        }


//        else if (objSelected.transform.GetChild(0).tag == "frodo" || objSelected.transform.GetChild(0).tag == "sam" || objSelected.transform.GetChild(0).tag == "merry" || objSelected.transform.GetChild(0).tag == "pippin")
//        {
//            xBoardPos = game.xPos(objSelected);
//            yBoardPos = game.yPos(objSelected);

//            //RIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 1) && mv.emptySpace(xBoardPos, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos) && mv.emptySpace(xBoardPos + 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }

//            //LEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 1) && mv.emptySpace(xBoardPos, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }

//            //UP
//            if (mv.validMovement(xBoardPos - 1, yBoardPos) && mv.emptySpace(xBoardPos - 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//        }


//        else if (objSelected.transform.GetChild(0).tag == "aragorn")
//        {
//            xBoardPos = game.xPos(objSelected);
//            yBoardPos = game.yPos(objSelected);

//            //RIGHT-UP
//            if (mv.validMovement(xBoardPos - 1, yBoardPos + 1) && mv.emptySpace(xBoardPos - 1, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);

//            }
//            //RIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 1) && mv.emptySpace(xBoardPos, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //2xRIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 2) && mv.emptySpace(xBoardPos, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //RIGHT-DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos + 1) && mv.emptySpace(xBoardPos + 1, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);

//            }
//            //DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos) && mv.emptySpace(xBoardPos + 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }
//            //2xDOWN
//            if (mv.validMovement(xBoardPos + 2, yBoardPos) && mv.emptySpace(xBoardPos + 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);
//            }
//            //DOWN-LEFT
//            if (mv.validMovement(xBoardPos + 1, yBoardPos - 1) && mv.emptySpace(xBoardPos + 1, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }

//            //LEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 1) && mv.emptySpace(xBoardPos, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //2xLEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 2) && mv.emptySpace(xBoardPos, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //UP-LEFT
//            if (mv.validMovement(xBoardPos - 1, yBoardPos - 1) && mv.emptySpace(xBoardPos - 1, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//            //UP
//            if (mv.validMovement(xBoardPos - 1, yBoardPos) && mv.emptySpace(xBoardPos - 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//            //2xUP
//            if (mv.validMovement(xBoardPos - 2, yBoardPos) && mv.emptySpace(xBoardPos - 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);
//            }
//        }


//        else if (objSelected.transform.GetChild(0).tag == "legolas")
//        {
//            xBoardPos = game.xPos(objSelected);
//            yBoardPos = game.yPos(objSelected);

//            //2xRIGHT-UP
//            if (mv.validMovement(xBoardPos - 2, yBoardPos + 2) && mv.emptySpace(xBoardPos - 2, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);

//            }
//            //1xRIGHT-UPx2
//            if (mv.validMovement(xBoardPos - 2, yBoardPos + 1) && mv.emptySpace(xBoardPos - 2, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);

//            }
//            //2xRIGHT-UPx1
//            if (mv.validMovement(xBoardPos - 1, yBoardPos + 2) && mv.emptySpace(xBoardPos - 1, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);

//            }
//            //2xRIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 2) && mv.emptySpace(xBoardPos, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //2xRIGHT-DOWNx1
//            if (mv.validMovement(xBoardPos + 1, yBoardPos + 2) && mv.emptySpace(xBoardPos + 1, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);

//            }
//            //2xRIGHT-DOWN
//            if (mv.validMovement(xBoardPos + 2, yBoardPos + 2) && mv.emptySpace(xBoardPos + 2, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);

//            }
//            //1xRIGHT-DOWNx2
//            if (mv.validMovement(xBoardPos + 2, yBoardPos + 1) && mv.emptySpace(xBoardPos + 2, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);

//            }
//            //2xDOWN
//            if (mv.validMovement(xBoardPos + 2, yBoardPos) && mv.emptySpace(xBoardPos + 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);
//            }
//            //2xDOWN-LEFTx1
//            if (mv.validMovement(xBoardPos + 2, yBoardPos - 1) && mv.emptySpace(xBoardPos + 2, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);
//            }
//            //2xDOWN-LEFT
//            if (mv.validMovement(xBoardPos + 2, yBoardPos - 2) && mv.emptySpace(xBoardPos + 2, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);
//            }
//            //2xDOWN-LEFT
//            if (mv.validMovement(xBoardPos + 1, yBoardPos - 2) && mv.emptySpace(xBoardPos + 1, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }
//            //2xLEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 2) && mv.emptySpace(xBoardPos, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //1xUP-LEFTx2
//            if (mv.validMovement(xBoardPos - 1, yBoardPos - 2) && mv.emptySpace(xBoardPos - 1, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//            //2xUP-LEFT
//            if (mv.validMovement(xBoardPos - 2, yBoardPos - 2) && mv.emptySpace(xBoardPos - 2, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);
//            }
//            //2xUP-LEFTx1
//            if (mv.validMovement(xBoardPos - 2, yBoardPos - 1) && mv.emptySpace(xBoardPos - 2, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);
//            }
//            //2xUP
//            if (mv.validMovement(xBoardPos - 2, yBoardPos) && mv.emptySpace(xBoardPos - 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);
//            }

//        }


//        else if (objSelected.transform.GetChild(0).tag == "nazgul")
//        {
//            xBoardPos = game.xPos(objSelected);
//            yBoardPos = game.yPos(objSelected);

//            //RIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 1) && mv.emptySpace(xBoardPos, yBoardPos + 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //2xRIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 2) && mv.emptySpace(xBoardPos, yBoardPos + 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }
//            //3xRIGHT
//            if (mv.validMovement(xBoardPos, yBoardPos + 3) && mv.emptySpace(xBoardPos, yBoardPos + 3))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x + 3, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);

//            }

//            //DOWN
//            if (mv.validMovement(xBoardPos + 1, yBoardPos) && mv.emptySpace(xBoardPos + 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 1), Quaternion.identity);
//            }
//            //2xDOWN
//            if (mv.validMovement(xBoardPos + 2, yBoardPos) && mv.emptySpace(xBoardPos + 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 2), Quaternion.identity);
//            }

//            //3xDOWN
//            if (mv.validMovement(xBoardPos + 3, yBoardPos) && mv.emptySpace(xBoardPos + 3, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y - 3), Quaternion.identity);
//            }

//            //LEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 1) && mv.emptySpace(xBoardPos, yBoardPos - 1))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 1, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //2xLEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 2) && mv.emptySpace(xBoardPos, yBoardPos - 2))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 2, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }
//            //3xLEFT
//            if (mv.validMovement(xBoardPos, yBoardPos - 3) && mv.emptySpace(xBoardPos, yBoardPos - 3))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x - 3, objSelected.GetComponent<Transform>().position.y), Quaternion.identity);
//            }

//            //UP
//            if (mv.validMovement(xBoardPos - 1, yBoardPos) && mv.emptySpace(xBoardPos - 1, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 1), Quaternion.identity);
//            }
//            //2xUP
//            if (mv.validMovement(xBoardPos - 2, yBoardPos) && mv.emptySpace(xBoardPos - 2, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 2), Quaternion.identity);
//            }
//            //3xUP
//            if (mv.validMovement(xBoardPos - 3, yBoardPos) && mv.emptySpace(xBoardPos - 3, yBoardPos))
//            {
//                Instantiate(tileMovement, new Vector2(objSelected.GetComponent<Transform>().position.x, objSelected.GetComponent<Transform>().position.y + 3), Quaternion.identity);
//            }
//        }

//    }







//}
