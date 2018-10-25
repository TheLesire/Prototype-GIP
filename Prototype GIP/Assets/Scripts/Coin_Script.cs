using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Script : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D col)
    {
        ScoreScript.coinAmount += 1;
        Destroy (gameObject);
    }
}
