using System;
using UnityEngine;

[Serializable]
public class Block {
    [SerializeField]
    private GameObject first, second, third, fourth;

    public void Deactivate()
    {
        first.SetActive(false);
        second.SetActive(false);
        third.SetActive(false);
        fourth.SetActive(false);
    }

    public void Activate()
    {
        first.SetActive(true);
        second.SetActive(true);
        third.SetActive(true);
        fourth.SetActive(true);
    }
}
