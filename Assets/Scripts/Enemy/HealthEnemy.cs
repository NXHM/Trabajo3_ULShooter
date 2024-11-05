using System.Collections;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField]
    private int m_Health = 3; // Vida inicial del enemigo

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
        

        Destroy(gameObject);
    }
}
