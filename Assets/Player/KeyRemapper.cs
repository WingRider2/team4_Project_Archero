using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyRemapper : MonoBehaviour
{
    [SerializeField]
    private InputActionReference currentAction = null;
    [SerializeField]
    private TMP_Text bindingDisplayNameText = null;
    [SerializeField]
    private Button Button;
    [SerializeField]
    private InputBinding.DisplayStringOptions displayStringOptions;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    //내일 추가 수정
    public void StartRebinding()
    {
        // 리바인딩 시작하는 함수. 각 버튼에 OnClick함수로 넣어줄 함수.
        currentAction.action.Disable();

        Button.gameObject.SetActive(true);

        rebindingOperation = currentAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/rightButton")
            .WithCancelingThrough("<Mouse>/leftButton")
            .OnCancel(operation => RebindCancel())
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindCancel()
    {
        // 리바인딩이 취소됐을 때 실행될 함수
        Button.gameObject.SetActive(false);
        rebindingOperation.Dispose();
        currentAction.action.Enable();
        ShowBindText();
    }

    private void RebindComplete()
    {
        // 리바인딩이 완료됐을 때 실행될 함수
        var displayString = string.Empty;
        var deviceLayoutName = default(string);
        var controlPath = default(string);

        displayString = currentAction.action.GetBindingDisplayString(0, out deviceLayoutName, out controlPath, displayStringOptions);

        bindingDisplayNameText.text = displayString;
    }

    public void ShowBindText()
    {
        // 바인딩된 입력 키를 버튼에 보여줄 함수
        rebindingOperation.Dispose();
        currentAction.action.Enable();
        Button.gameObject.SetActive(false);
    }
}
