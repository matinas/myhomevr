using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace HomeVR {
	public class SceneManager : MonoBehaviour {

		[SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
		[SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
		[SerializeField] private Transform m_mainCamera;

		// Use this for initialization
		void Awake () {
			
			m_Reticle.Show();
			m_Radial.Hide();
		
			#if UNITY_ANDROID || UNITY_EDITOR
				m_mainCamera.position = new Vector3(m_mainCamera.position.x,m_mainCamera.position.y+1.6f,m_mainCamera.position.z);
			#endif

			#if UNITY_EDITOR
            	m_mainCamera.GetComponent<MoveCamera>().enabled = true;
        	#else
	            m_mainCamera.GetComponent<MoveCamera>().enabled = true;
    	    #endif
		}
	}
}
