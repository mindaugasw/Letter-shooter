using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour {

	new Collider2D collider;
	float timer = 1f;

	void Start()
	{
		collider = GetComponent<Collider2D>();
		//Invoke("DestroyThis", timer);
	}

	void DestroyThis()
	{
		if (collider.IsTouching(MainScript.Instance.gameArea))
		{
			Invoke("DestroyThis", timer);
		}
		else
		{
			Destroy(gameObject);
		}

	}

	void OnTriggerExit2D(Collider2D collision)
	{
		Invoke("DestroyThis", 5);
		//DestroyThis();
	}
}
