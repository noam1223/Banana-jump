using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGameScript : MonoBehaviour
{

    public void RestartBtnPressed()
    {
        GameManager.instance.LoadGame();
    }

    public void QuitGamePressed()
    {
        Application.Quit();
    }


    public void MuteSound()
    {

        AudioSource[] audios = GameObject.Find("Sound Manager").GetComponents<AudioSource>();
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].mute = !audios[i].mute;
        }
  
    }
}
