using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//*************************************
// �Q�[���^�C�}�[
//*************************************
public class TimeManager : MonoBehaviour
{
    //***********************:
    // �g�p�����o�ϐ�
    //***********************:
    public TextMeshProUGUI timerText;     // TextMeshPro �\���p
    private float gameTime = 20f;          // �^�C�}�[�������ԁi�J�E���g�_�E���̏ꍇ�j
    public bool countDown = true;         // �J�E���g�_�E�����J�E���g�A�b�v��

    private float currentTime;            // ���݂̃^�C�}�[�l
    private bool isRunning = true;        // �^�C�}�[�̓���t���O

    private FinishMenuManager finishMenu; // �I�����j���[

    // Start is called before the first frame update
    void Start()
    {
        // �^�C�}�[�Z�b�g
        currentTime = gameTime;

        // �e�L�X�g�X�V
        UpdateTimerText();

        // �I�u�W�F�N�g�擾
        finishMenu = FindObjectOfType<FinishMenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;

        if (countDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;

                // null�`�F�b�N
                if (finishMenu != null)
                {
                    // �I�����j���[�\��
                    finishMenu.ShowFinishMenu();
                }
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        // 1�b���ƂɌ��炵�Ă���
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"Time {minutes:00}:{seconds:00}";
    }

    // �^�C�}�[��~
    public void StopTimer()
    {
        isRunning = false;
    }

    // �^�C�}�[�X�^�[�g
    public void StartTimer()
    {
        isRunning = true;
    }

    // �^�C�}�[���Z�b�g
    public void ResetTimer()
    {
        currentTime = gameTime;
        isRunning = true;
    }
}
