using UnityEngine;

public class TreasureItem : MonoBehaviour
{
    [Header("Floating Animation")]
    public float floatAmplitude = 0.2f;  // 浮动高度
    public float floatFrequency = 1f;    // 浮动速度

    [Header("Pickup Effect")]
    public AudioClip collectSFX;         // 拾取音效
    public GameObject visualEffect;      // 可选特效（例如粒子）

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSFX != null)
                AudioSource.PlayClipAtPoint(collectSFX, transform.position);

            if (visualEffect != null)
                Instantiate(visualEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
