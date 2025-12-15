using UnityEngine;
using DoorScript;

public class DoorAutoTrigger : MonoBehaviour
{
    public Door door;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (playerInside) return;

        playerInside = true;
        door.OpenAwayFrom(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = false;
    }
}
