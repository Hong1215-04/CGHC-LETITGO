using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead = false;

    public void Kill()
    {
        if (isDead) return;

        isDead = true;
        Die();
    }

    private void Die()
    {
        Debug.Log("Player Died!");

        gameObject.SetActive(false);
    }

    public void ResetHealth()
    {
        isDead = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Kill();
        }
    }
}