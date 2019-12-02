using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform deadline;
    public MeshRenderer platformMeshRenderer;

    private float length, platformLenght, ratio, top, distance, offset;
    private Transform mapDeadline, mapPlatform, mapStatusLabel;
    private TMP_Text label;

    private Dictionary<string, string> states = new Dictionary<string, string>()
    { {"Safe","green"}, {"Warning","yellow"}, {"Danger","red"} };

    private string state, color;

    void Start()
    {
        offset = 0.45f;

        mapDeadline = transform.Find("MapDeadline");
        mapPlatform = transform.Find("MapPlatform");
        mapStatusLabel = transform.Find("StatusLabel");

        platformLenght = platformMeshRenderer.bounds.size.z;
        length = mapPlatform.GetComponent<MeshRenderer>().bounds.size.z;
        label = mapStatusLabel.GetComponent<TMP_Text>();
    }

    void Update()
    {
        top = length / 2 + mapPlatform.position.z - offset;
        distance = Vector3.Distance(deadline.position, player.position);

        ratio = distance / platformLenght;

        mapDeadline.transform.position = new Vector3(
            mapDeadline.transform.position.x, 
            mapDeadline.transform.position.y,
            top - length * ratio);

        if (distance <= platformLenght/2)
        {
            state = "Danger";
            color = states[state];
        }
        else if(distance <= platformLenght)
        {
            state = "Warning";
            color = states[state];
        }
        else
        {
            state = "Safe";
            color = states[state];
        }

        label.text = $"<color=\"{color}\">{state}</color>";

        /* Move the map as player moves */
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z);

    }
}
