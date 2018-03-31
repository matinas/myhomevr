using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Remote))]
public class RemoteRaycaster : MonoBehaviour {

    [SerializeField]
    [Tooltip("Max distance for the remote to hit")]
    private float range = 10;
    
    private void Start()
    {
        GetComponent<Remote>().OnRemotePressed += HandleRemotePressed;
    }

	private void HandleRemotePressed()
    {
        Ray ray = new Ray(transform.position, transform.right);
		var hitInfos = Physics.RaycastAll(ray, range).OrderBy(t=> t.distance);

		foreach(var hit in hitInfos)
        {
            RemoteInteractable interactable = hit.collider.GetComponent<RemoteInteractable>();
            if (interactable != null)
            {
                interactable.TookHit(hit);
                break;
            }
        }
	}

}
