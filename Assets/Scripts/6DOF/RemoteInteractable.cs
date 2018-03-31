using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RemoteInteractable : MonoBehaviour {

    public event Action<RaycastHit> OnTookHit = (hit) => { };

	public void TookHit(RaycastHit hit)
    {
        OnTookHit(hit);
	}
}
