using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnPosition;

    [Header("Respawn Delay")]
    public float delayBeforeRespawn = 1.5f;

    private void Start()
    {
        respawnPosition = transform.position; // 初始点
    }

    public void UpdateCheckpoint(Vector2 newPos)
    {
        respawnPosition = newPos;
    }

    public Vector2 GetRespawnPosition()
    {
        return respawnPosition;
    }
}
