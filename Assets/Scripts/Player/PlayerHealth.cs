using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int shield = 100;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(int damage)
    {
        Debug.Log($"Daño recibido {damage}");
        if (shield > 0)
        {
            shield -= damage;
            Debug.Log($"Escudo {shield}");
            if (shield < 0)
            {
                health += shield; // Restar el exceso al health
                shield = 0;
            }
        }
        else
        {
            Debug.Log($"Health {health}");
            health -= damage;
        }

        if (health <= 0)
        {
            // Terminar el juego
            Debug.Log("El juego ha terminado");
            // ...implementación para finalizar el juego...
        }
    }
}
