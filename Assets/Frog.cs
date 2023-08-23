using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour {

	public Rigidbody2D rb;

	GameManager gameManager;



    private void Start()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update () {

		//Clamp the area in which frog can move

		//For PC to move all over

		if (Input.GetKeyDown (KeyCode.D))
			rb.MovePosition (rb.position + Vector2.right);
		else if (Input.GetKeyDown (KeyCode.A))
			rb.MovePosition (rb.position + Vector2.left);
		else if (Input.GetKeyDown (KeyCode.W))
			rb.MovePosition (rb.position + Vector2.up);
		else if (Input.GetKeyDown (KeyCode.S))
			rb.MovePosition (rb.position + Vector2.down);

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Car") 
		{
			Debug.Log("WE LOST!");
			
			// GameManager lose;
		}

		if (col.tag == "Coin")
		{
			GameObject.Find("Level").GetComponent<Level>().currency++;
			Destroy(col.gameObject);
			gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
		}
	}
}
