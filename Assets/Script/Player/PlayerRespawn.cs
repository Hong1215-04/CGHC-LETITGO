using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnPosition;
    private PlayerHealth health;

    [Header("Respawn Delay")]
    public float delayBeforeRespawn = 1.5f;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        respawnPosition = transform.position;
    }

    public void UpdateCheckpoint(Vector2 newPos)
    {
        respawnPosition = newPos;
        Debug.Log("Checkpoint saved at: " + respawnPosition);
    }

    public void HandleRespawn()
    {
        health.ResetHealth(); // Keep this
    }

    public Vector2 GetRespawnPosition()
    {
        return respawnPosition;
    }
}
