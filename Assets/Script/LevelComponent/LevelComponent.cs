using UnityEngine;

public class LevelComponent : MonoBehaviour, IDamageable
{
    public virtual void Damage()
    {
        var playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Kill();
        }
    }
}
