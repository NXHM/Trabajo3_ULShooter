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
        // Puedes inicializar variables o agregar lógica inicial aquí si es necesario
    }

    // Update is called once per frame
    void Update()
    {
        // Lógica de actualización, si es necesario
    }

    public void ReceiveDamage(int damage)
    {
        Debug.Log($"Daño recibido: {damage}");
        Debug.Log($"Shield: {shield}");
        Debug.Log($"Health: {health}");

        if (shield > 0)
        {
            // Restar daño al escudo
            shield -= damage;
            Debug.Log($"Escudo después de daño: {shield}");

            // Si el escudo se vuelve negativo, calculamos el daño excedente
            if (shield < 0)
            {
                int excessDamage = -shield; // Convertir el valor negativo en positivo
                shield = 0; // Restablecer el escudo a 0
                health -= excessDamage; // Aplicar el daño excedente a la salud
                Debug.Log($"Daño excedente aplicado a la salud: {excessDamage}");
            }
        }
        else
        {
            // Si no hay escudo, restar daño directamente de la salud
            health -= damage;
        }

        Debug.Log($"Salud después de daño: {health}");

        if (health <= 0)
        {
            // Terminar el juego
            Debug.Log("El juego ha terminado");
            // Aquí puedes agregar la implementación para finalizar el juego
        }
    }
}
