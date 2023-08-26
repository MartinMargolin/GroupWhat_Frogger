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
    [SerializeField] public int itemIndex;
    [SerializeField] public bool itemType; // true = car false = frog

   
    public GameManager gameManager;


    private void Awake()
    {
        isBought = false;
        isEquipped = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buyButton = gameObject.transform.Find("BUY").gameObject;
        equipButton = gameObject.transform.Find("EQUIP").gameObject;
        buyButton.GetComponent<Button>().onClick.AddListener(delegate { onBuy(); });
        equipButton.GetComponent<Button>().onClick.AddListener(delegate { onEquip(); });
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
            foreach (var item in gameManager.items)
            {
                if (item.GetComponent<ItemUI>().isBought && item.GetComponent<ItemUI>().itemType == itemType) item.GetComponent<ItemUI>().isEquipped = false;
            }

            isEquipped = true;

        }
    }

}
