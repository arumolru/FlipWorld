using System;
using UnityEngine;
using UnityEngine.UI;

public class StageSave : MonoBehaviour
{
    [SerializeField]
    private Button[] stageButtons; // 스테이지 버튼

    public bool[] stageClear; // 스테이지 클리어 여부

    private void Start()
    {
        // 1스테이지를 제외한 나머지 스테이지 버튼 기능 비활성화
        for (int i = 1; i < stageButtons.Length; i++)
        {
            stageButtons[i].interactable = false;
        }
    }

    private void Update()
    {
        for (int i = 1; i < stageButtons.Length; i++)
        {
            if (stageClear[i])
            {
                stageButtons[i].interactable = true;
            }
        }
    }
}
