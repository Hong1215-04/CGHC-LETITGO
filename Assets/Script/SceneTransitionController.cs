using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionController : MonoBehaviour
{
    public Material wipeMaterial;
    public Image transitionImage;
    public float duration = 1f;

    private void Awake()
    {
        // Assign a new material instance to avoid modifying the original asset
        if (transitionImage != null)
            transitionImage.material = new Material(wipeMaterial);

        // Hide the transition image at start
        if (transitionImage != null)
            transitionImage.gameObject.SetActive(false);
    }

    // Play the transition animation, execute onMidAction at the midpoint
    public void PlayTransition(System.Action onMidAction)
    {
        StartCoroutine(DoTransition(onMidAction));
    }

    private IEnumerator DoTransition(System.Action onMidAction)
    {
        // Show the transition image
        transitionImage.gameObject.SetActive(true);

        // Shrink (start transition)
        float time = 0f;
        while (time < duration)
        {
            float cutoff = Mathf.Lerp(1f, 0f, time / duration);
            transitionImage.material.SetFloat("_Cutoff", cutoff);
            time += Time.deltaTime;
            yield return null;
        }
        transitionImage.material.SetFloat("_Cutoff", 0f);

        // Execute mid-action logic (e.g., respawn)
        onMidAction?.Invoke();

        // Wait a moment before expanding
        yield return new WaitForSeconds(0.3f);

        // Expand (end transition)
        time = 0f;
        while (time < duration)
        {
            float cutoff = Mathf.Lerp(0f, 1f, time / duration);
            transitionImage.material.SetFloat("_Cutoff", cutoff);
            time += Time.deltaTime;
            yield return null;
        }
        transitionImage.material.SetFloat("_Cutoff", 1f);

        // Hide the transition image after animation
        transitionImage.gameObject.SetActive(false);
    }
}
