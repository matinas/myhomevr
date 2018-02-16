using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using VRStandardAssets.Utils;

public class VRTeleportInteractiveItem : MonoBehaviour
{
	public VRActionTriggerer m_actionTriggerer;

	[SerializeField] private Material m_NormalMaterial;
	[SerializeField] private Material m_OverMaterial;
    [SerializeField] private Renderer m_Renderer;
	[SerializeField] private VRCameraFade m_CameraFade;                 // This fades the scene out when a new scene is about to be loaded.
	[SerializeField] private Transform mainCamera;
	[SerializeField] private AudioClip m_teleport_ini;
	[SerializeField] private AudioClip m_teleport_end;
	[SerializeField] private Canvas m_canvas;
	[SerializeField] private Animator m_canvasAnimator;

	// Use this for initialization
	void OnEnable () {
		m_actionTriggerer.OnActionTrigger += HandleActionTrigger;
		m_actionTriggerer.OnOver += HandleOver;
		m_actionTriggerer.OnOut += HandleOut;
	}
	
	void OnDisable () {
		m_actionTriggerer.OnActionTrigger -= HandleActionTrigger;
		m_actionTriggerer.OnOver -= HandleOver;
		m_actionTriggerer.OnOut -= HandleOut;
	}

	void HandleOver()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_OverMaterial;
		
		if (m_canvas != null)
		{
			m_canvas.gameObject.SetActive(true);

			// Rotate canvas so to face the actual camera direction, taking the orientation from the main camera
			Transform vrCamera = mainCamera.GetChild(0).transform;
			m_canvas.transform.eulerAngles = new Vector3(0.0f,vrCamera.eulerAngles.y,0.0f);
		}
	}

	void HandleOut()
	{
		if (m_Renderer != null)
			m_Renderer.material = m_NormalMaterial;

		if (m_canvas != null)
			m_canvas.gameObject.SetActive(false);
	}

	void HandleActionTrigger()
	{
		// If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = m_teleport_ini;
		audioSource.Play();
		StartCoroutine (Teleport());
	}

	private IEnumerator Teleport()
	{
		// If the camera is already fading, ignore.
		if (m_CameraFade.IsFading)
			yield break;

		// Wait for the camera to fade out.
		yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

		Vector3 camRelativePos = gameObject.transform.position - mainCamera.GetChild(0).position;
		mainCamera.position += new Vector3(camRelativePos.x,0,camRelativePos.z);

		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.clip = m_teleport_end;
		audioSource.Play();

		yield return StartCoroutine(m_CameraFade.BeginFadeIn(true));
	}
}
