using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : Window 
{
    private static DialogueBox instance;

    public static DialogueBox MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueBox>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Text NPCName;

    [SerializeField]
    private Text dialogue;

    [SerializeField]
    private GameObject[] ButtonSets;

    public NPC currentNPC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Open(NPC npc)
    {
        base.Open(npc);
        currentNPC = npc;
        NPCName.text = npc.MyNPCName;
        dialogue.text = npc.MyNPCDialogue;
        SelectButton(npc.MyNPCName);
    }

    public void SelectButton(string name)
    {
        foreach (GameObject btnset in ButtonSets)
        {
            btnset.SetActive(false);
        }

        foreach (GameObject btnset in ButtonSets)
        {
            if (btnset.name.Equals(name))
            {
                btnset.SetActive(true);
                break;
            }
        }
    }
}
