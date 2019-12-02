using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadPlatformObjects : MonoBehaviour
{
    public GameObject healthpackPrefab;
    public float spawnOffsetX, spawnOffsetZ;
    public int maxTextLines;
    public List<GameObject> enemyList;

    private Vector3 currentPos, platformSize;
    private List<GameObject> gameObjectList = new List<GameObject>();
    private float healthSpawnChance;
    private TMP_Text platformText;
    private PlatformTextReader textReader;

    void Start()
    {
        platformSize = GetComponent<Renderer>().bounds.size;
        currentPos = transform.position;
        healthSpawnChance = 0.05f;

        platformText = GetComponentInChildren<TMP_Text>();
        textReader = PlatformTextReader.GetPlatformTextReader();
    }

    void Update()
    {
        if (currentPos != transform.position)
        {
            UpdatePlatformText();
            RemoveOldObjects();
            
            float difficulty = GameService.gameDifficulty;

            /* Spawn enemies */
            for (int i = 0; i < difficulty+2; i++)
            {
                if(difficulty > enemyList.Count)
                {
                    difficulty = enemyList.Count;
                }
                SpawnObject(enemyList[Random.Range(0, (int)difficulty)]);
            }

            /* Random chance for a health pack to spawn */
            if(Random.value < healthSpawnChance)
            {
                SpawnObject(healthpackPrefab);
            }

            currentPos = transform.position;
        }
    }

    Vector3 GetRandomPosition(Vector3 platformLocation)
    {
         return (
            new Vector3(
                Random.Range(spawnOffsetX + platformLocation.x  - platformSize.x / 2,
                -spawnOffsetX + platformLocation.x + platformSize.x / 2),
                1,
                Random.Range(platformLocation.z + spawnOffsetZ - platformSize.z / 2,
                platformLocation.z +platformSize.z / 2 - spawnOffsetZ)
            ));
    }

    GameObject SpawnObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.position = GetRandomPosition(transform.position);
        float col = obj.GetComponent<Collider>().bounds.size.y/2;
        obj.transform.position = new Vector3(obj.transform.position.x, col, obj.transform.position.z);

        gameObjectList.Add(obj);

        return obj;
    }

    void UpdatePlatformText()
    {
        platformText.text = "";

        foreach (string line in textReader.GetLines(maxTextLines))
        {
            platformText.text += line + "\n";
        }
    }

    void RemoveOldObjects()
    {
        foreach (GameObject obj in gameObjectList)
        {
            RemoveObject(obj);
        }
    }

    void RemoveObject(GameObject obj)
    {
        Destroy(obj, 0.5f);
    }

}
