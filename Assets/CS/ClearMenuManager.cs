using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//**********************************
// �X�e�[�W�N���A���̐���X�N���v�g
//**********************************
public class ClearMenuManager : MonoBehaviour
{
    //*************************
    // �g�p�����o�ϐ�
    //*************************
    public GameObject clearMenuPanel;   // �p�l��
    public Button nextButton;           // ���ɐi�ރ{�^��
    public Button quitButton;           // ���߂�{�^��

    private AllBlockManager blockManager; // �u���b�N�}�l�[�W���[�̕ϐ�
    private TimeManager timeManager; // �^�C�}�[�ϐ�

    private Button[] menuButtons;
    private int currentIndex = 0;
    private float inputCooldown = 0.2f;
    private float inputTimer = 0f;

    void Start()
    {
        clearMenuPanel.SetActive(false); // �p�l����\��
        nextButton.gameObject.SetActive(false); // �{�^����\��
        quitButton.gameObject.SetActive(false); // �{�^����\��

        // �}�l�[�W���[�擾
        blockManager = FindObjectOfType<AllBlockManager>();

        // �^�C���}�l�[�W���[�擾
        timeManager = FindObjectOfType<TimeManager>();

        // �l��ݒ�
        nextButton.onClick.AddListener(OnNextStage);
        quitButton.onClick.AddListener(OnQuitGame);

        // �z�񐶐�
        menuButtons = new Button[] { nextButton, quitButton };
    }

    // ���j���[�o��
    public void ShowClearMenu()
    {
        clearMenuPanel.SetActive(true);         // �p�l��
        nextButton.gameObject.SetActive(true);  // �{�^���\��
        quitButton.gameObject.SetActive(true);  // �{�^���\��
        Time.timeScale = 0f; // �|�[�Y

        currentIndex = 0;

       //  EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);
    }

    // ���̃X�e�[�W�ɐi��
    void OnNextStage()
    {
        // 1f�҂�
        Time.timeScale = 1f;

        clearMenuPanel.SetActive(false);        // �p�l����\��
        nextButton.gameObject.SetActive(false); // �{�^����\��
        quitButton.gameObject.SetActive(false); // �{�^����\��

        // ���̃X�e�[�W���[�h
        blockManager.LoadNextStage();
    }

    // �A�v���I��
    void OnQuitGame()
    {
        // �Q�[���I��
        Application.Quit();
#if UNITY_EDITOR

        // EDITOR���[�h�I��
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        // ��O
        if (!clearMenuPanel.activeSelf) return;

        // ���Z
        inputTimer += Time.unscaledDeltaTime;

        float vertical = Input.GetAxisRaw("Vertical"); // �p�b�h�I��

        if (Mathf.Abs(vertical) > 0.5f && inputTimer >= inputCooldown)
        {
            // �㉺�ɉ����ăC���f�b�N�X�ύX
            if (vertical < 0)
                currentIndex = (currentIndex + 1) % menuButtons.Length;
            else if (vertical > 0)
                currentIndex = (currentIndex - 1 + menuButtons.Length) % menuButtons.Length;

            // �I���X�V
            EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);

            // �����l�ݒ�
            inputTimer = 0f;
        }

        // ����
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
        {
            menuButtons[currentIndex].onClick.Invoke();
        }
    }
}
