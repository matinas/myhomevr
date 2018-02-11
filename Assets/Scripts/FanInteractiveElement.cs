using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class FanInteractiveElement : MonoBehaviour
{
	[SerializeField] private VRInteractiveItem m_InteractiveItem;
	[SerializeField] private SelectionRadial m_SelectionRadial;
	private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.

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
		// If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
		if(m_GazeOver)
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
}
