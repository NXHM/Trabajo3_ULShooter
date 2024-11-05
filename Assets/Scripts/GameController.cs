using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Agregar referencia al canvas de game over
    public GameObject gameOverCanvas;
    [SerializeField] private PlayerHealth m_PlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
        // Suscribirse al evento OnPlayerDeath de PlayerHealth
        m_PlayerHealth.OnPlayerDeath += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver()
    {
        Debug.Log("El juego ha terminado");

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Detener el tiempo del juego
        Time.timeScale = 0f;
    }
}
