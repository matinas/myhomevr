using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VRStandardAssets.Utils;

public class TVInteractiveElement : MonoBehaviour
{
	[SerializeField] private VRInteractiveItem m_InteractiveItem;
	[SerializeField] private SelectionRadial m_SelectionRadial;
	private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.

	private VideoPlayer player;

	private void Awake()
	{
		player = GetComponent<VideoPlayer>();
	}

	// Use this for initialization
	void OnEnable () {
		
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;

		if (m_SelectionRadial.m_Full2DUI)
		{
			m_InteractiveItem.OnClick += HandleClick;
			m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
		}
		
		m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
	}
	
	void OnDisable () {
		
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;

		if (m_SelectionRadial.m_Full2DUI)
		{
			m_InteractiveItem.OnClick -= HandleClick;
			m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
		}

		m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
	}

	void HandleOver()
	{
		m_SelectionRadial.Show();

		if (m_SelectionRadial.m_GazeBased)
			m_SelectionRadial.m_SelectionFillRoutine = StartCoroutine(m_SelectionRadial.FillSelectionRadial());

		m_GazeOver = true;
	}

	void HandleOut()
	{
		if (m_SelectionRadial.m_GazeBased)
			StopCoroutine(m_SelectionRadial.m_SelectionFillRoutine); // We stop it from this MonoBehaviours because it's the one that triggered the coroutine
		else
			if (m_SelectionRadial.m_SelectionFillRoutine != null)
			{
				m_SelectionRadial.StopCoroutine(m_SelectionRadial.m_SelectionFillRoutine); // We stop it from the SelectionBar MonoBehaviours because it's the one that triggered the coroutine
				m_SelectionRadial.m_SelectionFillRoutine = null;
			}

		m_SelectionRadial.Hide();

		m_GazeOver = false;
	}

	void HandleClick()
	{
		
	}

	void HandleDoubleClick()
	{
	}

	void HandleSelectionComplete()
	{
		if(m_GazeOver)
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
}
