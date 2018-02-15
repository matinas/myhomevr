using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VRStandardAssets.Utils;

public class VRTVInteractiveItem : MonoBehaviour
{
	[SerializeField] private VRActionTriggerer m_actionTriggerer;
	[SerializeField] private VideoPlayer m_player;
	[SerializeField] private AudioSource m_audio;

	private void Awake()
	{
		m_player = GetComponent<VideoPlayer>();
		m_audio = GetComponent<AudioSource>();
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
		if (m_player != null)
		{
			if (m_player.isPlaying)
			{
				m_player.Stop();
				m_audio.Stop();
				m_player.enabled = false;
			}
			else
			{
				m_player.enabled = true;
				m_player.Play();
				m_audio.Play();
			}
		}
	}
}
