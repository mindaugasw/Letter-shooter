using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

	public GameObject ThrowablePrefab;
	public Transform spawnParent;
	public float force = 10f;
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
		}
	}
}
