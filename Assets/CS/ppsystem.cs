using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ppsystem : MonoBehaviour
{
    public Transform target;          // �J�������ǂ�������Ώ�
    public float distance = 5.0f;     // �v���C���[�Ƃ̋���
    public float xSpeed = 120.0f;     // ������]���x
    public float ySpeed = 80.0f;      // ������]���x

    public float yMinLimit = -20f;    // �㉺��]�̉���
    public float yMaxLimit = 80f;     // �㉺��]�̏��

    private float x = 0.0f;           // ���݂�X���p�x�i�����j
    private float y = 0.0f;           // ���݂�Y���p�x�i�����j

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // �J�[�\�����\���ɂ���i�K�v�ɉ����āj
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target)
        {
            // �}�E�X�ړ��擾
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // �����p�x����
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            // ��]��񂩂�J�����̈ʒu���v�Z
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
