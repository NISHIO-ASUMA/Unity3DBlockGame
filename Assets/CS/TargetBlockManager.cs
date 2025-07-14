using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//*************************************
// �^�[�Q�b�g�u���b�N�\��
//*************************************
public class TargetBlockManager : MonoBehaviour
{
    //***********************:
    // �g�p�����o�ϐ�
    //***********************:
    public TextMeshProUGUI Target;     // TextMeshPro �\���p
    public GameObject BreakBlock;      // �j��\�u���b�N�̎擾

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {// ����������\���������炷

        // �^�O�ŃI�u�W�F�N�g�������ĕ\��
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("BreakBlock");

        // �����J�E���g
        int count = blocks.Length;

        // �e�L�X�g�\��
        Target.text = "Target : " + count.ToString();

    }
}
