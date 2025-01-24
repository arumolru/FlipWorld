using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageLevel; // 스테이지 레벨

    [SerializeField]
    private GameObject pause; // Pause UI
    [SerializeField]
    private Animator resume; // Resume 버튼

    public bool isPause; // 게임 정지 여부

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
            isPause = true; // 정지 여부 활성화
            pause.SetActive(true); // Pause UI활성화
        }
    }

    public void Resume()
    {
        isPause = false; // 정지 여부 비활성화
        pause.SetActive(false); // 버튼을 누를경우 UI비활성화
    }

    public void Exit()
    {
        Debug.Log("게임 시작 화면으로 이동");
    }

    public void OnMouseEnter()
    {
        resume.SetBool("IsMouse", true);
    }
}
