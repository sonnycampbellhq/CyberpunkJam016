using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportDestination;
    public string playerTag = "Player";

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTeleporting) return;

        if (other.CompareTag(playerTag) && teleportDestination != null)
        {
            Teleport(other);
        }
    }

    private void Teleport(Collider player)
    {
        isTeleporting = true;

        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
            controller.enabled = false;

        player.transform.position = teleportDestination.position;

        if (controller != null)
            controller.enabled = true;

        isTeleporting = false;
    }
}
