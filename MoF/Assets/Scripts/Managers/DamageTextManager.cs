using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DMGTEXTTYPE { ATTACK, DAMAGED, HPHEAL, MANAHEAL }

public class DamageTextManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    //Variables
    private static DamageTextManager instance;

    public static DamageTextManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DamageTextManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private GameObject imagePrefab;

    [SerializeField]
    private Sprite[] attackNumberImages;

    [SerializeField]
    private Sprite[] damagedNumberImages;

    [SerializeField]
    private Sprite[] HPHealNumberImages;

    [SerializeField]
    private Sprite[] ManaHealNumberImages;

    [SerializeField]
    private Sprite criticalImage;

    [SerializeField]
    private Sprite criticalBGImage;

    [SerializeField]
    private Sprite missImage;

    [SerializeField]
    private RectTransform canvasTransform;

    private void Start()
    {

    }

    //Functions
    public void CreateText(Vector2 position, int dmg, DMGTEXTTYPE type, bool crit)
    {
        position.y += 0.7f;

        switch (type)
        {
            case DMGTEXTTYPE.ATTACK:
                imagePrefab.GetComponent<Image>().sprite = attackNumberImages[dmg];
                break;
            case DMGTEXTTYPE.DAMAGED:
                imagePrefab.GetComponent<Image>().sprite = damagedNumberImages[dmg];
                break;
            case DMGTEXTTYPE.HPHEAL:
                imagePrefab.GetComponent<Image>().sprite = HPHealNumberImages[dmg];
                break;
            case DMGTEXTTYPE.MANAHEAL:
                imagePrefab.GetComponent<Image>().sprite = ManaHealNumberImages[dmg];
                break;
        }

        Image dmgImage = Instantiate(imagePrefab, transform).GetComponent<Image>();
        dmgImage.transform.position = position;

        /*o.transform.SetParent(canvasTransform);
        o.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
        o.GetComponent<DamageText>().Initialize(speed, direction, fadeTime);
        o.GetComponent<Text>().text = "-" + text;
        if (critical)
        {
            o.GetComponent<Text>().color = criticalColor;
        }
        else 
        {
            o.GetComponent<Text>().color = normalColor;
        }*/
    }

    public int[] seperateNumber(int dmg)
    {
        int count = 0;
        int dmgtemp = dmg;

        while (dmgtemp != 0)
        {
            dmgtemp /= 10;

            count++;
        }

        int[] ret = new int[count];
        dmgtemp = dmg;

        for (int i = 0; i < count; i++)
        {
            for (int j = i; j < count - 1; j++)
            {
                dmgtemp /= 10;
            }

            ret[i] = dmgtemp;
            dmgtemp = dmg - (int)(dmgtemp * Mathf.Pow(10, count - i - 1));
        }

        return ret;
    }
}
