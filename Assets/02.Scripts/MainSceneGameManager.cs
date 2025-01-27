using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    // Start 버튼을 눌렀을 때
    public void StartButton()
    {
        panel.SetActive(true); // 패널 활성화
        StartCoroutine(FadeCoroutine()); // 코루틴 실행
    }

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
            levelSelect.SetActive(true); // 스테이지 선택 UI 활성화
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
}
