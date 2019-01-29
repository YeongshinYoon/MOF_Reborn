using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour 
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private NPC npc;

    public NPC MyNPC
    {
        get
        {
            return npc;
        }
    }

    public virtual void Open(NPC npc)
    {
        this.npc = npc;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        npc = null;
    }
}
