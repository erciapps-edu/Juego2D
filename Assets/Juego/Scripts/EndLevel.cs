using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public AudioClip endLevelSound;
    private bool nivelTerminado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (nivelTerminado) return;

        if (collision.GetComponent<Player>() != null)
        {
            nivelTerminado = true;

            if (endLevelSound != null)
            {
                AudioSource.PlayClipAtPoint(endLevelSound, transform.position);
            }

            AvanzarSiguienteNivel();
        }
    }

    private void AvanzarSiguienteNivel()
    {
        int indiceActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteIndice = indiceActual + 1;

        if (siguienteIndice < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguienteIndice);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}