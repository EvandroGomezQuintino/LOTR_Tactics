using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using TMPro;

using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{

    public TMP_Text textMenu;


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





}
