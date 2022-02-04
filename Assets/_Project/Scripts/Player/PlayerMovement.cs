using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    private Vector3 m_Movement;
    private Quaternion m_Rotation = Quaternion.identity;

    [SerializeField] [Range(0,20)] private float turnSpeed;
    

    private void Initialization()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("isWalking", isWalking);
    }

    private void Rotation()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

    }

}
