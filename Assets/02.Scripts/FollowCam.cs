using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform BlueSuitFree01; // �÷��̾� ĳ������ Transform ������Ʈ
    public Vector3 cameraOffset = new Vector3(0f, 0.1f, 0f);  // ī�޶� ��ġ ������

    void Update()
    {
        // ī�޶� ��ġ�� �÷��̾��� �Ӹ� �Ǵ� �� ��ġ�� ����
        transform.position = BlueSuitFree01.position + cameraOffset;


        // �÷��̾��� ȸ�� ���� ī�޶� ����
        transform.rotation = BlueSuitFree01.rotation;
    }
}

