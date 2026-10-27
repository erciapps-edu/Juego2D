using UnityEngine;

public class Fruit : MonoBehaviour
{
    public AudioClip fruitSound;
    private UIManager uiManager;

    // Evita contar esta moneda dos veces antes de que se elimine.
    private bool recogida = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (recogida)
        {
            return;
        }

        // La moneda solo se recoge al entrar el objeto con Tag Player.
        if (collision.transform.CompareTag("Player"))
        {
            // Buscamos el componente UIManager activo de la escena.
            uiManager = FindAnyObjectByType<UIManager>();

            if (uiManager == null)
            {
                Debug.LogError("Falta un UIManager activo en el Canvas.", this);
                return;
            }

            recogida = true;

            // Primero avisamos a la interfaz; después eliminamos la moneda.
            uiManager.FruitCollected();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(fruitSound, transform.position);
        }
    }
}