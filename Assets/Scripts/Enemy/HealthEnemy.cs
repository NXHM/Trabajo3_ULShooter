using System.Collections;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    private int m_Health; // Vida inicial del enemigo

    public void SetHealth(int health)
    {
        m_Health = health;
    }

    // Función pública para recibir daño
    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        Debug.Log("Enemigo recibió daño, salud actual: " + m_Health);

        if (m_Health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("El enemigo ha muerto.");
        

        Destroy(transform.parent.gameObject);
    }
}
