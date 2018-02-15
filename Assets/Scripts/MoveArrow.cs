using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour {

	private Vector3 initialPos;
	private bool movingUp;

	[SerializeField] private float speed;
	[SerializeField] private float maxHeight;

	void Start()
	{
		initialPos = gameObject.transform.position;
		movingUp = true;
	}

	// Update is called once per frame
	void Update () {
		
		if (movingUp)
		{
			if (transform.position.y < maxHeight)
				transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
			else
				movingUp = false;
		}
		else
		{
			if (transform.position.y > initialPos.y)
				transform.position = new Vector3(transform.position.x, transform.position.y - (speed * Time.deltaTime), transform.position.z);
			else
				movingUp = true;
		}
	}
}
