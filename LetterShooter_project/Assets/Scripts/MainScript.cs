using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MainScript : MonoBehaviour {

	public Camera mainCamera;
	public GameObject ThrowablePrefab;
	public Transform spawnParent;
	public float force = 10f;

	public GameObject[] LettersTop;
	public GameObject[] LettersMiddle;
	public GameObject[] LettersBottom;

	Key[][] keyboard;
	Vector2 viewport;

	public Sprite letter_c;

	void Start ()
	{
		keyboard = new Key[][]
		{
			new Key[]
			{
				new Key(KeyCode.Q, LettersTop[0]),
				new Key(KeyCode.W, LettersTop[1]),
				new Key(KeyCode.E, LettersTop[2]),
				new Key(KeyCode.R, LettersTop[3]),
				new Key(KeyCode.T, LettersTop[4]),
				new Key(KeyCode.Y, LettersTop[5])
			}

		};

		spawnParent = this.transform;
		viewport = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) * 2;
	}

	void Update()
	{
		for (int row = 0; row < keyboard.Length; row++)
		{
			for (int key = 0; key < keyboard[row].Length; key++)
			{
				if (Input.GetKey((keyboard[row][key].KeyCode)))
				{
					spawnLetter(row, key);
				}
			}
		}



		if (Input.GetKey(KeyCode.G))
		{
			Debug.Log("G pressed.");
			GameObject newLetter = Instantiate(ThrowablePrefab, spawnParent);
			Vector2 newDirection = Random.insideUnitCircle;
			newDirection.y = Mathf.Abs(newDirection.y);
			newLetter.GetComponent<Rigidbody2D>().velocity = newDirection * force;
			newLetter.GetComponent<SpriteRenderer>().sprite = letter_c;
			//newLetter.AddComponent<PolygonCollider2D>();
		}
	}

	void spawnLetter(int row, int key)
	{
		Vector2 position = new Vector2(
			viewport.x / keyboard[row].Length * (key + 0.5f) - (viewport.x / 2),
			-viewport.y / 2);

		GameObject newLetter = Instantiate(keyboard[row][key].Prefab, position, Quaternion.identity, spawnParent);

		Vector2 newDirection = Random.insideUnitCircle;
		newDirection.y = Mathf.Abs(newDirection.y);
		newLetter.GetComponent<Rigidbody2D>().velocity = newDirection * force;
	}
}
