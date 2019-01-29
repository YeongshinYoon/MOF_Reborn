using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickButton : MonoBehaviour {
    [SerializeField]
    private Button MyButton;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OpenClose);
    }

    public void OpenClose()
    {
        UIManager.MyInstance.OpenClose(canvasGroup);
    }
}
