using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int shield = 100;
    public int health = 100;
    public int maxHealth = 100;
    public int maxShield = 100;
    public float rechargeDelay = 5f;
    public int rechargeAmount = 10;
    public float rechargeInterval = 1f;

    // Agregar el evento OnPlayerDeath
    public event Action OnPlayerDeath;

    [SerializeField] HUDController hudController;
    [SerializeField] BarController healthBar;
    [SerializeField] BarController shieldBar;
    private Coroutine rechargingShield;

    void Start()
    {
        health = maxHealth;
        healthBar.Setup(maxHealth, health);
        shieldBar.Setup(maxShield, shield);
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health -= 10;
            hudController.UpdateSprite();
            healthBar.SetValue(health);
        }
        */

        shieldBar.SetValue(shield);
    }

    public void ReceiveDamage(int damage)
    {
        Debug.Log($"Daño recibido: {damage}");
        //Debug.Log($"Shield: {shield}");
        //Debug.Log($"Health: {health}");

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

     
        if (rechargingShield == null)
        {
            rechargingShield = StartCoroutine(RechargeShield());
        }

        hudController.UpdateSprite();
        healthBar.SetValue(health);
    }

    private IEnumerator RechargeShield()
    {
        Debug.Log("Esperando para comenzar a recargar el escudo...");

        
        yield return new WaitForSeconds(rechargeDelay);

        while (shield < maxShield)
        {
            shield += rechargeAmount;
            //Debug.Log($"Escudo recargado: {shield}");
            yield return new WaitForSeconds(rechargeInterval);
        }
        shield = maxShield;
        rechargingShield = null;
        //Debug.Log("Recarga de escudo completa");
        //Debug.Log($"Escudo completo: {shield}");
    }
}
