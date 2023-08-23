using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    private int coinsToSpawn;

    [SerializeField] private TMP_Text coinText;

    public int currency;
   
    void Start()
    {
        coinsToSpawn = Random.Range(2, 8);
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = currency.ToString();
        while(coinsToSpawn >= 0) 
        {
            coinsToSpawn--;
            Instantiate(coin, new Vector2(Random.Range(-8, 9),Random.Range(-3, 4)), coin.transform.rotation);
        }
    }
}
