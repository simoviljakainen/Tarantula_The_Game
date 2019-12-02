using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HiScoreUtilities : MonoBehaviour
{
    static HiScoreStorage storage;
    const int TOPCOUNT = 10;

    private void Start()
    {
        storage = HiScoreStorage.GetInstance();
    }

    public static bool IsUsernameAvailable(string username)
    {
        return !storage.IsKeyInStorage(username);
    }

    public static bool IsHiScore(int value)
    {
        Dictionary<string, int> scores = storage.GetHiScores();
        int count = 0;
        bool isHiScore = false;

        if (scores.Count < TOPCOUNT || scores == null)
        {
            isHiScore = true;
        }
        else
        {
            foreach (KeyValuePair<string, int> score in scores.OrderByDescending(key => key.Value))
            {
                if (score.Value < value)
                {
                    isHiScore = true;
                }
                count++;
            }
        }

        return isHiScore;
    }

    public static void AddIntoHiScores(string name, int value)
    {
        Dictionary<string, int> scores = storage.GetHiScores();
        int count = 1;
        bool isAdded = false;
        string currentKey = "";

        if (scores.Count() < TOPCOUNT)
        {
            storage.AddHiScore(name, value);
        }
        else
        {
            count = 0;

            foreach (KeyValuePair<string, int> score in scores.OrderByDescending(key => key.Value))
            {
                if (score.Value < value)
                {
                    isAdded = true;
                }

                currentKey = score.Key;
                count++;
            }

            /* Pop the last record */
            if (isAdded)
            {
                storage.RemoveHiScore(currentKey);
                storage.AddHiScore(name, value);
            }
        }

    }
}
