using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Key
{
	public KeyCode KeyCode;
	public GameObject Prefab;

	public Key(KeyCode keyCode, GameObject prefab)
	{
		KeyCode = keyCode;
		Prefab = prefab;
	}
}
