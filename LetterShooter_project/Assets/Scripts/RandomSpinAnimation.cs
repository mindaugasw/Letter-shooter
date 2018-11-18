using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpinAnimation : MonoBehaviour
{
	Transform t;

	// Scale
	float orginialScale = 2f;
	float minScale = 0.8f;
	float maxScale = 1.2f;
	float scalePeriod = 15f; // Time it takes to go from minScale to maxScale

	float scaleTime = 0f;
	bool scaleIncreasing = true;
	float newScale;

	// Spin
	public float spinSpeed = -1f;
	
    void Start()
    {
		t = transform;
    }

    
    void Update()
    {
		// Scale
		if (scaleIncreasing)
		{
			scaleTime += Time.deltaTime;

			if (scaleTime > scalePeriod)
				scaleIncreasing = false;
		}
		else
		{
			scaleTime -= Time.deltaTime;

			if (scaleTime < 0)
				scaleIncreasing = true;
		}

		newScale = Mathf.Lerp(orginialScale * minScale, orginialScale * maxScale, scaleTime / scalePeriod);
		t.localScale = new Vector3(newScale, newScale, 0);

		// Spin
		t.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
	}
}
