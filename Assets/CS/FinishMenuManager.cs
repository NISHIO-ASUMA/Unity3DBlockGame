using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

//**********************************
//�Q�[���I�������j���[�̐���X�N���v�g
//**********************************
public class FinishMenuManager : MonoBehaviour
{
    //*************************
    // �g�p�����o�ϐ�
    //*************************
    public GameObject FinishMenuPanel; // �p�l���I�u�W�F�N�g
    public Button RetryButton;         // �{�^���I�u�W�F�N�g
    public Button QuitButton;          // �{�^���I�u�W�F�N�g
    public GameObject player;

    private TimeManager timeManager;    // �^�C�}�[�X�N���v�g�ϐ�
    private AllBlockManager blockManager; // �u���b�N�Ǘ��X�N���v�g�ϐ�

    private Button[] menuButtons;       // �{�^���̔z��
    private int currentIndex = 0;       // �I�����j���[�ԍ�
    private float inputCooldown = 0.2f; // ���͎�t�܂ł̎���
    private float inputTimer = 0f;      // �L�[���͎���
    private bool isKeyDown = false;     // �L�[�̉������
    private Vector3 playerStartPosition; // �v���C���[�̏����ʒu

    void Start()
    {
        // �p�l���A�{�^�����A�N�e�B�u��
        FinishMenuPanel.SetActive(false);
        RetryButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);

        // �X�N���v�g�擾
        timeManager = FindObjectOfType<TimeManager>();
        blockManager = FindObjectOfType<AllBlockManager>();

        // �{�^���C�x���g�o�^
        RetryButton.onClick.AddListener(OnRetry);
        QuitButton.onClick.AddListener(OnQuit);

        // �{�^���z��
        menuButtons = new Button[] { RetryButton, QuitButton };

        // �������W���Z�b�g
        playerStartPosition = player.transform.position;
    }

    void Update()
    {
        // ���g�p����Ȃ�������
        if (!FinishMenuPanel.activeSelf) return;

        // ���͎��Ԃ����Z
        inputTimer += Time.unscaledDeltaTime;

        // Enter�L�[�������ꂽ���ǂ����Ō��o
        if (!isKeyDown && !Input.GetKey(KeyCode.Return))
        {
            // ���͉\��ԂɂȂ�
            isKeyDown = true;
        }

        // �Q�[���p�b�h�̏c�������͂�L����
        float vertical = Input.GetAxisRaw("Vertical");

        // ���͉\���Ԃ��N�[���_�E���^�C�����傫���Ȃ�����
        if (Mathf.Abs(vertical) > 0.5f && inputTimer >= inputCooldown)
        {
            // ���j���[��I��
            if (vertical < 0)
                currentIndex = (currentIndex + 1) % menuButtons.Length;
            else
                currentIndex = (currentIndex - 1 + menuButtons.Length) % menuButtons.Length;

            // �����͈�ԏ�ɐݒ�
            EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);

            // ���͎�t��������
            inputTimer = 0f;
        }

        // ��x�������͂��󂯕t����
        if (Input.GetKeyDown(KeyCode.Return) && isKeyDown)
        {
            // ���j���[�̏��������s
            menuButtons[currentIndex].onClick.Invoke();
        }
    }

    //==============================
    // ���j���[�\���֐�
    //==============================
    public void ShowFinishMenu()
    {
        // ���Ԃ����Z�b�g
        Time.timeScale = 0f;

        // �L�[�t���O�𖢎g�p�ɂ���
        isKeyDown = false;

        // �p�l���A�{�^�����A�N�e�B�u��
        FinishMenuPanel.SetActive(true);
        RetryButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);

        // �C���f�b�N�X,���͎�t���Ԃ̏�����
        currentIndex = 0;
        inputTimer = 0f;
    }

    //==============================
    // ���g���C�֐�
    //==============================
    void OnRetry()
    {
        // �J�ڎ��Ԃ��Z�b�g
        Time.timeScale = 1f;

        // ���g���C�p�̏���
        blockManager.RetryStage();
        timeManager.ResetTimer();

        // �p�l���A�{�^�����A�N�e�B�u��
        FinishMenuPanel.SetActive(false);
        RetryButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);

        // �v���C���[�������ʒu�ɖ߂�
        if (player != null)
        {
            // �������W����
            player.transform.position = playerStartPosition;
        }
    }

    //==============================
    // �Q�[���I���֐�
    //==============================
    void OnQuit()
    {
        // �A�v���P�[�V�����I��
        Application.Quit();
#if UNITY_EDITOR
        // EDITOR�I��
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
