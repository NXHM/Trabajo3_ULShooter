using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform m_FirePoint;
    [SerializeField]
    private float m_FireRange = 10f;
    [SerializeField]
    private CharacterController m_CharacterController;
    [SerializeField]
    private float m_Speed = 5.0f;
    [SerializeField]
    private float m_JumpHeight = 2.0f; 
    [SerializeField]
    private GameObject m_FireSphere;

    private Vector3 m_PlayerVelocity = Vector3.zero;

    private bool m_IsGrounded;

    private bool m_OnJump = false;

    //private PlayerHealth playerHealth; // Nueva referencia
    public int shield = 100;
    public int health = 100;
    public int maxShield = 100;
    public float rechargeDelay = 5f;
    public int rechargeAmount = 10;
    public float rechargeInterval = 1f;
    private bool isRecharging = false;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        //playerHealth = GetComponent<PlayerHealth>(); // Obtener el componente PlayerHealth
    }

    public void Move(Vector2 movement)
    {
        m_IsGrounded = m_CharacterController.isGrounded;

        if (m_IsGrounded && m_PlayerVelocity.y < 0)
        {
            m_PlayerVelocity.y = 0f;

        }

        // Calcular el movimiento plano XZ
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = move * m_Speed;

        // Calculamos la nueva velocidad en Y
        if (m_OnJump &&  m_IsGrounded)
        {
            m_PlayerVelocity.y += Mathf.Sqrt(
                -2f * Physics.gravity.y * m_JumpHeight
            );
            m_OnJump = false;
        }

        // Muevo el objeto en referencia al forward de Camara
        move = Camera.main.transform.forward * move.z + 
            Camera.main.transform.right * move.x;
        move.y = 0f;

        // Rotacion
        var angles = Quaternion.RotateTowards(
            transform.rotation,
            Camera.main.transform.rotation,
            300f * Time.deltaTime
        ).eulerAngles;
        angles.x = 0f; // bloqueamos la rotacion
        angles.z = 0f; // bloqueamos la rotacion

        transform.rotation = Quaternion.Euler(angles);

        m_PlayerVelocity.y += Physics.gravity.y * Time.deltaTime;
        move = new Vector3(
            move.x * Time.deltaTime,
            m_PlayerVelocity.y * Time.deltaTime,
            move.z * Time.deltaTime
        );

        m_CharacterController.Move(move);
    }

    public void Jump()
    {
        m_OnJump = true;
    }

    public void Fire()
    {
        // 1. Lanzar un raycast
        RaycastHit hit;
        var collision = Physics.Raycast(
            m_FirePoint.position,
            Camera.main.transform.forward,
            out hit,
            m_FireRange
        );
        if (collision)
        {
            // 2. Donde colisione el raycast, ejecutar un sistema de particulas
            Instantiate(m_FireSphere, hit.point, Quaternion.identity);
        }

        
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

        // Iniciar la recarga si no está ya en progreso
        if (!isRecharging)
        {
            StartCoroutine(RechargeShield());
        }
    }

    public void TakeDamage(int amount)
    {
        ReceiveDamage(amount);
    }

    private IEnumerator RechargeShield()
    {
        isRecharging = true;
        Debug.Log("Esperando para comenzar a recargar el escudo...");

        // Esperar el tiempo de retraso antes de comenzar a recargar
        yield return new WaitForSeconds(rechargeDelay);

        while (shield < maxShield)
        {
            shield += rechargeAmount;
            if (shield > maxShield)
            {
                shield = maxShield; // Asegurarse de que el escudo no exceda el máximo
            }

            Debug.Log($"Escudo recargado: {shield}");
            yield return new WaitForSeconds(rechargeInterval);
        }

        isRecharging = false;
        Debug.Log("Recarga de escudo completa");
        Debug.Log($"Escudo completo: {shield}");
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Colisión detectada con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colisión con un enemigo detectada. Recibiendo daño...");

            TakeDamage(10); 
            Debug.Log("Daño recibido: 10 puntos");
        }
    }

}
