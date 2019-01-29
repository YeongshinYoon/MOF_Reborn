using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void KillConfirmed(Enemy enemy);

public class GameManager : MonoBehaviour {
    public event KillConfirmed killConfirmEvent;

    private static GameManager instance;

    public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Player player;

    private Enemy currentTarget;

    [SerializeField]
    private Enemy[] monsters;

    public Enemy[] MyMonsters
    {
        get
        {
            return monsters;
        }
    }

    private GameObject[] mobs;
    private Transform[] poss;
    private float[] dists;
    private float[,] sorteddists;
    private int curindex = 0;

    // Update is called once per frame
    void Update () 
    {
        TabTarget();

        ClickTarget();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.StopInteract();
        }
	}

    private void FloatInsertionSort(float[] list, int size)
    {
        sorteddists = new float[size,2];
        int i, j;
        float temp;

        for (i = 0; i < size; i++)
        {
            sorteddists[i, 0] = list[i];
        }

        for (i = 1; i < size; i++)
        {
            temp = sorteddists[i, 0];
            for (j = i - 1; j >= 0; j--)
            {
                if (sorteddists[j, 0] < temp) break;
                sorteddists[j + 1, 0] = sorteddists[j, 0];
                sorteddists[j + 1, 1] = j;
            }
            sorteddists[j + 1, 0] = temp;
            sorteddists[j + 1, 1] = i;
        }
    }

    public void calculatedLists()
    {
        curindex = 0;

        mobs = GameObject.FindGameObjectsWithTag("Enemy");
        poss = new Transform[mobs.Length];
        dists = new float[mobs.Length];

        for (int i = 0; i < mobs.Length; i++)
        {
            poss[i] = mobs[i].GetComponent<Transform>();
            dists[i] = Mathf.Sqrt(Mathf.Pow((poss[i].position.x - player.transform.position.x), 2) + Mathf.Pow((poss[i].position.y - player.transform.position.y), 2));
        }

        FloatInsertionSort(dists, dists.Length);
    }

    private void TabTarget()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (curindex == 0) calculatedLists();

            if (curindex >= mobs.Length) curindex = 0;

            if (mobs.Length > 0)
            {
                if (currentTarget != null) currentTarget.DeSelect();

                currentTarget = mobs[(int)sorteddists[curindex++, 1]].GetComponent<Enemy>();
                while (currentTarget.MyHealth.MyCurrentValue <= 0)
                {
                    currentTarget = mobs[(int)sorteddists[curindex++, 1]].GetComponent<Enemy>();
                }

                player.MyTarget = currentTarget.Select();

                UIManager.MyInstance.showTargetFrame(currentTarget);
            }
            else
            {
                UIManager.MyInstance.hideTargetFrame();

                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = null;
                player.MyTarget = null;
            }
        }
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);
            
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Interactable")
                {
                    player.Interact();
                }
                else 
                {
                    if (currentTarget != null) currentTarget.DeSelect();

                    currentTarget = hit.collider.GetComponent<Enemy>();

                    player.MyTarget = currentTarget.Select();

                    UIManager.MyInstance.showTargetFrame(currentTarget);
                }
            }
            else
            {
                UIManager.MyInstance.hideTargetFrame();

                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = null;
                player.MyTarget = null;
            }
        }
        //else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

        //    if (hit.collider != null && hit.collider.tag == "Enemy")
        //    {
        //        hit.collider.GetComponent<NPC>().Interact();
        //    }
        //}
    }

    public void OnKillConfirmed(Enemy enemy)
    {
        if (killConfirmEvent != null)
        {
            killConfirmEvent(enemy);
        }
    }
}
