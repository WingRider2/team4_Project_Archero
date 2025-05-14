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

    private int bindingIndexToRebind = -1;


    public int FindBindingIndexByPart(int compositeIndex, string partName)
    {
        var bindings = currentAction.action.bindings;
        int compositeCount = -1;

        for (int i = 0; i < bindings.Count; i++)
        {
            if (bindings[i].isComposite)
            {
                compositeCount++;
            }

            if (bindings[i].isPartOfComposite &&
                compositeCount == compositeIndex &&
                bindings[i].name.CompareTo(partName)==0)
            {
                return i;
            }
        }

        return -1;
    }
    // 리바인딩 시작하는 함수. 각 버튼에 OnClick함수로 넣어줄 함수.
    public void StartRebinding(string partName)
    {
        bindingIndexToRebind = FindBindingIndexByPart(0, partName);
        if (bindingIndexToRebind == -1)
        {
            Debug.LogError($"'{partName}'에 해당하는 바인딩을 찾을 수 없습니다.");
            return;
        }
        
        currentAction.action.Disable();

        Button.gameObject.SetActive(true);

        rebindingOperation = currentAction.action.PerformInteractiveRebinding(bindingIndexToRebind)
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
        string displayString = currentAction.action.GetBindingDisplayString(bindingIndexToRebind, displayStringOptions);
        bindingDisplayNameText.text = displayString;

        rebindingOperation.Dispose();
        currentAction.action.actionMap?.Enable();
        currentAction.action.Enable();
    }

    public void ShowBindText()
    {
        // 바인딩된 입력 키를 버튼에 보여줄 함수
        rebindingOperation.Dispose();
        currentAction.action.Enable();
        Button.gameObject.SetActive(false);
    }
}
