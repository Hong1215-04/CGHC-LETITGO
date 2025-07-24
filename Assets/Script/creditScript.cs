using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsScript : MonoBehaviour
{
    public float scrollSpeed = 40f;
    public float duration = 10f; // Seconds before returning to Start Screen
    public string startSceneName = "StartScreen"; // Replace with your actual scene name

    private RectTransform rectTransform;
    private float timer;
    [SerializeField] public GameObject AudioScene;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        timer = duration; // Start countdown
        AudioScene.SetActive(true);
    }

    void Update()
    {
        // Scroll the credits upwards
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // Countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            AudioScene.SetActive(false);
            SceneManager.LoadScene(startSceneName);
        }
    }


}
