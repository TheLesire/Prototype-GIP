using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    PlayerController player;
    public Text pointstext;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        pointstext.text = (" " + player.points);
    }
}