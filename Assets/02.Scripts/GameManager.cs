using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageLevel; // 스테이지 레벨

    [SerializeField]
    private GameObject pause; // Pause UI

    private void Awake()
    {
        pause.SetActive(false); // Pause UI 비활성화
    }

    private void Update()
    {
        Pause(); // 게임 일시 정지
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0; // 게임 정지
            pause.SetActive(true); // Pause UI활성화
        }
    }

    public void Resume()
    {
        pause.SetActive(false); // 버튼을 누를경우 UI비활성화
        Time.timeScale = 1; // 게임 정지
    }

    public void Exit()
    {
        Debug.Log("게임 시작 화면으로 이동");
    }
}
