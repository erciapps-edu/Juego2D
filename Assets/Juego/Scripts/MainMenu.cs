using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void JugarNivel(string nivel)
    {
        SceneManager.LoadScene("Level"+nivel);
    }

    public void Salir()
    {
        Application.Quit();
    }
}