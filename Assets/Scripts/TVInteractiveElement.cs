using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VRStandardAssets.Utils;

public class TVInteractiveElement : MonoBehaviour
{
	[SerializeField] private VRInteractiveItem m_InteractiveItem;

	private VideoPlayer player;

	private void Awake()
	{
		player = GetComponent<VideoPlayer>();
	}

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
		Debug.Log("Press your controller button to turn on/off the TV");
	}

	void HandleClick()
	{
		if (player != null)
		{
			if (player.isPlaying)
			{
				player.Stop();
				player.enabled = false;
			}
			else
			{
				player.enabled = true;
				player.Play();
			}
		}
	}
}
