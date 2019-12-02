using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public static float gameDifficulty;
    public static int kills;
    public Transform target;

    public static int score;
    public static bool playing;

    private void Start()
    {
        kills = 0;
        playing = true;
    }

    void Update()
    {
        score = (int)target.position.z;
        gameDifficulty = target.position.z / 1000;
        UIController.uiInstance.SetScore(score);

        if(gameDifficulty <= 0)
        {
            gameDifficulty = 0.0001f;
        }

    }

    private void OnApplicationQuit()
    {
        PlatformTextReader.GetPlatformTextReader().CloseFileStream();
    }
}
