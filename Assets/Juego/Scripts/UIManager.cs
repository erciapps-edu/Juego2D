using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text fruitText;

    private int totalFruits = 0;
    private int collectedFruits = 0;

    public TMP_Text shootText;
    private int collectedShoots;

    public TMP_Text healthText;

    private int vidaMaxima = 3;
    private int vidaActual;

    private void Start()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");

        totalFruits = fruits.Length;
        collectedShoots = totalFruits;

        vidaActual = 3;

        UpdateFruitText();
        UpdateShootText();
        UpdateHealthText();
    }

    public void FruitCollected()
    {
        collectedFruits++;
        collectedShoots++;
        
        UpdateFruitText();

        UpdateShootText();
    }

    private void UpdateFruitText()
    {
        fruitText.text = $"{collectedFruits} / {totalFruits}";
    }

    private void UpdateShootText()
    {
        shootText.text = $"{collectedShoots}";
    }

    public void UpdateHealth(int vida)
    {
        vidaActual = vida;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"{vidaActual} / {vidaMaxima}";
        }
    }

    public bool GastarDisparo()
    {
        if (collectedShoots > 0)
        {
            collectedShoots--;
            UpdateShootText();
            return true;
        }
        
        return false;
    }
}