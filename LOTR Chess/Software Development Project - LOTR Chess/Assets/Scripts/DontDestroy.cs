using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    // Game Mode
    public bool localGameMode = false;
    public bool multiplayerGameMode = false;



    void Awake()
    {



        DontDestroyOnLoad(this.gameObject);
    }



}
