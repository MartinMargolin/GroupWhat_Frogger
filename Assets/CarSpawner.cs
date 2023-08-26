using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

	public float spawnDelay = .3f;

	public GameObject car;

	public Transform[] spawnPoints;

	public GameManager gameManager;

	float nextTimeToSpawn = 0f;

		

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
	{	
		
		if (nextTimeToSpawn <= Time.time && gameManager.state == GameManager.GameState.PLAY) 
		{
			SpawnCar ();
			nextTimeToSpawn = Time.time + spawnDelay;
		}
	}

	void SpawnCar()
	{
		int randomIndex = Random.Range (0, spawnPoints.Length);
		Transform spawnPoint = spawnPoints [randomIndex];

		Instantiate(car, spawnPoint.position, spawnPoint.rotation);
	}


}
