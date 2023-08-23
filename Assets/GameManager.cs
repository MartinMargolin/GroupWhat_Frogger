using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public int coins;

    public enum GameState
    {
        START,
        PLAY,
        RESET,
    }

    public GameState state;

    [SerializeField] public List<Sprite> carSprites;
    [SerializeField] public List<Sprite> frogSprites;


    [SerializeField] public int currentCarSprite;
    [SerializeField] public int currentFrogSprite;


    

    public bool doOnce;

    private void Awake()
    {
        // load json data and apply it
    }

    private void Start()
    {
        coins = 0;
        doOnce = true;
      

    }

    private void Update()
    {
        switch(state)
        {
            case GameState.START:
                if (doOnce)
                {
                    doOnce = false;
                }
                break;
            case GameState.PLAY:
                if (doOnce)
                {
                    doOnce = false;
                }
                break;
            case GameState.RESET:
                if (doOnce)
                {
                    doOnce = false;
                }
                break;
        }
    }


}
