using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class WallInteractiveElement : MonoBehaviour
{
	[SerializeField] private Material m_NormalMaterial;                
    [SerializeField] private Material m_OverMaterial;                  
    [SerializeField] private Material m_ClickedMaterial;               
    [SerializeField] private Material m_DoubleClickedMaterial;         
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private Renderer m_Renderer;
	[SerializeField] private SelectionRadial m_SelectionRadial;
	private bool m_GazeOver; 											// Whether the user is looking at the VRInteractiveItem currently.

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

	private void Awake ()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_NormalMaterial;
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
		m_Renderer.material = m_NormalMaterial;
		m_GazeOver = false;
	}

	void HandleClick()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_ClickedMaterial;
	}

	void HandleDoubleClick()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_DoubleClickedMaterial;
	}

	void HandleSelectionComplete()
	{
		// If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
		if (m_GazeOver)
			if (m_Renderer != null)
				m_Renderer.material = m_DoubleClickedMaterial;
	}
}
