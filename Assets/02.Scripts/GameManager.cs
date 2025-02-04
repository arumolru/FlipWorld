using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int stageLevel; // 스테이지 레벨

    [SerializeField]
    private GameObject pause; // Pause UI
    [SerializeField]
    private Animator resume; // Resume 버튼
    [SerializeField]
    private Animator exit; // Exit 버튼
    [SerializeField]
    private GameObject panel; // 패널 오브젝트
    [SerializeField]
    private Image panelImage; // 패널 이미지

    public bool isPause; // 게임 정지 여부

    [SerializeField]
    private AudioSource buttonSounds; // 버튼 사운드

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
        buttonSounds.Play(); // 사운드 재생
        isPause = false; // 정지 여부 비활성화
        pause.SetActive(false); // 버튼을 누를경우 UI비활성화
    }

    public void Exit()
    {
        buttonSounds.Play(); // 사운드 재생

        panel.SetActive(true); // 패널 활성화
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
    }

    IEnumerator LoadCoroutine()
    {
        float loadTime = 0; // 로딩 시간
        while (loadTime < 3.0f) // 알파값이 1이 될 때까지 반복
        {
            loadTime += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            panelImage.color = new Color(0, 0, 0, loadTime); // 해당 패널의 투명도 조절
        }

        if (loadTime > 2.5f)
        {
            SceneManager.LoadScene("MainScene"); // 메인 화면으로 이동
        }
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
