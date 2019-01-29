using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour {
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float speed;

    public Transform MyTarget { get; private set; }

    private Transform source;

    private int damage;

    private float SpellNumber;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}

    public void Initialize(Transform target, int damage, float spellNumber, Transform source)
    {
        MyTarget = target;
        this.damage = damage;
        this.source = source;
        SpellNumber = spellNumber;
    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            Vector2 direction = MyTarget.position - transform.position;

            myRigidbody.velocity = direction.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox" && collision.transform == MyTarget)
        {
            Character c = collision.GetComponentInParent<Character>();
            speed = 0;
            c.TakeDamage(damage, source);
            GetComponent<Animator>().SetTrigger("impact");
            GetComponent<Animator>().SetFloat("SpellNumber", SpellNumber);
            myRigidbody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}
