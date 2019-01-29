using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {
    private static Bounds instance;

    public static Bounds MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Bounds>();
            }

            return instance;
        }
    }

    private BoxCollider2D bounds;
    private CameraFollow camFollow;

    [SerializeField]
    private EdgeCollider2D MovableBound;

    public EdgeCollider2D MyMovableBound
    {
        get
        {
            return MovableBound;
        }
    }

	// Use this for initialization
	void Start () {
        bounds = GetComponent<BoxCollider2D>();
        camFollow = FindObjectOfType<CameraFollow>();
        camFollow.setBounds(bounds);
	}
}
