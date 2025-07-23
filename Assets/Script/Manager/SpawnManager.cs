using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void RespawnPlayer(GameObject player, Vector2 position, float delay)
    {
        StartCoroutine(RespawnCoroutine(player, position, delay));
    }

    IEnumerator RespawnCoroutine(GameObject player, Vector2 position, float delay)
    {
        yield return new WaitForSeconds(delay);

        player.transform.position = position;
        player.SetActive(true);

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.ResetHealth();
        }
    }
}
