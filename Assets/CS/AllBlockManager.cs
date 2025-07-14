using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**************************************
// �u���b�N�S�̂��Ǘ�����X�N���v�g
//**************************************
public class AllBlockManager : MonoBehaviour
{
    //*************************
    // �g�p�����o�ϐ�
    //*************************
    [System.Serializable]
    
    // �X�e�[�W�ɃZ�b�g������
    public class StageBlockSet
    {
        public string stageName;
        public List<GameObject> blockPrefabs;
        public List<Vector3> spawnPositions;
    }

    [Header("�X�e�[�W���X�g�i�C���f�b�N�X���j")]
    public List<StageBlockSet> stages = new List<StageBlockSet>();
    private List<GameObject> spawnedBlocks = new List<GameObject>();
    private int currentStageIndex = 0;      // �X�e�[�W�̃C���f�b�N�X�ԍ�

    private int remainingBlocks = 0; // �c��u���b�N��
    private ClearMenuManager clearMenuManager;�@ // ���j���[�\��
    private FinishMenuManager finishMenuManager;

    void Start()
    {
        clearMenuManager = FindObjectOfType<ClearMenuManager>(); //�}�l�[�W���[�擾
        finishMenuManager = FindObjectOfType<FinishMenuManager>(); // �擾

        LoadStage(currentStageIndex); // �����X�e�[�W��ǂݍ���
    }

    /// <summary>
    /// �C���f�b�N�X�ŃX�e�[�W�ǂݍ���
    /// </summary>
    public void LoadStage(int index)
    {
        ClearStage();

        if (index < 0 || index >= stages.Count)
        {
            Debug.LogWarning("�X�e�[�W�C���f�b�N�X���͈͊O�ł��B");
            return;
        }

        StageBlockSet selectedStage = stages[index];

        for (int i = 0; i < selectedStage.blockPrefabs.Count; i++)
        {
            Vector3 pos = selectedStage.spawnPositions[i];
            GameObject block = Instantiate(selectedStage.blockPrefabs[i], pos, Quaternion.identity);
            spawnedBlocks.Add(block);
        }

        // BreakBlock�^�O�t���������J�E���g
        remainingBlocks = GameObject.FindGameObjectsWithTag("BreakBlock").Length;

        Debug.Log($"�X�e�[�W[{index}]�F{selectedStage.stageName} ��ǂݍ��݂܂����B�c��u���b�N: {remainingBlocks}");

    }

    // �j��ʒm���󂯎��֐�
    public void OnBlockDestroyed(GameObject block)
    {
        if (block.CompareTag("BreakBlock"))
        {
            // �����炷
            remainingBlocks--;

            Debug.Log("�j�󂳂ꂽ BreakBlock�F�c�� " + remainingBlocks);

            if (remainingBlocks <= 0)
            {
                Debug.Log("�X�e�[�W�N���A�I");

                // �Ō�̃X�e�[�W���ǂ����`�F�b�N
                if (currentStageIndex +1  >= stages.Count)
                {
                    Debug.Log("�Ō�̃X�e�[�W���N���A���܂����BFinishMenu��\�����܂��B");

                    // �擾�ł��Ă�����
                    if (finishMenuManager != null)
                    {
                        // �I�����j���[�\��
                        finishMenuManager.ShowFinishMenu();
                    }
                }
                else
                {
                    // �ʏ탁�j���[�\��
                    clearMenuManager.ShowClearMenu();
                }
            }
        }
    }

    /// <summary>
    /// ���̃X�e�[�W�ɐi��
    /// </summary>
    public void LoadNextStage()
    {
        currentStageIndex++;

        if (currentStageIndex >= stages.Count)
        {
            Debug.Log("�S�X�e�[�W���N���A���܂����I");

            if (finishMenuManager != null)
            {
                finishMenuManager.ShowFinishMenu(); // �I�����j���[��\��
            }

            return;
        }

        LoadStage(currentStageIndex);
    }

    /// <summary>
    /// ��������̃X�e�[�W��ǂݍ���
    /// </summary>
    public void RetryStage()
    {
        LoadStage(0);
    }

    /// <summary>
    /// �X�e�[�W�̃u���b�N�����ׂč폜
    /// </summary>
    private void ClearStage()
    {
        foreach (var block in spawnedBlocks)
        {
            if (block != null)
                Destroy(block);
        }
        spawnedBlocks.Clear();
    }

    /// <summary>
    /// ���݂̃X�e�[�W�ԍ����擾
    /// </summary>
    public int GetCurrentStageIndex()
    {
        return currentStageIndex;
    }
}
