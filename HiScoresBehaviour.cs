using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
public class HiScoresBehaviour : MonoBehaviour
{
    public GameObject hiscoreEntry;
    static HiScoreStorage storage;
    const int TOPCOUNT = 10;
    
    void Start()
    {
        storage = HiScoreStorage.GetInstance();
        LoadHiScoreListing();
    }

    public void LoadHiScoreListing()
    {
        /* Load top 10 -270 y 50 chars*/

        Dictionary<string, int> scores = storage.GetHiScores();
        int count = 0, tempScore = 0, offset = 45;
        string tempName = "name";
            

        foreach (KeyValuePair<string, int> score in scores.OrderByDescending(key => key.Value))
        {
            AddHiScoreEntry(GetStringEntry(score.Key, score.Value), count * offset);
            count++;
        }

        for(;count < TOPCOUNT; count++)
        {
            AddHiScoreEntry(GetStringEntry(tempName, tempScore), count * offset);
        }
    }
    
    private string GetStringEntry(string user, int score)
    {
        string filler = GetFiller($"{user}{score}".Count());
        return $"{user}{filler}{score}";
    }

    private string GetFiller(int size)
    {
        string filler = "";
        for(int i = 0; i < (50-size); i++)
        {
            filler += ".";
        }
        return filler;
    }

    private void AddHiScoreEntry(string hiscore, int offset)
    { 
        GameObject entry = Instantiate(hiscoreEntry, transform);
        entry.GetComponent<TMP_Text>().text = hiscore;
        entry.transform.localPosition = new Vector3(20f, 270f - offset, 0f);
    }
}
