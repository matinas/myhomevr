using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(LineRenderer))]
public class Remote : MonoBehaviour {

	public event Action OnRemotePressed = () => { };

	private Hand hand;

	private AudioSource audio;

	private SteamVR_Controller.Device device;

	private Collider TVCollider;
	LineRenderer lineRender;

	public bool locked;

	// Use this for initialization
	void Awake()
	{
		lineRender = GetComponent<LineRenderer>();
		locked = false;
		audio = GetComponent<AudioSource>();
	}
	
	void OnAttachedToHand(Hand h)
	{
		lineRender.enabled = true;
		hand = h;
	}

	void HandAttachedUpdate()
	{
		lineRender.SetPosition(0,transform.position);
		lineRender.SetPosition(1,transform.position+transform.right*10.0f);

		if (hand !=null && hand.controller != null)
		{
			device = SteamVR_Controller.Input((int) hand.controller.index);
			if (!locked && device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
			{
				audio.Play();
				OnRemotePressed();
				locked = true;
			}
			else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
			{
				locked = false;
			}
		}
	}

	void OnDetachedFromHand(Hand h)
	{
		hand = null;
		lineRender.enabled = false;
		locked = false;
	}
}
