using System;
using UnityEngine;
using UnityEngine.Video;
using VRStandardAssets.Utils;
using UnityEngine.UI;

public class VRActionTriggerer : MonoBehaviour
{
	public event Action OnActionTrigger;
	public event Action OnOver;
	public event Action OnOut;
	
	[SerializeField] private VRInteractiveItem m_InteractiveItem;
	[SerializeField] private SelectionRadial m_SelectionRadial;
	[SerializeField] private Text m_tooltipText;
	[SerializeField] private string m_text;
	[SerializeField] private Color m_textColor;
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
		if (m_tooltipText != null)
		{
			if (m_text != String.Empty) m_tooltipText.text = m_text;
			m_tooltipText.color = new Color(m_textColor.r,m_textColor.g,m_textColor.b); // Direct assignation .color = m_textColor didn't work...
		}
		
		m_SelectionRadial.Show();

		if (OnOver != null)
			OnOver();

		if (m_SelectionRadial.m_GazeBased)
			m_SelectionRadial.m_SelectionFillRoutine = StartCoroutine(m_SelectionRadial.FillSelectionRadial());

		m_GazeOver = true;
	}

	void HandleOut()
	{
		if (m_tooltipText != null) m_tooltipText.text = String.Empty;

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

		if (OnOut != null)
			OnOut();
	}

	void HandleClick()
	{
		if (m_SelectionRadial.m_Full2DUI)
			OnActionTrigger();
	}

	void HandleDoubleClick()
	{
		if (m_SelectionRadial.m_Full2DUI)
			OnActionTrigger();
	}

	void HandleSelectionComplete()
	{
		if(m_GazeOver)
			OnActionTrigger();
	}
}
