using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using VRStandardAssets.Utils;

public class VRAnimationInteractiveItem : MonoBehaviour
{
	[SerializeField] private VRActionTriggerer m_actionTriggerer;
	[SerializeField] private Animator anim;
	[SerializeField] private string trigger;

	// Use this for initialization
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
		if (anim != null)
			anim.SetTrigger(trigger);
	}
}
