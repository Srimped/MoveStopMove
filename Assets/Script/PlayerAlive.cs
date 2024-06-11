using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAlive : MonoBehaviour
{
    public static GameObject[] playerList;
    public static bool playerDead = false;
    public int totalPlayer = 100;
    public TextMeshProUGUI aliveNumber;
    public bool isWinning = false;

    void Update()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");

        if (playerDead == true)
        {
            playerDead = false;
            totalPlayer--;
        }

        aliveNumber.text = totalPlayer.ToString();

        if (totalPlayer == 1)
        {
            isWinning = true;
        }
    }
}
