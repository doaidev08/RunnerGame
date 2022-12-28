using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + 1);
            PlayerManager.score += 5;
            FindObjectOfType<AudioManager>().PlaySound("CoinCollect");
            Destroy(this.gameObject);
        }
    }
}
