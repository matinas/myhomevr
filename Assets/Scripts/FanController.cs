using UnityEngine;

public class FanController : MonoBehaviour {

	public int speed;

	// Update is called once per frame
	void Update () {
		transform.Rotate(0,speed,0);
	}
}
