using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image whiteFadePanel;
    public float fadeDuration = 2f;
    public string nextSceneName;

    [Header("References")]
    public GameObject player;
    public AvalancheController avalanche;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(FadeToWhite());
        }
    }

    private System.Collections.IEnumerator FadeToWhite()
    {
        // 🔒 Freeze player
        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true; // prevent physics movement
            }

            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false; // disable all movement scripts (like Player_Movement)
            }
        }

        // ⛰️ Stop avalanche
        if (avalanche != null)
        {
            avalanche.StopAvalanche();
        }

        // 🌫️ Begin white fade
        float timer = 0f;
        Color color = whiteFadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            whiteFadePanel.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        whiteFadePanel.color = new Color(color.r, color.g, color.b, 1f);

        // 🛫 Optional: switch scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
