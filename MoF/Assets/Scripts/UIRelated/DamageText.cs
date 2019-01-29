using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour 
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Image damage;

    [SerializeField]
    private float fadeTime;

    private void Start()
    {
        StartCoroutine(Fadeout());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private IEnumerator Fadeout()
    {
        float startAlpha = damage.color.a;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Color tmpColor = damage.color;

            tmpColor.a = Mathf.Lerp(startAlpha, 0, progress);

            damage.color = tmpColor;

            progress += rate * Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
