using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	[SerializeField] private VRActionTriggerer m_actionTriggerer;

	[SerializeField] private bool isOpen;

	[SerializeField] private AudioClip openSound;
	[SerializeField] private AudioClip closeSound;

	void OnEnable ()
	{
		m_actionTriggerer.OnActionTrigger += HandleActionTrigger;
	}
	
	void OnDisable ()
	{
		m_actionTriggerer.OnActionTrigger -= HandleActionTrigger;
	}

	void HandleActionTrigger()
	{
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		if (audio != null)
		{
			if (!isOpen)
				audio.clip = openSound;
			else
			{
				audio.volume = 0.25f;
				audio.clip = closeSound;
			}

			isOpen = !isOpen;
			audio.Play();
		}
	}
}
