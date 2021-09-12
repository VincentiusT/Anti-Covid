using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardParticle : MonoBehaviour
{
    float speed;
    private RectTransform rectTransform;

    [SerializeField] private float pushDistance = 50f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void PlayParticle(Vector2 target)
    {
        StartCoroutine(PushToRandomDirection(target));
    }

    private IEnumerator PushToRandomDirection(Vector2 target)
    {
        float timer = 1f;
        float distance = Random.Range(0f, pushDistance);
        Vector2 randDirection = Random.insideUnitCircle.normalized;
        Vector2 pushEndPosition = transform.position + new Vector3(randDirection.x, randDirection.y) * distance;
        while (timer > 0f)
        {
            transform.position = Vector2.LerpUnclamped(transform.position, pushEndPosition, 5f * Time.deltaTime);

            timer -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveToTarget(target));
    }

    private IEnumerator MoveToTarget(Vector2 target)
    {
        while (true)
        {
            //rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, target, 1f);
            //transform.position = Vector2.MoveTowards(transform.position, target, 500f * Time.deltaTime);
            transform.position = Vector2.LerpUnclamped(transform.position, target, 5f * Time.deltaTime);

            if (Vector2.Distance(transform.position, target) < 50f)
            {
                transform.gameObject.SetActive(false);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
