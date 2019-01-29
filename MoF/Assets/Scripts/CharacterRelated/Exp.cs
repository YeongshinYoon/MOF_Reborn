using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour {
    private Image content;

    [SerializeField]
    private Text expValue;

    [SerializeField]
    private Text expPerValue;

    [SerializeField]
    private float lerpSpeed;

    private float currentFill;

    public float MyMaxValue { get; set; }

    public float MyCurrentValue { get; set; }

    // Use this for initialization
    void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFill = MyCurrentValue / MyMaxValue;
        content.fillAmount = currentFill;
        content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);

        expValue.text = MyCurrentValue + " / " + MyMaxValue;
        if (expPerValue != null)
            expPerValue.text = MyCurrentValue * 100 / MyMaxValue + "%";
    }

    public void Initialize(float cur, float max)
    {
        if (content == null)
        {
            content = GetComponent<Image>();
        }
        MyMaxValue = max;
        MyCurrentValue = cur;
        content.fillAmount = MyCurrentValue / MyMaxValue;
    }
}
