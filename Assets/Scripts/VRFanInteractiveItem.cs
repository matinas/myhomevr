using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class VRFanInteractiveItem : MonoBehaviour
{
	public VRActionTriggerer m_actionTriggerer;

	// Use this for initialization
	void OnEnable () {
		
		m_actionTriggerer.OnActionTrigger += HandleActionTrigger;
	}
	
	void OnDisable () {
		
		m_actionTriggerer.OnActionTrigger -= HandleActionTrigger;
	}

	void HandleActionTrigger()
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
