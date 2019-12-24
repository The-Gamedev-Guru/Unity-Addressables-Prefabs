using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneSequencePlayer : MonoBehaviour
{
    public Button tanks, soldiers;
    
    IEnumerator Start()
    {
        const float time = 3;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        yield return new WaitForSeconds(time);
        PressButton(tanks);
        yield return new WaitForSeconds(time);
        PressButton(soldiers);
        yield return new WaitForSeconds(time);
        PressButton(soldiers);
        yield return new WaitForSeconds(time);
        PressButton(tanks);
    }

    private void PressButton(Button button)
    {
        ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);

    }
}
