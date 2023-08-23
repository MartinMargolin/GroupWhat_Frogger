using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject equipButton;

    [SerializeField] int price;

    public bool isBought;
    public bool isEquipped;
    
    public GameManager gameManager;


    private void Start()
    {
        isBought = false;
        isEquipped = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void Update()
    {
        if (isBought)
        {
            buyButton.GetComponent<Button>().interactable = false;
            equipButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = true;
            equipButton.GetComponent<Button>().interactable = false;
        }

        if (isEquipped)
        {
            equipButton.GetComponent<Button>().interactable = false;
        }
    }

    public void onBuy()
    {
        if (gameManager.coins >= price)
        {
            isBought = true;
            gameManager.coins -= price;
        }
    }

    public void onEquip()
    {
        if (isBought && !isEquipped)
        {
            
        }
    }

}
