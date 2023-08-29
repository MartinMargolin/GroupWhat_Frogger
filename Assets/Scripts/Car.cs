using System.Threading;
using UnityEngine;

public class Car : MonoBehaviour {

	public Rigidbody2D rb;

	public float speed = 1f;

	[SerializeField] SpriteRenderer carSprite;
	public GameManager gameManager;

	public float minSpeed = 8f;
	public float maxSpeed = 12f;

	private float timer = 2f;

	void Start()
	{
		speed = 10f;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		carSprite.sprite = gameManager.carSprites[gameManager.currentCarSprite];
		
	}
		
	void FixedUpdate()
	{
		timer -= Time.deltaTime;
		if (timer < 0) Destroy(gameObject);
		Vector2 forward = new Vector2 (transform.right.x, transform.right.y);
		rb.MovePosition (rb.position + forward * Time.fixedDeltaTime * speed);
	}

}
