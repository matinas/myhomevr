using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speedX = 2.0f;
    [SerializeField] private float speedY = 2.0f;

    [SerializeField] private float yaw = 0.0f;
    [SerializeField] private float pitch = 0.0f;

    void Awake()
    {
        #if UNITY_EDITOR
            gameObject.active = true;
        #else
            gameObject.active = false;
        #endif
    }

	// Update is called once per frame
	void Update ()
    {
        yaw += speedX * Input.GetAxis("Mouse X");
        pitch -= speedY * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
