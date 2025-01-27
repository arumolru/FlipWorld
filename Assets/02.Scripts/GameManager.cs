using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageLevel; // 스테이지 레벨

    [SerializeField]
    private GameObject pause; // Pause UI
    [SerializeField]
    private Animator resume; // Resume 버튼
    [SerializeField]
    private Animator exit; // Exit 버튼

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


    // 마우스 포인터를 버튼에 가져다 댔을 때 이벤트 실행
    // Resume 버튼에 마우스를 가져다 댔을 때
    public void ResumeEnter()
    {
        resume.SetBool("IsMouse", true);
    }

    // Resume 버튼에 마우스를 뺐을 때
    public void ResumeExit()
    {
        resume.SetBool("IsMouse", false);
    }

    // Exit 버튼에 마우스를 댔을 때
    public void ExitEnter()
    {
        exit.SetBool("IsMouse", true);
    }

    // Exit 버튼에 마우스를 뺐을 때
    public void ExitExit()
    {
        exit.SetBool("IsMouse", false);
    }
}
