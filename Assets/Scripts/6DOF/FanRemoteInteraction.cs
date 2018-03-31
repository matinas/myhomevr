using UnityEngine;

[RequireComponent(typeof(RemoteInteractable))]
[RequireComponent(typeof(FanController))]
public class FanRemoteInteraction : MonoBehaviour {

    private bool isPoweredOn;
    private FanController fanController;

    private AudioSource audio;

    void Awake()
    {
        GetComponent<RemoteInteractable>().OnTookHit += HandleTookHit;
        isPoweredOn = false;
        fanController = GetComponent<FanController>();
        audio = GetComponent<AudioSource>();
    }

	public void HandleTookHit(RaycastHit hit)
    {
        // Power on the fan...

        if (!isPoweredOn)
        {
            fanController.enabled = true;
            audio.Play();
            isPoweredOn = true;
        }
        else
        {
            fanController.enabled = false;
            audio.Stop();
            isPoweredOn = false;
        }
	}
}
