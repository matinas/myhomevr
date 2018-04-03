using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class GrabHandler : MonoBehaviour {

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

	// NOTE: the behavior expected for this script was substituted by using the LinearDrive and
	// LinearMapping scripts from the Interaction System of SteamVR to handle the mapping between
	// hand movement and the door animation.

	// In CalculateLinearMapping() of LinearDrive what they are doing is just projecting the controller movement
	// vector to the normalized direction vector to get the length of the vector (that's the lineal mapping basically).
	// "When normalizing a vector you are making its length 1 - finding the unit vector that points in the same direction.
	// This is useful for various purposes, for example, if you take the dot product of a vector with a unit vector you
	// have the length of the component of that vector in the direction of the unit vector"

	void Update()
	{
		if (attachedHand == null)
		{
			anim.SetFloat("Door",animTime); // Just for debug when no hand is attached
		}
	}

	void OnHandHoverBegin(Hand hand)
	{
		attachedHand = hand;
	}

	void HandHoverUpdate(Hand hand)
	{
		if (!hand.hoverLocked)
		{
			if (hand.GetStandardInteractionButtonDown())
			{
				Debug.Log("Door handler trigger pressed");
				{
					grabPos = attachedHand.transform.position;
					Debug.Log("New Grab Position: " + grabPos);
				}

				hand.HoverLock(GetComponent<Interactable>());
			}
		}
		else
		{
			Vector3 diff = attachedHand.transform.position - grabPos;
			nPos = diff.magnitude * Mathf.Sign(Vector3.Dot(diff.normalized,attachedHand.transform.forward));

			// Debug.Log("Grab Position: " + grabPos);
			// Debug.Log("New Hand Position: " + attachedHand.transform.position);
			// Debug.Log("Diff: " + diff.ToString("F6") + ", magnitude: " + diff.magnitude);

			nPos = Mathf.Clamp01(lastNPos+nPos);
			anim.SetFloat("Door",nPos);
			
			// Debug.Log("nPos:" + nPos);

			if (attachedHand.GetStandardInteractionButtonUp())
			{
				Debug.Log("Door handle trigger released");

				lastNPos = nPos;

				hand.HoverUnlock(GetComponent<Interactable>());
			}
		}
	}
}
