using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // ������Ʈ�� ĳ�� ó���� ����
    private Transform tr;
    // Animation ������Ʈ�� ������ ����
    private Animator animator;


    private bool wPressed = false;
    private bool sPressed = false;
    private bool aPressed = false;
    private bool dPressed = false;


    private Rigidbody rb;


    // ��������Ʈ ����
    public delegate void PlayerDieHandler();


    // �̵� �ӷ� ���� (public���� ����Ǿ� �ν����� �信 �����)
    public float moveSpeed = 10.0f;
    // ȸ�� �ӵ� ����
    public float turnSpeed = 80.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Transform ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        animator.Play("Idle");

        // WPressed �Ķ���͸� false�� �ʱ�ȭ
        animator.SetBool("WPressed", false);

        // SPressed �Ķ���͸� false�� �ʱ�ȭ
        animator.SetBool("SPressed", false);

        // APressed �Ķ���͸� false�� �ʱ�ȭ
        animator.SetBool("APressed", false);

        // DPressed �Ķ���͸� false�� �ʱ�ȭ
        animator.SetBool("DPressed", false);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        // Debug.Log("h=" + h);
        // Debug.Log("v=" + v);

        // �����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(�̵� ���� * �ӷ� * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        // Vector3.up ���� �������� turnSpeed��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);


        // ������ �̵�
        if(Input.GetKeyDown(KeyCode.W))
        {
            wPressed = true;
            animator.SetBool("WPressed", wPressed);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            wPressed = false;
            animator.SetBool("WPressed", wPressed);
        }

        // �ڷ� �̵�
        if(Input.GetKeyDown(KeyCode.S))
        {
            sPressed = true;
            animator.SetBool("SPressed", sPressed);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            sPressed = false;
            animator.SetBool("SPressed", sPressed);
        }

        // �������� �̵�
        if (Input.GetKeyDown(KeyCode.A))
        {
            aPressed = true;
            animator.SetBool("APressed", aPressed);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            aPressed = false;
            animator.SetBool("APressed", aPressed);
        }


        // ���������� �̵�
        if (Input.GetKeyDown(KeyCode.D))
        {
            dPressed = true;
            animator.SetBool("DPressed", dPressed);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            dPressed = false;
            animator.SetBool("DPressed", dPressed);
        }
    }
}
