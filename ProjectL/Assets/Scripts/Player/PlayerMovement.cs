using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 FaceDirection { get => m_faceDirection; set => m_faceDirection = value; }
    public static PlayerMovement Instance { get => m_instance; set => m_instance = value; }

    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private Rigidbody2D m_rb;
    [SerializeField]
    private Vector2 m_faceDirection;

    private static PlayerMovement m_instance;

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if (inputX > 0)
        {
            FaceDirection = new Vector2(1, 0);
        }
        else if (inputX < 0)
        {
            FaceDirection = new Vector2(-1, 0);
        }
        else if (inputY > 0)
        {
            FaceDirection = new Vector2(0, 1);
        }
        else if (inputY < 0)
        {
            FaceDirection = new Vector2(0, -1);
        }
        Vector3 dir = new Vector3(inputX, inputY, 0);
        dir = dir * m_movementSpeed;

        m_rb.velocity = dir;
    }
}
