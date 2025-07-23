using UnityEngine;

public class AvalancheController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public ParticleSystem avalancheParticles;

    private bool isActive = false;

    private void Start()
    {
        gameObject.SetActive(false);
        avalancheParticles.Stop();
    }

    private void Update()
    {
        if (isActive)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    public void StartAvalanche()
    {
        gameObject.SetActive(true);
        isActive = true;
        avalancheParticles.Play();
    }

    public void ResetToPosition(Vector2 position)
    {
        isActive = false;
        transform.position = position;
        avalancheParticles.Stop();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by avalanche!");
            var health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.Kill();
        }
    }

    public void StopAvalanche()
    {
        isActive = false;
        avalancheParticles.Stop();
    }
}
