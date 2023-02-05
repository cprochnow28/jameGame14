using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public bool hasWon = false;
    public GameObject victoryText;
    public GameObject victoryFlag;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            hasWon = true;
            victoryText.SetActive(true);
            victoryFlag.SetActive(true);
        }
    }
}
