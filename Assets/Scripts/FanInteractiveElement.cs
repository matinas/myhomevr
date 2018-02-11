using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class FanInteractiveElement : MonoBehaviour
{
	[SerializeField] private VRInteractiveItem m_InteractiveItem;

	// Use this for initialization
	void OnEnable () {
		
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnClick += HandleClick;
	}
	
	void OnDisable () {
		
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnClick -= HandleClick;
	}

	void HandleOver()
	{
		Debug.Log("Press your controller button to turn on/off the fan");
	}

	void HandleClick()
	{
        AudioSource audio = GetComponent<AudioSource>();
		FanController fanController = GetComponent<FanController>();

        if (audio != null)
		{
			if (audio.isPlaying)
			{
				audio.Stop();
				fanController.enabled = false;
			}
			else
			{
				audio.Play();
				fanController.enabled = true;
			}
		}
	}
}
