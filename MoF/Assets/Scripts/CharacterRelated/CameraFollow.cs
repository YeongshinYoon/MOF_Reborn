using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private float xMax, xMin, yMin, yMax;

    private BoxCollider2D bound;

    private Vector3 minBound, maxBound;

    private float height, width;

    private void Start()
    {
        Debug.Log(name);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), -10);
    }

    private void SetLimits(Vector3 min, Vector3 max)
    {
        Camera cam = Camera.main;

        height = 2f * cam.orthographicSize;
        width = height * Screen.width / Screen.height;

        Debug.Log(width + ", " + height);
        Debug.Log(Screen.width + ", " + Screen.height);
        Debug.Log(cam.orthographicSize);

        xMin = min.x + width / 2;
        xMax = max.x - width / 2;

        yMin = min.y + height / 2;
        yMax = max.y - height / 2;
    }

    public void setBounds(BoxCollider2D newBounds)
    {
        bound = newBounds;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        SetLimits(minBound, maxBound);
    }

    /*private GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;
    private static bool cameraExists;

    private BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    // Use this for initialization
    void Start()
    {
        followTarget = FindObjectOfType<Player>().gameObject;

        //if (!cameraExists)
        //{
        //    cameraExists = true;
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        //minBounds = boundBox.bounds.min;
        //maxBounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (boundBox == null)
        {
            boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
        }

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }*/
}