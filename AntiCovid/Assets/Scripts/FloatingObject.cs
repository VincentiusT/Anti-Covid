using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    Vector2 originalPosition;
    [SerializeField] float floatingSpeed, amplitudoY;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        transform.position = originalPosition + new Vector2(0, amplitudoY) * Mathf.Sin(floatingSpeed * Time.time);
    }
}
