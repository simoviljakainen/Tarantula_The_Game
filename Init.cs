using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Init : MonoBehaviour
{
    void Awake()
    {
        HiScoreStorage.GetInstance().LoadHiScores();
    }

    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
