using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            var respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                Vector2 checkpointPos = transform.position;
                Vector2 avalanchePos = checkpointPos + new Vector2(respawn.avalancheOffsetX, 0f);

                respawn.UpdateCheckpoint(checkpointPos, avalanchePos);
                activated = true;
            }
        }
    }
}
