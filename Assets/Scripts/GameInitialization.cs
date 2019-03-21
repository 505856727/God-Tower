using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialization()
    {
        GameManager.GetInstance();
        InputManager.GetInstance();
        TimeManager.GetInstance();
    }
}
