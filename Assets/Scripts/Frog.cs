using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour {

	public Rigidbody2D rb;

	GameManager gameManager;

	public bool canMove;

    [SerializeField] SpriteRenderer frogSprite;

    private void Start()
    {
		canMove = true;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update () {

        frogSprite.sprite = gameManager.frogSprites[gameManager.currentFrogSprite];
        //For PC to move all over
        if (canMove)
		{
            if (Input.GetKeyDown(KeyCode.D))
            {
				rb.MovePosition(rb.position + Vector2.right);
				this.gameObject.transform.eulerAngles = new Vector3(0, 0, -90);
			}
             
            else if (Input.GetKeyDown(KeyCode.A))
            {
                rb.MovePosition(rb.position + Vector2.left);
				this.gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
			}
            else if (Input.GetKeyDown(KeyCode.W))
            {
                rb.MovePosition(rb.position + Vector2.up);
				this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                rb.MovePosition(rb.position + Vector2.down);
				this.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
			}
        }
		

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Car") 
		{
			Debug.Log("WE LOST!");
			gameManager.state = GameManager.GameState.GAMEOVER;
			gameManager.doOnce = true;
		}

		if (col.tag == "Coin")
		{
			GameObject.Find("Level").GetComponent<Level>().currency++;
			Destroy(col.gameObject);
			gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
		}

		if (col.tag == "Goal")
		{
			GameObject.Find("Frog").transform.position = (gameManager.spawnPoint);
			GameObject.Find("Level").GetComponent<Level>().ResetCoins();
		}
	}
}
