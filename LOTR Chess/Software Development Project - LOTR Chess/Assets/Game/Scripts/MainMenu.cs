using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using TMPro;

using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{



    public AudioSource selectedMenu;
    public TMP_Text textMenu;

    private void Awake()
    {
        //selectedMenu = gameObject.AddComponent<AudioSource>();
        //selectedMenu.clip = (AudioClip)Resources.Load("Sound/menu_clickSound");
        //Debug.LogError(selectedMenu);
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

    public void PlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LocalGame()
    {
        GameObject.Find("BackGround").GetComponent<DontDestroy>().localGameMode = true;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void MultiplayerGame()
    {
        GameObject.Find("BackGround").GetComponent<DontDestroy>().multiplayerGameMode = true;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void playSound()
    {
        selectedMenu.Play();
    }

}
