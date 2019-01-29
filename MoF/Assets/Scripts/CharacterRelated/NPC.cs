using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private Window window;

    [SerializeField]
    private string NPCName;

    public string MyNPCName
    {
        get
        {
            return NPCName;
        }
    }

    [SerializeField]
    private string NPCDialogue;

    public string MyNPCDialogue
    {
        get
        {
            return NPCDialogue;
        }
    }

    public bool IsInteracting { get; set; }

    public virtual void Interact()
    {
        window = FindObjectOfType<DialogueBox>();
        if (!IsInteracting)
        {
            IsInteracting = true;
            window.Open(this);
        }
    }

    public virtual void StopInteract()
    {
        if (IsInteracting)
        {
            IsInteracting = false;
            window.Close();
        }
    }
}
