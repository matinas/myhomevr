using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrabHandler : MonoBehaviour {

	private bool isGrabbing;

	private Animator anim;

	private Vector3 grabPos;

	private Hand attachedHand;

	public float animTime = 0.8f;

	private float nPos, last_t_anim = 0.0f;

	// Use this for initialization
	void Awake ()
	{
		anim = gameObject.GetComponentInParent<Animator>();
	}
	
	void Update()
	{
		if (isGrabbing)
		{
			// Vector3 diff = grabPos - attachedHand.transform.localPosition;
			// nPos =  diff.magnitude * Mathf.Sign(Vector3.Dot(diff,attachedHand.transform.right));

			Debug.Log("New Hand Position: " + attachedHand.transform.localPosition.x);
			float diff = attachedHand.transform.localPosition.x - grabPos.x;
			Debug.Log("Diff: " + diff);
			float t_anim = Mathf.Clamp01((last_t_anim+diff)*animTime);

			Debug.Log("Se va a setear la animacion en " + t_anim + " , npos: " + nPos + "last_t_anim: " + last_t_anim);
			anim.SetFloat("Door",t_anim);

			if (attachedHand.GetStandardInteractionButtonUp())
			{
				Debug.Log("Door handle trigger released");

				isGrabbing = false;
				last_t_anim = t_anim;
			}
		}
	}

	void OnHandHoverBegin(Hand hand)
	{
		attachedHand = hand;
	}

	void HandHoverUpdate(Hand hand)
	{
		if (!isGrabbing)
		{
			if (hand.GetStandardInteractionButtonDown())
			{
				Debug.Log("Door handler trigger pressed");
				{
					grabPos = attachedHand.transform.localPosition;
					Debug.Log("New Grab Position: " + grabPos.x);
					isGrabbing = true;
				}
			}
		}
	}

	void OnHandHoverEnd(Hand hand)
	{
		if (isGrabbing)
			Debug.Log("The hand isn't colliding anymore but we are still grabbing");
	}
}
