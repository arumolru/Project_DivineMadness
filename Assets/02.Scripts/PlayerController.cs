using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))] // 오디오 소스를 요구합니다.

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 700f;
    public float stepHeight = 0.3f; // 계단 높이
    public float stepOffset = 0.2f; // 계단 오프셋
    public float slopeLimit = 45f; // 경사각 제한
    public float gravityMultiplier = 2f; // 중력 강도
    public AudioClip footstepSound; // 걷는 소리

    private CharacterController controller;
    private Camera cam;
    private float xRotation = 0f;

    private Vector3 previousPosition;
    private bool isGrounded = false;
    private AudioSource audioSource; // 오디오 소스 추가

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;

        // 계단 관련 설정
        controller.stepOffset = stepOffset;
        controller.slopeLimit = slopeLimit;

        previousPosition = transform.position;

        // 오디오 소스 초기화
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 이동 벡터 계산
        Vector3 moveDir = transform.right * x + transform.forward * z;
        moveDir.Normalize(); // 이동 벡터 정규화

        // 캐릭터 컨트롤러를 통한 이동 처리
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // 마우스 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 이동 방향에 따른 계단 처리
        Vector3 moveDelta = transform.position - previousPosition;
        RaycastHit hit;
        if (moveDelta.y < 0 && controller.isGrounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, stepHeight + 0.1f))
            {
                if (hit.collider.CompareTag("Stair"))
                {
                    float heightDifference = hit.point.y - transform.position.y;
                    controller.Move(Vector3.up * heightDifference);
                }
            }
        }
        else if (moveDelta.y > 0 && !isGrounded)
        {
            if (Physics.Raycast(transform.position, Vector3.up, out hit, stepHeight + 0.1f))
            {
                if (hit.collider.CompareTag("Stair"))
                {
                    float heightDifference = transform.position.y - hit.point.y;
                    controller.Move(Vector3.down * heightDifference);
                }
            }
        }

        isGrounded = controller.isGrounded;
        previousPosition = transform.position;

        // 중력 적용
        if (!isGrounded)
        {
            controller.Move(Physics.gravity * gravityMultiplier * Time.deltaTime);
        }

        // 플레이어 움직임 감지하여 걷는 소리 재생
        if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = footstepSound;
                audioSource.Play();
            }
        }
    }
}
