using UnityEngine;

public enum LocomotionType { NONE, TELEPORT_POINTS, TELEPORT_AREA, TELEPORT_POINT_AREA }

public class LocomotionController : MonoBehaviour
{
    private LocomotionType activeLocomotion;

    [SerializeField]
    [Tooltip("Reference to the game object including all the teleport points")]
    private GameObject teleportPoints;

    [SerializeField]
    [Tooltip("Reference to the game object including all the teleport areas")]
    private GameObject teleportAreas;

    void Awake()
    {
        activeLocomotion = LocomotionType.NONE;

        teleportPoints.SetActive(false);
        teleportAreas.SetActive(false);
    }

    public void activateLocomotion(LocomotionType lt)
    {
        switch(lt)
        {
            case LocomotionType.TELEPORT_POINTS:
            {
                teleportPoints.SetActive(true);
                activeLocomotion = (activeLocomotion == LocomotionType.TELEPORT_AREA) ? LocomotionType.TELEPORT_POINT_AREA : LocomotionType.TELEPORT_POINTS;
                break;
            }
            case LocomotionType.TELEPORT_AREA:
            {
                teleportAreas.SetActive(true);
                activeLocomotion = activeLocomotion == LocomotionType.TELEPORT_POINTS ? LocomotionType.TELEPORT_POINT_AREA : LocomotionType.TELEPORT_AREA;
                break;
            }
        }
    }

    public void deactivateLocomotion(LocomotionType lt)
    {
        switch(lt)
        {
            case LocomotionType.TELEPORT_POINTS:
            {
                teleportPoints.SetActive(false);
                activeLocomotion = activeLocomotion == LocomotionType.TELEPORT_POINT_AREA ? LocomotionType.TELEPORT_AREA : LocomotionType.NONE;
                break;
            }
            case LocomotionType.TELEPORT_AREA:
            {
                teleportAreas.SetActive(false);
                activeLocomotion = activeLocomotion == LocomotionType.TELEPORT_POINT_AREA ? LocomotionType.TELEPORT_POINTS : LocomotionType.NONE;
                break;
            }
        }
    }
}