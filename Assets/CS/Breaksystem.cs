using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*****************************************
// �j��\�u���b�N�ɑ΂��鐧��X�N���v�g
//*****************************************
public class Breaksystem : MonoBehaviour
{
    //****************************************
    // �g�p�����o�ϐ�
    //****************************************
    public int BlockLife = 0;   // �̗�
    public GameObject BreakEffectPrefab;    // �G�t�F�N�g�v���n�u
    private AllBlockManager blockManager; // �ǉ�

    // Start is called before the first frame update
    void Start()
    {
        // �擾
        blockManager = FindObjectOfType<AllBlockManager>(); // �X�e�[�W�}�l�[�W���擾
    }

    // �_���[�W����
    public void ReceiveDamage(int damage)
    {
        // �̗͂����炷
        BlockLife -= damage;

        // 0�ȉ�
        if (BlockLife <= 0)
        {
            // �j��O�ɕ񍐁i�����Ɏ�����n���j
            if (blockManager != null)
            {
                blockManager.OnBlockDestroyed(gameObject);
            }

            // �j��
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    // �ԓ_��
    private IEnumerator FlashRed()
    {
        if (BreakEffectPrefab != null)
        {
            Vector3 effectPos = transform.position + Vector3.up * 0.5f;
            GameObject effect = Instantiate(BreakEffectPrefab, effectPos, Quaternion.identity);
            Destroy(effect, 0.5f); // �G�t�F�N�g�I��

            yield return new WaitForSeconds(0.1f); // �������Ԃ�u���ď����I��
        }
        else
        {
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
