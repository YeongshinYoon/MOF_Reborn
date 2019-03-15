using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedGame : MonoBehaviour 
{
    [SerializeField]
    private GameObject visuals;

    [SerializeField]
    private GameObject borderline;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Text classText;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private int index;

    public int MyIndex
    {
        get
        {
            return index;
        }
    }

    private void Awake()
    {
        borderline.SetActive(false);
        visuals.SetActive(false);
    }

    public void ShowInfo(SaveData data)
    {
        visuals.SetActive(true);
        if (data.MyPlayerData != null)
        {
            levelText.text = "Lv." + data.MyPlayerData.MyLevel;
            classText.text = data.MyPlayerData.MyClass;
            nameText.text = data.MyPlayerData.MyName;
        }
    }

    public void Selected()
    {
        borderline.SetActive(true);
    }

    public void Unselected()
    {
        borderline.SetActive(false);
    }

    public void HideInfo()
    {
        visuals.SetActive(false);
    }
}
