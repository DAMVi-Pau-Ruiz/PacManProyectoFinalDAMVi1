using UnityEngine;

public class WarpTunnelController : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;

    private static float cooldownTime = 0.2f;
    private static float lastWarpTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (Time.time < lastWarpTime)
            return;

        // Teleport
        collision.transform.position = exitPoint.position;

        // reset cooldown
        lastWarpTime = Time.time + cooldownTime;
    }
}