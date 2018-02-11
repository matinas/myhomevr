using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VRStandardAssets.Utils;

public class VRTVInteractiveItem : MonoBehaviour
{
	public VRActionTriggerer m_actionTriggerer;
	private VideoPlayer player;
	private AudioSource audio;

	private void Awake()
	{
		player = GetComponent<VideoPlayer>();
		audio = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void OnEnable () {
		
		m_actionTriggerer.OnActionTrigger += HandleActionTrigger;
	}
	
	void OnDisable () {
		
		m_actionTriggerer.OnActionTrigger -= HandleActionTrigger;
	}

	void HandleActionTrigger()
	{
		if (player != null)
		{
			if (player.isPlaying)
			{
				player.Stop();
				audio.Stop();
				player.enabled = false;
			}
			else
			{
				player.enabled = true;
				player.Play();
				audio.Play();
			}
		}
	}
}
