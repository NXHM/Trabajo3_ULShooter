using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Transform PlayerTransform;

    [SerializeField]
    private CharacterController m_CharacterController;
    [SerializeField]
    private float m_Speed = 5.0f;
    [SerializeField]
    private float m_JumpHeight = 2.0f;
    [SerializeField]
    private Transform m_FirePoint;
    [SerializeField]
    private WeaponSO m_WeaponData;

    private Vector3 m_PlayerVelocity = Vector3.zero;

    private bool m_IsGrounded;
    private bool m_OnJump = false;
    private float m_NextFireTime = 0f; // Tiempo en el que el jugador puede disparar nuevamente

    private PlayerHealth playerHealth; 


    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerHealth = GetComponent<PlayerHealth>();
        PlayerTransform = transform;
    }

    public void Move(Vector2 movement)
    {
        m_IsGrounded = m_CharacterController.isGrounded;

        if (m_IsGrounded && m_PlayerVelocity.y < 0)
        {
            m_PlayerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movement.x, 0, movement.y) * m_Speed;
        if (m_OnJump && m_IsGrounded)
        {
            m_PlayerVelocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * m_JumpHeight);
            m_OnJump = false;
        }

        move = Camera.main.transform.forward * move.z + Camera.main.transform.right * move.x;
        move.y = 0f;

        var angles = Quaternion.RotateTowards(
            transform.rotation,
            Camera.main.transform.rotation,
            300f * Time.deltaTime
        ).eulerAngles;
        angles.x = 0f;
        angles.z = 0f;

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
        if (Time.time >= m_NextFireTime)
        {
            RaycastHit hit;
            var collision = Physics.Raycast(
                m_FirePoint.position,
                Camera.main.transform.forward,
                out hit,
                m_WeaponData.FireRange
            );

            if (collision)
            {
                Debug.Log("Colisionó con: " + hit.collider.gameObject.name);
                Instantiate(m_WeaponData.FireSphere, hit.point, Quaternion.identity);
                if (hit.transform.TryGetComponent(out HealthEnemy m_HealthEnemy))
                {
                    Debug.Log("Mago sufrió daño");
                    m_HealthEnemy.TakeDamage(m_WeaponData.BulletDamage);
                }
            }

            // Establecer el siguiente tiempo de disparo
            m_NextFireTime = Time.time + m_WeaponData.FireRate;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colisi�n con un enemigo detectada. Recibiendo da�o...");
            playerHealth.ReceiveDamage(3); 
        }
    }

}
