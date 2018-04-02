using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class GrabHandler : MonoBehaviour {

	private bool isGrabbing;

	private Animator anim;

	private Vector3 grabPos;

	private Hand attachedHand;

	[RangeAttribute(0.0f,1.0f)]
	public float animTime = 0.8f;

	private float nPos, lastNPos = 0.0f;

	// Use this for initialization
	void Awake ()
	{
		anim = gameObject.GetComponentInParent<Animator>();
	}
	
	// FIXME: This is not working 100% fine yet. There is a little jumpliness when we grab the handle
	// pull it a bit, release it and try to grab and pull it again. I guess the jump in the animation
	// is due to the sign chance in the vector substraction below (jump happens when diff is near zero,
	// i.e.: when you have just grabbed the handle the second time)

	void Update()
	{
		if (isGrabbing)
		{
			if (attachedHand != null)
			{
				Vector3 diff = attachedHand.transform.position - grabPos;
				nPos = diff.magnitude * Mathf.Sign(Vector3.Dot(diff,attachedHand.transform.forward));

				// Debug.Log("Grab Position: " + grabPos);
				// Debug.Log("New Hand Position: " + attachedHand.transform.position);
				// Debug.Log("Diff: " + diff.ToString("F6") + ", magnitude: " + diff.magnitude);

				nPos = Mathf.Clamp01(lastNPos+nPos);
				anim.SetFloat("Door",nPos);
				
				// Debug.Log("nPos:" + nPos);

				if (attachedHand.GetStandardInteractionButtonUp())
				{
					Debug.Log("Door handle trigger released");

					isGrabbing = false;
					lastNPos = nPos;
				}
			}
			else
			{
				anim.SetFloat("Door",animTime); // Just for debug when no hand is attached
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
					grabPos = attachedHand.transform.position;
					Debug.Log("New Grab Position: " + grabPos);
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
