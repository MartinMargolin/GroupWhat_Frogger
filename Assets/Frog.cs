using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour {

	public Rigidbody2D rb;

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

		//For Android to move in only one direction
		/*
		if (Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown (0)) 
		{
			rb.MovePosition (rb.position + Vector2.up);
		}
		*/
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Car") 
		{
			Debug.Log("WE LOST!");
			Score.CurrentScore = 0;
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
