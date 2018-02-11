using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace HomeVR {
	public class SceneManager : MonoBehaviour {

		[SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
		[SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.

		// Use this for initialization
		void Awake () {
			
			m_Reticle.Show();
			m_Radial.Hide();
		}
	}
}
