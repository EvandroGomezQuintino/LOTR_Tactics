using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBackground : MonoBehaviour
{

    // Identify if music is playing
    public bool music_ON;
    
    // 
    public AudioSource musicBackground;
    public Animator musicButtonClick;

    // Start is called before the first frame update
    public void Start()
    {
        // Set music ON
        music_ON = true;
        
        // Set music components
        musicBackground = GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>();
        musicButtonClick = GetComponentInChildren<Animator>();

    }
    public void playMusic()
    {
        if (music_ON == true)
        {
            // Pause music
            music_ON = false;
            musicBackground.Pause();
        }
        else
        {
            // Play music
            music_ON = true;
            musicBackground.Play();
           
        }
        
        // Play animation
        musicButtonClick.SetTrigger("Music_clicked");
    }
}
