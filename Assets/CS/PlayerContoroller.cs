using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***************************************
// �v���C���[����X�N���v�g
//***************************************
public class PlayerContoroller : MonoBehaviour
{
    //***************************
    // �g�p�����o�ϐ�
    //***************************
    private float playermove = 5.0f;
    public float jumpPower = 5f;
    public float gravity = 9.8f;
    public float AttackRange = 3.0f;
    private float verticalSpeed = 0f;
    private bool isGround = true;

    void Start()
    {
        // �i�j�J���擾����
    }

    void Update()
    {
        Debug.Log($"Y={transform.position.y}, verticalSpeed={verticalSpeed}, isGround={isGround}");

        // �L�[���͂ňړ��X�V
        if (Input.GetKey(KeyCode.A))
        {// A�L�[
            transform.position -= transform.right * playermove * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {// D�L�[
            transform.position += transform.right * playermove * Time.deltaTime;
        }

        // �Q�[���p�b�h�ňړ�
        float horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            transform.position += transform.right * horizontal * playermove * Time.deltaTime;
        }

        // �W�����v����
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") )&& isGround)
        {
            verticalSpeed = jumpPower;

            isGround = false;
        }

        // �U������
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire2"))
        {
            // �u���b�N�U��
            AttackNearbyBlock();
        }

        // �d�͓K�p��
        if (!isGround)
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        // Y���ړ��K�p
        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }

    // �ڒn����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("BreakBlock"))
        {
            isGround = true;
            verticalSpeed = 0f;
        }
    }

    // �ڒn����
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("BreakBlock"))
        {
            isGround = true;
            verticalSpeed = 0f;
        }
    }

    // �ڒn����
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("BreakBlock"))
        {
            isGround = false;
        }
    }

    // �����Q�ƃu���b�N�U��
    void AttackNearbyBlock()
    {
#if true
        Breaksystem[] allBlocks = FindObjectsOfType<Breaksystem>();

        foreach (Breaksystem block in allBlocks)
        {
            // �����v�Z
            float distance = Vector3.Distance(transform.position, block.transform.position);

            // �͈͓��ɓ�������
            if (distance < AttackRange)
            {
                if (block.CompareTag("BreakBlock")) // �󂹂�u���b�N���^�O�Ŕ���
                {
                    block.ReceiveDamage(1); // 1�_���[�W�^����
                    break; // 1�����U�����ďI��
                }
            }
        }
#endif
    }
}
