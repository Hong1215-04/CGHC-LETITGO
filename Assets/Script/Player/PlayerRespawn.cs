using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnPosition;
    private Vector2 avalancheSpawnPos;
    private PlayerHealth health;

    [Header("Respawn Settings")]
    public float delayBeforeRespawn = 1.5f;
    public AvalancheController avalanche;
    public float avalancheOffsetX = -5f;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        respawnPosition = transform.position;
    }

    public void UpdateCheckpoint(Vector2 newPos, Vector2 avalanchePos)
    {
        respawnPosition = newPos;
        avalancheSpawnPos = avalanchePos;
        Debug.Log($"Checkpoint updated: Player @ {respawnPosition}, Avalanche @ {avalancheSpawnPos}");
    }

    public Vector2 GetRespawnPosition() => respawnPosition;
    public Vector2 GetAvalancheSpawnPos() => avalancheSpawnPos;

    public void HandleRespawn()
    {
        health.ResetHealth();
        transform.position = respawnPosition;

        if (avalanche != null)
            avalanche.ResetToPosition(avalancheSpawnPos);
    }
}
