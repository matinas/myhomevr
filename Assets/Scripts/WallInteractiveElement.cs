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

	// Use this for initialization
	void OnEnable () {
		
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_InteractiveItem.OnClick += HandleClick;
		m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
	}
	
	void OnDisable () {
		
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
		m_InteractiveItem.OnClick -= HandleClick;
		m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
	}

	private void Awake ()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_NormalMaterial;
	}

	void HandleOver()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_OverMaterial;
	}

	void HandleOut()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_NormalMaterial;
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
}
