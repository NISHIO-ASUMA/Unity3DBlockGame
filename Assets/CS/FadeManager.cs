using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI�g���̂Œǉ�

//*********************************
//// �t�F�[�h�V�X�e���X�N���v�g
//*********************************
public class FadeManager : MonoBehaviour
{
    // �t�F�[�h�̊Ǘ��t���O�ϐ�
    public static bool isFadeInstance = false;

    bool isFadeIn = false;      // �t�F�[�h�C������t���O
    bool isFadeOut = false;     // �t�F�[�h�A�E�g����t���O

    public float alpha = 0.0f;      // �t�F�[�h�̓��ߗ�
    public float fadeSpeed = 0.2f;  // �t�F�[�h�̃X�s�[�h

    private Image fadeImage; // Image�Q��

    // Start is called before the first frame update
    void Start()
    {
        // �N����
        if (!isFadeInstance)
        {// false�Ȃ�

            DontDestroyOnLoad(this);
            isFadeInstance = true;      // �t���O��L��������
        }
        else
        {// �N�����ȊO�͏d�����Ȃ��悤�ɂ���
            Destroy(this);
        }

        // �������g��Image���擾
        fadeImage = GetComponent<Image>();

        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeIn)
        {// �t�F�[�h���L��

            // ���ߗ����v�Z����
            alpha -= Time.deltaTime / fadeSpeed;

            if (alpha <= 0.0f)
            {// 0.0f��艺�������
                // �t�F�[�h�C���t���O��false�ɂ���
                isFadeIn = false;

                // �����x��0.0f�ɌŒ�
                alpha = 0.0f;
            }

            // �J���[�𒲐�
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
        {// �t�F�[�h�A�E�g���L��

            // ���ߗ����v�Z����
            alpha += Time.deltaTime / fadeSpeed;

            if (alpha >= 1.0f)
            {//1.0f����������
                // �t�F�[�h�C���t���O��false�ɂ���
                isFadeOut = false;

                // �����x��1.0f�ɌŒ�
                alpha = 1.0f;
            }

            // �J���[�𒲐�
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }

    // �t�F�[�h�C���p�̊֐�
    public void FadeIn()
    {
        isFadeIn = true;    // In��true
        isFadeOut = false;  // Out��false

        // �K���\�����Ă���J�n
        if (fadeImage != null)
            fadeImage.gameObject.SetActive(true);
    }

    // �t�F�[�h�A�E�g�p�̊֐�
    public void FadeOut()
    {
        isFadeIn = false;   // In��false
        isFadeOut = true;   // Out��true

        if (fadeImage != null && !fadeImage.gameObject.activeSelf)
            fadeImage.gameObject.SetActive(true);
    }

}
