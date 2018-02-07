using UnityEngine;
using UnityEngine.VR;

public class VRToggler : MonoBehaviour {

	[SerializeField] private float m_RenderScale = 10.5f; // The render scale. Higher numbers = better quality, but trades performance

	private void Start()
	{
		VRSettings.renderScale = m_RenderScale; // We also set the renderScale...
	}

	// Use this for initialization
	private void Update ()
    {
        // If C is pressed, toggle VRSettings.enabled
        if (Input.GetKeyDown(KeyCode.C))
        {
            VRSettings.enabled = !VRSettings.enabled;
			string[] st = VRSettings.supportedDevices;

            Debug.Log("Changed VRSettings.enabled to:" + VRSettings.enabled);
			Debug.Log("Supported devices:");
			foreach (string s in st)
				Debug.Log(s);
        }
    }
}
