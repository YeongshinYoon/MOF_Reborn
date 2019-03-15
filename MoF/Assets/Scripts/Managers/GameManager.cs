using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void KillConfirmed(Enemy enemy);

public class GameManager : MonoBehaviour {
    public event KillConfirmed killConfirmEvent;

    private Camera mainCamera;

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
    //private int targetIndex;

    private void Start()
    {
        //mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update () 
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        TabTarget();

        ClickTarget();

        //NextTarget();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.StopInteract();
        }
	}

    private void DeSelectTarget()
    {
        if (currentTarget != null)
        {
            currentTarget.DeSelect();
        }
    }

    private void SelectTarget(Enemy enemy)
    {
        currentTarget = enemy;
        player.MyTarget = currentTarget.Select();
        UIManager.MyInstance.showTargetFrame(currentTarget);
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);
            
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Interactable")
                {
                    player.Interact();
                }
                else 
                {
                    DeSelectTarget();

                    SelectTarget(hit.collider.GetComponent<Enemy>());
                }
            }
            else
            {
                UIManager.MyInstance.hideTargetFrame();

                DeSelectTarget();

                currentTarget = null;
                player.MyTarget = null;
            }
        }
        else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null)
            {
                IInteractable entity = hit.collider.gameObject.GetComponent<IInteractable>();

                //hit.collider.GetComponent<NPC>().Interact();
                entity.Interact();
            }
        }
    }

    public void OnKillConfirmed(Enemy enemy)
    {
        if (killConfirmEvent != null)
        {
            killConfirmEvent(enemy);
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
        int count = 0;

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<Enemy>().MyHealth.MyCurrentValue > 0)
            {
                count++;
            }
        }

        mobs = new GameObject[count];

        count = 0;

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<Enemy>().MyHealth.MyCurrentValue > 0)
            {
                mobs[count++] = temp[i];
            }
        }

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
                DeSelectTarget();

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

                DeSelectTarget();

                currentTarget = null;
                player.MyTarget = null;
            }
        }
    }

    /*private void NextTarget()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < temp.Length; i++)
        {
            Player.MyInstance.AddAttacker(temp[i].GetComponent<Enemy>());
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DeSelectTarget();

            if (Player.MyInstance.MyAttackers.Count > 0)
            {
                SelectTarget(Player.MyInstance.MyAttackers[targetIndex]);
                targetIndex++;

                if (targetIndex >= Player.MyInstance.MyAttackers.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }*/
}
