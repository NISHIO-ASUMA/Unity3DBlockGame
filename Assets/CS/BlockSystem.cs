using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************************
// �u���b�N�I�u�W�F�N�g����
//***********************************
public class BlockSystem : MonoBehaviour
{
    public int BreakBlockLife = 2;      // ����u���b�N�̗̑�

    void Start()
    {
       // ����
    }

    void Update()
    { 
        // ����
    }

    // �_���[�W�����֐�
    public void ReceiveDamage(int damage)
    {
        // �̗͂����炷
        BreakBlockLife -= damage;

        // 0�ȉ��̎�
        if (BreakBlockLife <= 0)
        {
            Destroy(gameObject); // ������j��
        }
    }
}
