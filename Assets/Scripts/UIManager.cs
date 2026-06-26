using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 1. 관리할 UI 패널들을 등록할 변수
    // Inspector 창에 이 변수들 보고 임의로 수정가능
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject pausePanel;
    public GameObject IngamePanel;

    // 게임이 처음 시작될 때 메인 메뉴만 켜고 나머지는 끈다.
    void Start()
    {
        // 처음 상태: 메인 메뉴만 켜기
        OpenMainMenu();
    }

    // 2. 버튼들이 호출할 public 함수들

    // 모든 UI 끄기 (기능 정리용)
    private void CloseAllPanels()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
        IngamePanel.SetActive(false);
    }

    // 메인 메뉴 켜기 함수 (설정 창의 '닫기' 버튼이 호출)
    public void OpenMainMenu()
    {
        CloseAllPanels();
        mainMenuPanel.SetActive(true);
    }

    // 설정 메뉴 켜기 함수 (메인 메뉴의 '설정' 버튼이 호출)
    public void OpenSettings()
    {
        CloseAllPanels();
        settingsPanel.SetActive(true);
    }

    // 일시정지 메뉴 켜" 함수 (이건 나중에 Esc키 등으로 호출)
    public void OpenPauseMenu()
    {
        CloseAllPanels();
        pausePanel.SetActive(true);
    }

    //인게임 시작
    public void OpneIngame()
    {
        CloseAllPanels();
        IngamePanel.SetActive(true);
    }

    //겜 종료
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
}