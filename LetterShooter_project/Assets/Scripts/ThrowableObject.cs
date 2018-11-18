using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour {

	Collider2D collider;
	float timer = 1f;

	void Start()
	{
		collider = GetComponent<Collider2D>();
		//Invoke("DestroyThis", timer);
	}

	void DestroyThis()
	{
		if (collider.IsTouching(MainScript.instance.gameArea))
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
		Invoke("DestroyThis", 2);
		//DestroyThis();
	}
}
