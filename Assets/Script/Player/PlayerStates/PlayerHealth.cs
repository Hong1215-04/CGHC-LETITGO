using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false;
    public AvalancheController avalanche;

    public void Kill()
    {
        if (isDead) return;
        isDead = true;

        PlayerRespawn respawn = GetComponent<PlayerRespawn>();
        var transition = FindObjectOfType<SceneTransitionController>();
        var triggers = FindObjectsOfType<AvalancheTrigger>(); // ✅ 支持多个触发器

        void DoRespawn()
        {
            if (respawn != null)
            {
                if (avalanche != null)
                    avalanche.ResetToPosition(respawn.GetAvalancheSpawnPos());

                // ✅ 重置所有 AvalancheTrigger
                foreach (var trigger in triggers)
                {
                    if (trigger != null)
                        trigger.ResetTrigger();
                }

                transform.position = respawn.GetRespawnPosition();
                respawn.HandleRespawn();
            }

            ResetHealth();
            gameObject.SetActive(true);
        }

        if (transition != null)
        {
            gameObject.SetActive(false);
            transition.PlayTransition(DoRespawn);
        }
        else
        {
            DoRespawn();
        }
    }

    public void ResetHealth()
    {
        isDead = false;
    }
}
