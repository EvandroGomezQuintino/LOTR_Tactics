using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using TMPro;

using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{


    // AudioSource used for button click sound
    public AudioSource selectedMenu;
    // Button Text
    public TMP_Text textMenu;

    private void Awake()
    {
        // Creating AudioSource and Loading Click Sound
        selectedMenu = gameObject.AddComponent<AudioSource>();
        selectedMenu.clip = (AudioClip)Resources.Load("Sound/menu_clickSound");
        
    }
    


    
    void OnMouseOver()
    {
        textMenu.fontStyle = FontStyles.Underline;
    }

    void OnMouseExit()
    {
        textMenu.fontStyle = FontStyles.Normal;
    }

    public void OnMouseDown()
    {
        textMenu.fontStyle = FontStyles.Normal;
    }

    public void playGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void localGame()
    {
        GameObject.Find("BackGround").GetComponent<DontDestroy>().localGameMode = true;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void multiplayerGame()
    {
        GameObject.Find("BackGround").GetComponent<DontDestroy>().multiplayerGameMode = true;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void playSound()
    {
        selectedMenu.Play();
    }



}
