using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private AudioSource m_AudioSource;

    private Vector3 m_Movement;
    private Quaternion m_Rotation = Quaternion.identity;

    [SerializeField] [Range(0,20)] private float turnSpeed;

    private Vector3 target;

    private RaycastHit hit;

    [Range(0, 20)] public float speed;

    public bool isMoving;

    public bool isMouse;

    public int index;
    
    private void Initialization()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetControls(index);
    }

    public void SetControls(int i)
    {
        index = i;

        switch (i)
        {
            case 0:
                Movement();
                Rotation();
                isMouse = false;
                break;
            case 1:
                MoveWithMouse();
                isMouse = true;
                break;
        }
    }

    private void OnAnimatorMove()
    {
        if (!isMouse)
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }

    private void MoveWithMouse()
    {
        isMoving = Input.GetKey(KeyCode.Mouse0);

        m_Animator.SetBool("isWalking", isMoving);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);
            if (hit.transform != null)
            {
                target = hit.point;
                
            }
        }

        Quaternion look = Quaternion.LookRotation(target - transform.position);
        look.x = 0;
        look.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, look, 10 * Time.deltaTime);


        if ((target - transform.position).magnitude >= 1.5f)
        {
            m_Rigidbody.velocity = transform.forward * speed;
            
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero;
        }

        if (isMoving)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
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

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    private void Rotation()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

}
