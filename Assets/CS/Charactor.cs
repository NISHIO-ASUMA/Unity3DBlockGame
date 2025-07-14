using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    void Start()
    {
        if (!cameraTransform)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // ���͎擾
        float h = Input.GetAxis("Horizontal"); // A/D or ���X�e�B�b�N���E
        float v = Input.GetAxis("Vertical");   // W/S or ���X�e�B�b�N�㉺

        Vector3 inputDir = new Vector3(h, 0, v);
        if (inputDir.magnitude > 0.1f)
        {
            // �J�����̌����ɍ��킹�Ĉړ������𒲐�
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = camForward * v + camRight * h;

            // ���ۂɈړ�
            transform.position += move * moveSpeed * Time.deltaTime;

            // �������ړ������ɍ��킹��
            transform.rotation = Quaternion.LookRotation(move);
        }
    }
}
