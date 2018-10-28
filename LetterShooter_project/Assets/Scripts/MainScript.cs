using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

	public GameObject ThrowablePrefab;
	public Transform spawnParent;
	public float force = 10f;
	public Sprite letter_c;

	void Start () {
		spawnParent = this.transform;
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.G))
		{
			Debug.Log("G pressed.");
			GameObject newLetter = Instantiate(ThrowablePrefab, spawnParent);
			Vector2 newDirection = Random.insideUnitCircle;
			newDirection.y = Mathf.Abs(newDirection.y);
			newLetter.GetComponent<Rigidbody2D>().velocity = newDirection * force;
			newLetter.GetComponent<SpriteRenderer>().sprite = letter_c;
			//newLetter.GetComponent<PolygonCollider2D>().
		}
	}
}
