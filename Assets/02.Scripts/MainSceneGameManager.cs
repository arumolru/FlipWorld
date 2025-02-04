using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainSceneGameManager : MonoBehaviour
{
    [SerializeField]
    private Animator startAnim; // 스타트 버튼 애니메이터
    [SerializeField]
    private GameObject levelSelect; // 스테이지 선택 UI

    [SerializeField]
    private GameObject panel; // 패널 오브젝트
    [SerializeField]
    private Image panelImage; // 패널 색상 조절을 위한 변수

    [SerializeField]
    private Animator exitAnim; // 나가기 버튼 애니메이터

    private float fadeTime = 0; // 처음 알파값
    private int stageNumber; // 스테이지 값

    private bool isStageScene = false; // 스테이지 선택 씬 활성화 여부

    [SerializeField]
    private Animator[] selectButton; // 스테이지 버튼 애니메이션

    [SerializeField]
    private AudioSource buttonSounds; // 버튼 사운드

    // Start 버튼을 눌렀을 때
    public void StartButton()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        panel.SetActive(true); // 패널 활성화
        StartCoroutine(FadeCoroutine()); // 코루틴 실행
    }

    // 화면이 어두워지는 코루틴 실행
    IEnumerator FadeCoroutine()
    {
        while (fadeTime < 1.0f) // 알파값이 1이 될 때까지 반복
        {
            fadeTime += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            panelImage.color = new Color(0, 0, 0, fadeTime); // 해당 패널의 투명도 조절
        }
        if (fadeTime > 0.9f) // 배경이 완전히 어두워 졌을 때
        {
            if(!isStageScene) // LevelSelect 창이 활성화 되지 않을 경우
            {
                levelSelect.SetActive(true); // 스테이지 선택 UI 활성화
                isStageScene = true;
            }
            else
            {
                levelSelect.SetActive(false); //  LevelSelect 창이 활성화 됬을 경우
                isStageScene = false;
            }
            StartCoroutine(FadeInCoroutine()); // 화면이 밝아지는 코루틴 실행
        }
    }

    // 화면이 밝아지는 코루틴 실행
    IEnumerator FadeInCoroutine()
    {
        while (fadeTime > 0.0f) // 알파값이 0이 될 때까지 반복
        {
            fadeTime -= 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            panelImage.color = new Color(0, 0, 0, fadeTime); // 해당 패널의 투명도 조절
        }
        if (fadeTime < 0.1f) // 배경이 완전히 밝아졌을 때
        {
            panel.SetActive(false); // 패널 비활성화
        }
    }

    // Start 버튼에 마우스를 가져다 댔을 때
    public void StartMouseEnter()
    {
        startAnim.SetBool("IsMouse", true);
    }

    // Start버튼에 마우스를 땠을 때
    public void StartMouseExit()
    {
        startAnim.SetBool("IsMouse", false);
    }

    // Exit버튼을 눌렀을 때
    public void ExitButton()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        Debug.Log("게임 종료");
    }

    // Exit 버튼에 마우스를 가져다 댔을 때
    public void ExitMouseEnter()
    {
        exitAnim.SetBool("IsMouse", true);
    }

    // Exit버튼에 마우스를 땠을 때
    public void ExitMouseExit()
    {
        exitAnim.SetBool("IsMouse", false);
    }

    // Back버튼을 눌렀을 때
    public void Back()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        panel.SetActive(true); // 패널 활성화
        StartCoroutine(FadeCoroutine());
    }

    // 스테이지 버튼을 눌렀을 때
    public void Stage1()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 1;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage2()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 2;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage3()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 3;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage4()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 4;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage5()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 5;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage6()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 6;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage7()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 7;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage8()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 8;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage9()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 9;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage10()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 10;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage11()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 11;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage12()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 12;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage13()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 13;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage14()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 14;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage15()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 15;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage16()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 16;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage17()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 17;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage18()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 18;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage19()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 19;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage20()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 20;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage21()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 21;
        panel.SetActive(true); // 패널 활성화
    }

    public void Stage22()
    {
        buttonSounds.Play(); // 버튼 사운드 재생
        StartCoroutine(LoadCoroutine()); // 코루틴 실행
        stageNumber = 22;
        panel.SetActive(true); // 패널 활성화
    }

    // 마우스를 버튼 위에 가져다 댔을 경우
    public void Stage1ButtonEnter()
    {
        selectButton[0].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage2ButtonEnter()
    {
        selectButton[1].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage3ButtonEnter()
    {
        selectButton[2].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage4ButtonEnter()
    {
        selectButton[3].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage5ButtonEnter()
    {
        selectButton[4].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage6ButtonEnter()
    {
        selectButton[5].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage7ButtonEnter()
    {
        selectButton[6].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage8ButtonEnter()
    {
        selectButton[7].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage9ButtonEnter()
    {
        selectButton[8].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage10ButtonEnter()
    {
        selectButton[9].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage11ButtonEnter()
    {
        selectButton[10].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage12ButtonEnter()
    {
        selectButton[11].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage13ButtonEnter()
    {
        selectButton[12].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage14ButtonEnter()
    {
        selectButton[13].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage15ButtonEnter()
    {
        selectButton[14].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage16ButtonEnter()
    {
        selectButton[15].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage17ButtonEnter()
    {
        selectButton[16].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage18ButtonEnter()
    {
        selectButton[17].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage19ButtonEnter()
    {
        selectButton[18].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage20ButtonEnter()
    {
        selectButton[19].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage21ButtonEnter()
    {
        selectButton[20].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage22ButtonEnter()
    {
        selectButton[21].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void BackEnter()
    {
        selectButton[22].SetBool("IsMouse", true); // 애니메이션 실행
    }

    public void Stage1Exit()
    {
        selectButton[0].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage2Exit()
    {
        selectButton[1].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage3Exit()
    {
        selectButton[2].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage4Exit()
    {
        selectButton[3].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage5Exit()
    {
        selectButton[4].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage6Exit()
    {
        selectButton[5].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage7Exit()
    {
        selectButton[6].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage8Exit()
    {
        selectButton[7].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage9Exit()
    {
        selectButton[8].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage10Exit()
    {
        selectButton[9].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage11Exit()
    {
        selectButton[10].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage12Exit()
    {
        selectButton[11].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage13Exit()
    {
        selectButton[12].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage14Exit()
    {
        selectButton[13].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage15Exit()
    {
        selectButton[14].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage16Exit()
    {
        selectButton[15].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage17Exit()
    {
        selectButton[16].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage18Exit()
    {
        selectButton[17].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage19Exit()
    {
        selectButton[18].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage20Exit()
    {
        selectButton[19].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage21Exit()
    {
        selectButton[20].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void Stage22Exit()
    {
        selectButton[21].SetBool("IsMouse", false); // 애니메이션 실행
    }

    public void BackExit()
    {
        selectButton[22].SetBool("IsMouse", false); // 애니메이션 실행
    }

    IEnumerator LoadCoroutine()
    {
        float loadTime = 0; // 로딩 시간
        while (loadTime < 3.0f) // 알파값이 1이 될 때까지 반복
        {
            loadTime += 0.01f;

            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            panelImage.color = new Color(0, 0, 0, loadTime); // 해당 패널의 투명도 조절

            if (loadTime > 2.5f)
            {
                SceneManager.LoadScene("Stage" + stageNumber);
            }
        }
    }
}
