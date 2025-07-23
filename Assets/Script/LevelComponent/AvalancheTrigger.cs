using UnityEngine;

public class AvalancheTrigger : MonoBehaviour
{
    public AvalancheController avalanche;
    public Transform avalancheSpawnPoint;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (avalanche != null)
            {
                avalanche.transform.position = avalancheSpawnPoint.position;
                avalanche.StartAvalanche();
            }
        }
    }

    public void ResetTrigger()
    {
        hasTriggered = false;
    }
}
