using UnityEngine;

public class Shoot : MonoBehaviour
{
    private UIManager uiManager;

    private bool lanzado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lanzado)
        {
            return;
        }

        if (collision.transform.CompareTag("Player"))
        {
            uiManager = FindAnyObjectByType<UIManager>();

            if (uiManager == null)
            {
                Debug.LogError("Falta un UIManager activo en el Canvas.", this);
                return;
            }

            lanzado = true;

            uiManager.FruitCollected();
            Destroy(gameObject);
        }
    }
}