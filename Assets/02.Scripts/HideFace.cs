using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFace : MonoBehaviour
{
    // Start is called before the first frame update
    public SkinnedMeshRenderer faceRenderer; // ���� �����ϴ� �޽� ������


    void Start()
    {
        // �� �޽� ������ ��Ȱ��ȭ
        faceRenderer.enabled = false;
    }
}
