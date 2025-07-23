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
            SetAlpha(1);
        }

        if (transition != null)
        {
            SetAlpha(0);
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
    void SetAlpha(float alpha)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color color = sr.color;
            color.a = Mathf.Clamp01(alpha); // Ensure alpha stays between 0 and 1
            sr.color = color;
        }
    }
}
