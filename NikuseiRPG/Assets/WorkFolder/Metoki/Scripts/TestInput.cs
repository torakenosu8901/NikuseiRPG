using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    public Canvas canvas;
   
    private void Start()
    {
        //startedは押した瞬間しか機能させない
        InputManager.instance.InputSystem.Player.Pause.started += PrintPause;
        //canseledは離した瞬間に機能する
        InputManager.instance.InputSystem.Player.Pause.canceled -= PrintPause;
    }

    private void Update()
    {
        //※ SetActiveより軽い
        //  ただし、enabledが使えない型もある。(GameObject型)
        // Canvas非表示
        //Text.txt;
        if(Input.GetKey(KeyCode.D))
        {
            canvas.enabled = false;
            //txt.enabled = false;
        }
        // Canvas表示
        if(Input.GetKey(KeyCode.E))
        {
            canvas.enabled = false;
        }
        //Junpに設定されたボタンを押されたとき
        if(InputManager.instance.InputSystem.Player.Junp.IsPressed() == true)
        {
            Debug.Log("Junp　!");
        }
    }

    private void PrintPause(InputAction.CallbackContext context)
    {
        Debug.Log("ポーズ画面表示");
    }
}
