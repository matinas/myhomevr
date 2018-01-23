using UnityEngine;

public class SpinController : MonoBehaviour {

	public int speed;

	// Update is called once per frame
	void Update () {
		transform.Rotate(0,speed,0);
	}
}
