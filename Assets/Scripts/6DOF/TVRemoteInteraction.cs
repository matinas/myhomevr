using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(RemoteInteractable))]
public class TVRemoteInteraction : MonoBehaviour {

    private VideoPlayer m_player;
	private AudioSource m_audio;
    private bool isPoweredOn;

    void Awake()
    {
        GetComponent<RemoteInteractable>().OnTookHit += HandleTookHit;
        isPoweredOn = false;

        m_player = GetComponent<VideoPlayer>();
        m_audio = GetComponent<AudioSource>();
    }

	public void HandleTookHit(RaycastHit hit)
    {
        // Power on the TV...

        if (!isPoweredOn)
        {
            if (m_player != null)
            {
                m_player.enabled = true;
                m_player.Play();
                m_audio.Play();
            }

            isPoweredOn = true;
        }
        else
        {
            if (m_player != null)
            {
                m_audio.Stop();
                m_player.Stop(); // There is a bug in this call which triggers a spike in frame time (400 ms): https://issuetracker.unity3d.com/issues/videoplayer-dot-stop-causes-a-performance-spike
                m_player.enabled = false;
            }

            isPoweredOn = false;
        }
	}
}
