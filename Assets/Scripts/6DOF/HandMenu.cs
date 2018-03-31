using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Hand))]
public class HandMenu : MonoBehaviour {

	[SerializeField]
	[Tooltip("Reference to the Menu to be anchored in front of the player")]
	public Transform menu;

	[SerializeField]
	[Tooltip("Reference to the Locomotion Controller")]
	public LocomotionController lc;

	private Hand hand;

	private GameObject[] indicatorSpheres;

	private SteamVR_Controller.Device device;

	void Start()
	{
		hand = GetComponent<Hand>();

		indicatorSpheres = new GameObject[2];

		indicatorSpheres[0] = GameObject.Find("IndicatorSphere1");
		indicatorSpheres[1] = GameObject.Find("IndicatorSphere2");

		foreach (GameObject go in indicatorSpheres)
		{
			go.SetActive(false);
		}

		menu.gameObject.SetActive(false);
	}

	void Update()
	{
		if (hand.controller != null)
		{
			device = SteamVR_Controller.Input((int) hand.controller.index);

			if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
			{
				menu.gameObject.SetActive(true);
				menu.transform.position = hand.transform.position + hand.transform.forward*0.25f;
				menu.transform.eulerAngles = new Vector3(0.0f,hand.transform.eulerAngles.y,0.0f);
			}
		}
	}

	public void HandleSelectTeleporPoints()
	{
		if (!indicatorSpheres[0].activeSelf)
		{
			indicatorSpheres[0].SetActive(true);
			lc.activateLocomotion(LocomotionType.TELEPORT_POINTS);
		}
		else
		{
			indicatorSpheres[0].SetActive(false);
			lc.deactivateLocomotion(LocomotionType.TELEPORT_POINTS);
		}
	}

	public void HandleSelectTeleportArea()
	{
		if (!indicatorSpheres[1].activeSelf)
		{
			indicatorSpheres[1].SetActive(true);
			lc.activateLocomotion(LocomotionType.TELEPORT_AREA);
		}
		else
		{
			indicatorSpheres[1].SetActive(false);
			lc.deactivateLocomotion(LocomotionType.TELEPORT_AREA);
		}
	}
}
