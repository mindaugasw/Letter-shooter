using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MainScript : MonoBehaviour {

	// --- References to other objects ---
	public static MainScript instance;
	public Camera mainCamera;
	public Transform floor;
	public Collider2D gameArea;
	public GameObject ThrowablePrefab;
	public Transform spawnParent;

	

	// --- Configuration ---
	float spawnInterval = 0.1f;
	float arenaWidth = 1.2f; // Relative to screen width
	//const float forceMax = 30f;
	//const float forceMin = 15f;
	const float forceBuildTime = 8f; // Time needed of holding a single key to achieve max force
	public float forceCurrent;

	public AnimationCurve forceCurve; // Defines minimum, maximum force and it's change over time

	//public float middleForceMultiplier = 2f;
	//public float bottomForceMultiplier = 5f;

	// --- Instance variables ---
	KeyCode recentKeyPressed = KeyCode.None;

	float lastSpawn = 0;

	public GameObject[] LettersTop;
	public GameObject[] LettersMiddle;
	public GameObject[] LettersBottom;

	Key[][] keyboard;
	Vector2 viewport;
	
	void Start ()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(gameObject);

		keyboard = new Key[][]
		{
			new Key[] // Top keys row
			{
				new Key(KeyCode.Q, LettersTop[0]),
				new Key(KeyCode.W, LettersTop[1]),
				new Key(KeyCode.E, LettersTop[2]),
				new Key(KeyCode.R, LettersTop[3]),
				new Key(KeyCode.T, LettersTop[4]),
				new Key(KeyCode.Y, LettersTop[5]),
				new Key(KeyCode.U, LettersTop[6]),
				new Key(KeyCode.I, LettersTop[7]),
				new Key(KeyCode.O, LettersTop[8]),
				new Key(KeyCode.P, LettersTop[9])
			},
			new Key[] // Middle keys row
			{
				new Key(KeyCode.A, LettersMiddle[0]),
				new Key(KeyCode.S, LettersMiddle[1]),
				new Key(KeyCode.D, LettersMiddle[2]),
			},
			new Key[] // Bottom keys row
			{
				new Key(KeyCode.Z, LettersBottom[0]),
				new Key(KeyCode.X, LettersBottom[1]),
				new Key(KeyCode.C, LettersBottom[2]),
			}
		};

		spawnParent = this.transform;
		doScaling();
		StartCoroutine(forceControl());
	}

	void doScaling()
	{
		viewport = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) * 2;
		floor.localScale = new Vector3(viewport.x * arenaWidth, 1, 1);
		floor.position = new Vector3(0, -viewport.y / 2);
		gameArea.transform.localScale = new Vector3(viewport.x * arenaWidth, viewport.y * 4);
		gameArea.transform.Translate(0, viewport.y * 1.5f, 0);
	}

	void Update()
	{
		for (int row = 0; row < keyboard.Length; row++)
		{
			for (int key = 0; key < keyboard[row].Length; key++)
			{
				if (Input.GetKey((keyboard[row][key].KeyCode)))
				{
					recentKeyPressed = keyboard[row][key].KeyCode;
					spawnLetter(row, key);
				}
			}
		}



		/*if (Input.GetKey(KeyCode.G))
		{
			Debug.Log("G pressed.");
			GameObject newLetter = Instantiate(ThrowablePrefab, spawnParent);
			Vector2 newDirection = Random.insideUnitCircle;
			newDirection.y = Mathf.Abs(newDirection.y);
			newLetter.GetComponent<Rigidbody2D>().velocity = newDirection * force;
			newLetter.GetComponent<SpriteRenderer>().sprite = letter_c;
			//newLetter.AddComponent<PolygonCollider2D>();
		}*/
	}

	void spawnLetter(int row, int key)
	{
		if (Time.time - spawnInterval < lastSpawn)
			return;
		else
			lastSpawn = Time.time;

		Vector2 position = new Vector2(
			viewport.x / keyboard[row].Length * (key + 0.5f) - (viewport.x / 2),
			-viewport.y / 2 - 2);

		GameObject newLetter = Instantiate(keyboard[row][key].Prefab, position, Quaternion.identity, spawnParent);

		//float angle = Random.RandomRange(Mathf.PI / 4, Mathf.PI * 3 / 4);
		float angle = Random.Range(Mathf.PI / 2.25f, Mathf.PI / 1.8f); // Spawn at 90+-10 degrees (80-100)

		/*float f;
		if (row == 0)
			f = forceCurrent;
		else if (row == 1)
			f = forceCurrent * middleForceMultiplier;
		else
			f = forceCurrent * bottomForceMultiplier;*/

		newLetter.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * forceCurrent;

		/*Vector2 newDirection = Random.insideUnitCircle;
		newDirection.y = Mathf.Abs(newDirection.y);
		newLetter.GetComponent<Rigidbody2D>().velocity = newDirection * force;*/
	}

	public void clearAll()
	{
		int n = spawnParent.childCount;
		for (int i = 0; i < n; i++)
			Destroy(spawnParent.GetChild(i).gameObject);
	}

	IEnumerator forceControl()
	{
		KeyCode lastKey = KeyCode.None;
		float timeHeld = 0;
		while (true)
		{
			if (lastKey == KeyCode.None || !Input.GetKey(recentKeyPressed)) // If no key is pressed or a different one
			{
				lastKey = KeyCode.None;
				timeHeld = 0;
				//forceCurrent = forceCurve.Evaluate(timeHeld);
			}

			lastKey = recentKeyPressed;

			//forceCurrent = Mathf.Lerp(forceMin, forceMax, timeHeld / forceBuildTime);
			forceCurrent = forceCurve.Evaluate(timeHeld);
			timeHeld += Time.deltaTime;

			yield return null;
		}
	}
}
