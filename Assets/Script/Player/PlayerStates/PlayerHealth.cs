using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false;

    public void Kill()
    {
        if (isDead) return;

        isDead = true;

        PlayerRespawn respawn = GetComponent<PlayerRespawn>();
        SceneTransitionController transition = FindObjectOfType<SceneTransitionController>();
        if (transition != null && respawn != null)
        {
            transition.PlayTransition(() =>
            {
                gameObject.transform.position = respawn.GetRespawnPosition();
                ResetHealth();
                gameObject.SetActive(true);
            });
            gameObject.SetActive(false); // Disable after starting transition
        }
        else
        {
            // fallback: just respawn without transition
            if (respawn != null)
            {
                gameObject.transform.position = respawn.GetRespawnPosition();
                ResetHealth();
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");

        gameObject.SetActive(false);

        PlayerRespawn respawn = GetComponent<PlayerRespawn>();
        if (respawn != null && RespawnManager.instance != null)
        {
            RespawnManager.instance.RespawnPlayer(gameObject, respawn.GetRespawnPosition(), respawn.delayBeforeRespawn);
        }
    }

    public void ResetHealth()
    {
        isDead = false;
    }


    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.V))
    //     {
    //         Kill();
    //     }
    // }
}