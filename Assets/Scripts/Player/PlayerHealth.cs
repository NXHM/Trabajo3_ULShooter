using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int shield = 100;
    public int health = 100;
    public int maxShield = 100;
    public float rechargeDelay = 5f;
    public int rechargeAmount = 10;
    public float rechargeInterval = 1f;
    private bool isRecharging = false;

    // Agregar el evento OnPlayerDeath
    public event Action OnPlayerDeath;

    public void ReceiveDamage(int damage)
    {
        Debug.Log($"Daño recibido: {damage}");
        Debug.Log($"Shield: {shield}");
        Debug.Log($"Health: {health}");

        if (shield > 0)
        {
           
            shield -= damage;
            Debug.Log($"Escudo después de daño: {shield}");

            
            if (shield < 0)
            {
                int excessDamage = -shield; 
                shield = 0; 
                health -= excessDamage; 
                Debug.Log($"Daño excedente aplicado a la salud: {excessDamage}");
            }
        }
        else
        {
           
            health -= damage;
        }

        Debug.Log($"Salud después de daño: {health}");

        if (health <= 0)
        {
            // Notificar al GameController en lugar de manejar la lógica aquí
            if (OnPlayerDeath != null)
                OnPlayerDeath();
        }

     
        if (!isRecharging)
        {
            StartCoroutine(RechargeShield());
        }
    }

    private IEnumerator RechargeShield()
    {
        isRecharging = true;
        Debug.Log("Esperando para comenzar a recargar el escudo...");

        
        yield return new WaitForSeconds(rechargeDelay);

        while (shield < maxShield)
        {
            shield += rechargeAmount;
            if (shield > maxShield)
            {
                shield = maxShield; 
            }

            Debug.Log($"Escudo recargado: {shield}");
            yield return new WaitForSeconds(rechargeInterval);
        }

        isRecharging = false;
        Debug.Log("Recarga de escudo completa");
        Debug.Log($"Escudo completo: {shield}");
    }
}
