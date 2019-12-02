using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class HiScoreStorage
{
    public static HiScoreStorage storage = null;
    private Dictionary<string, int> scores = new Dictionary<string, int>();

    public static HiScoreStorage GetInstance()
    {
        if(storage == null)
        {
            storage = new HiScoreStorage();
        }
        return storage;
    }

    public void AddHiScore(string name, int score)
    {
        scores[name] = score;
    }

    public void RemoveHiScore(string name)
    {
        scores.Remove(name);
    }

    public Dictionary<string, int> GetHiScores()
    {
        return scores;
    }

    public int GetScore(string str)
    {
        return GetHiScores()[str];
    }

    public bool IsKeyInStorage(string username)
    {
        return scores.ContainsKey(username);
    }

    public void SaveHiScores()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.dataPath + "/Resources/Scores.tarantula", FileMode.Create, FileAccess.Write);

        formatter.Serialize(stream, scores);
        stream.Close();
        Debug.Log("Saved");
    }

    public void LoadHiScores()
    {
        IFormatter formatter = new BinaryFormatter();
        try
        {
            Stream stream = new FileStream(Application.dataPath+"/Resources/Scores.tarantula", FileMode.Open, FileAccess.Read);

            scores = (Dictionary<string, int>)formatter.Deserialize(stream);
            stream.Close();
        }
        catch
        {
            Debug.Log("Failed to open the file");
        }
        
    }
}
