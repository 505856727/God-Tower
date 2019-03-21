using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DDOLSingleton<GameManager>
{
    public enum GameState { origin,battle,backpack}
    public GameState state;


    public void Initialization()
    {
        gameObject.AddComponent<AudioSource>();
    }
    //播放音乐一系列重载
    #region
    public void PlayMusic(int number)
    {
        
    }

    public void PlayMusic(int number,int volume)
    {

    }

    public void PlayMusic(int number,int volume,int pitch)
    {

    }

    public void PlayMusic(int number, int volume, int pitch, bool loop)
    {

    }    

    public void PlayMusic(int number,bool loop)
    {

    }
    #endregion



}
