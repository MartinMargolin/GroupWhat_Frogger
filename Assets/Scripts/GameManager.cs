using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public int coins;

    public enum GameState
    {
        START,
        PLAY,
        GAMEOVER,
        RESET,
    }

    public GameState state;

    [SerializeField] public List<Sprite> carSprites;
    [SerializeField] public List<Sprite> frogSprites;


    [SerializeField] public int currentCarSprite;
    [SerializeField] public int currentFrogSprite;

    [SerializeField] public GameObject shopHolder;



    [Serializable] public class Item
    {
        public Item(string  name, bool isBought, bool isEquipped, bool itemType)
        {
            this.name = name;
            this.isBought = isBought;
            this.isEquipped = isEquipped;
            this.itemType = itemType;
        }


        public string name;
        public bool isBought;
        public bool isEquipped;
        public bool itemType;
    }


    [Serializable]
    public class SerializedItems
    {
        public int coins;
        public List<Item> items;
    }


    public SerializedItems serializedItems;


    public Vector3 spawnPoint = new Vector3(0,-4,0);

    public List<GameObject> items;

    public bool doOnce;

    private void Awake()
    {
        shopHolder = GameObject.Find("UI").transform.Find("ShopMenu").transform.Find("Items").gameObject;
        GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(true);
        foreach (Transform item in shopHolder.transform)
        {
           
            items.Add(item.gameObject);
        }

        foreach (var i in items)
        {
            ItemUI temp = i.GetComponent<ItemUI>();
            serializedItems.items.Add(new Item(temp.name, temp.isBought, temp.isEquipped, temp.itemType));
        }
        serializedItems.coins = coins;
        //Debug.Log(serializedItems.items.Count);
        string json = JsonUtility.ToJson(serializedItems);
        //Debug.Log(json);   
        //Debug.Log(Application.persistentDataPath);
        if (!File.Exists(Application.persistentDataPath + "/ItemData.json")) File.WriteAllText(Application.persistentDataPath + "/ItemData.json", json);
        else 
        {
           json = File.ReadAllText(Application.persistentDataPath + "/ItemData.json");

           serializedItems = JsonUtility.FromJson<SerializedItems>(json);

           foreach (var i in serializedItems.items)
            {
                Debug.Log(items.First(x => x.name == i.name).name);
                items.First(x => x.name == i.name).GetComponent<ItemUI>().isBought = i.isBought;
                items.First(x => x.name == i.name).GetComponent<ItemUI>().isEquipped = i.isEquipped;
                items.First(x => x.name == i.name).GetComponent<ItemUI>().itemType = i.itemType;
            }
            
            coins = serializedItems.coins;
        }

        serializedItems.items.Clear();
        DontDestroyOnLoad(GameObject.Find("UI"));
        DontDestroyOnLoad(this);
         GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(false);
    }

    private void Start()
    {
        // load json data and apply it
        GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(false);
        doOnce = true;


    }

    private void Update()
    {

        if (items.Where(i => i.GetComponent<ItemUI>().itemType == true).Any(i => i.GetComponent<ItemUI>().isEquipped)) currentCarSprite = items.Where(i => i.GetComponent<ItemUI>().itemType == true).First(x => x.GetComponent<ItemUI>().isEquipped).GetComponent<ItemUI>().itemIndex;
        if (items.Where(i => i.GetComponent<ItemUI>().itemType == false).Any(i => i.GetComponent<ItemUI>().isEquipped)) currentFrogSprite = items.Where(i => i.GetComponent<ItemUI>().itemType == false).First(x => x.GetComponent<ItemUI>().isEquipped).GetComponent<ItemUI>().itemIndex;



        switch (state)
        {
            case GameState.START:
                if (doOnce)
                {
                    GameObject.Find("Frog").GetComponent<Frog>().canMove = false;
                    doOnce = false;
                }
                break;
            case GameState.PLAY:
                if (doOnce)
                {
                    GameObject.Find("UI").transform.Find("GameUI").gameObject.SetActive(true);
                    GameObject.Find("Frog").GetComponent<Frog>().canMove = true;
                    doOnce = false;
                }
                break;
            case GameState.GAMEOVER:
                if (doOnce)
                    {
                        GameObject.Find("UI").transform.Find("GameOverMenu").gameObject.SetActive(true);
                        GameObject.Find("Frog").transform.position = spawnPoint;
                        GameObject.Find("Frog").GetComponent<Frog>().canMove = false;
                        coins += GameObject.Find("Level").GetComponent<Level>().currency;
                        GameObject.Find("Level").GetComponent<Level>().currency = 0;
                    doOnce = false;
                }
                break;
            case GameState.RESET:
                if (doOnce)
                {   
                    GameObject.Find("UI").transform.Find("GameOverMenu").gameObject.SetActive(false);
                    GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(false);
                    GameObject.Find("UI").transform.Find("StartMenu").gameObject.SetActive(true);
                    GameObject.Find("UI").transform.Find("GameUI").gameObject.SetActive(false);
                    state = GameState.START;

                    foreach (var i in items)
                    {
                        ItemUI temp = i.GetComponent<ItemUI>();
                       
                        serializedItems.items.Add(new Item(temp.name, temp.isBought, temp.isEquipped, temp.itemType));
                    }

                    serializedItems.coins = coins;
                    //Debug.Log(serializedItems.items.Count);
                    string json = JsonUtility.ToJson(serializedItems);
                    //Debug.Log(json);   
                    //Debug.Log(Application.persistentDataPath);
                    File.WriteAllText(Application.persistentDataPath + "/ItemData.json", json);
                    serializedItems.items.Clear();



                    doOnce = false;
                }

                doOnce = true;
                break;
        }
    }


    public void onPlayClick()
    {
        state = GameState.PLAY;
        doOnce = true;
        GameObject.Find("Level").GetComponent<Level>().ResetCoins();
        GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(false);
        GameObject.Find("UI").transform.Find("StartMenu").gameObject.SetActive(false);
    }

    public void onShopClick()
    {
        GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(true);
        GameObject.Find("UI").transform.Find("StartMenu").gameObject.SetActive(false);
    }

    public void onMenuClick()
    {
        GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(false);
        GameObject.Find("UI").transform.Find("StartMenu").gameObject.SetActive(true);


        foreach (var i in items)
        {
            ItemUI temp = i.GetComponent<ItemUI>();
            serializedItems.items.Add(new Item(temp.name, temp.isBought, temp.isEquipped, temp.itemType));
        }

        serializedItems.coins = coins;
        //Debug.Log(serializedItems.items.Count);
        string json = JsonUtility.ToJson(serializedItems);
        //Debug.Log(json);   
        //Debug.Log(Application.persistentDataPath);
         File.WriteAllText(Application.persistentDataPath + "/ItemData.json", json);

        serializedItems.items.Clear();

    }

    public void onResetClick()
    {
        state = GameState.RESET;
        doOnce = true;
        GameObject.Find("UI").transform.Find("ShopMenu").gameObject.SetActive(false);
        GameObject.Find("UI").transform.Find("StartMenu").gameObject.SetActive(true);
    }

}
