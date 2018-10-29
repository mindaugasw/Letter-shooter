using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableScript : MonoBehaviour {

	void Start()
	{
		Invoke("DestroyThis", 20);
	}

	void DestroyThis()
	{
		Destroy(gameObject);
	}
}
