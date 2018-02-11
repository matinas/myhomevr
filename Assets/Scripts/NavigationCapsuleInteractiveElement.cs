using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class NavigationCapsuleInteractiveElement : MonoBehaviour {

	[SerializeField] private Material m_NormalMaterial;
	[SerializeField] private Material m_OverMaterial;
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    [SerializeField] private Renderer m_Renderer;
	[SerializeField] private SelectionRadial m_SelectionRadial;
	[SerializeField] private VRCameraFade m_CameraFade;                 // This fades the scene out when a new scene is about to be loaded.
	[SerializeField] private Transform mainCamera;

	private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.

	[SerializeField] private AudioClip m_teleport_ini;
	[SerializeField] private AudioClip m_teleport_end;

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

		if (m_Renderer != null)
			m_Renderer.material = m_OverMaterial;

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
	}

	void HandleDoubleClick()
	{
	}

	void HandleSelectionComplete()
	{
		// If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
		if(m_GazeOver)
		{
			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.clip = m_teleport_ini;
			audioSource.Play();
			StartCoroutine (Teleport());
		}
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
