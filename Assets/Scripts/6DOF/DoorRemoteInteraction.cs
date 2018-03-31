using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(RemoteInteractable))]
public class DoorRemoteInteraction : MonoBehaviour {

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private string trigger;

    private bool isOpened;

    private AudioSource audio;
    private Animator anim;

    void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        GetComponent<RemoteInteractable>().OnTookHit += HandleTookHit;
        isOpened = false;
    }

	public void HandleTookHit(RaycastHit hit)
    {
        // Open the door...

        if (!isOpened)
        {
            audio.clip = openSound;
        }
        else
        {
            audio.volume = 0.25f;
            audio.clip = closeSound;
        }

        isOpened = !isOpened;
        audio.Play();

        if (anim != null)
			anim.SetTrigger(trigger);
	}
}
