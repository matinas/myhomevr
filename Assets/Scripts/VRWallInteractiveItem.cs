using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class VRWallInteractiveItem : MonoBehaviour
{
	public VRActionTriggerer m_actionTriggerer;

	[SerializeField] private Material m_NormalMaterial;                
    [SerializeField] private Material m_OverMaterial;                  
    [SerializeField] private Material m_ClickedMaterial;               
    [SerializeField] private Material m_DoubleClickedMaterial;         
    [SerializeField] private Renderer m_Renderer;
	
	// Use this for initialization
	void OnEnable () {
		m_actionTriggerer.OnActionTrigger += HandleActionTrigger;
		m_actionTriggerer.OnOut += HandleOut;
	}
	
	void OnDisable () {
		m_actionTriggerer.OnActionTrigger -= HandleActionTrigger;
		m_actionTriggerer.OnOut -= HandleOut;
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

	void HandleActionTrigger()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_DoubleClickedMaterial;
	}
}
