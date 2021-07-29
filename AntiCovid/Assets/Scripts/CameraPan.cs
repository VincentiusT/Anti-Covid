using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPan : MonoBehaviour
{
    private Vector3 touchStart;
    private Camera cam;
    [SerializeField] SpriteRenderer background;

    private float BackgroundMinX, BackgroundMaxX, BackgroundMinY, BackgroundMaxY;

    private void Awake()
    {
        BackgroundMinX = background.transform.position.x - background.bounds.size.x / 2f;
        BackgroundMaxX = background.transform.position.x + background.bounds.size.x / 2f;
        BackgroundMinY = background.transform.position.y - background.bounds.size.y / 2f;
        BackgroundMaxY = background.transform.position.y + background.bounds.size.y / 2f;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (InputManager.instance.IsPointerOverUIElement()) return;

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position = ClampCamera(cam.transform.position + direction);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPostion)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = BackgroundMinX + camWidth;
        float maxX = BackgroundMaxX - camWidth;
        float minY = BackgroundMinY + camHeight;
        float maxY = BackgroundMaxY - camHeight;

        float newX = Mathf.Clamp(targetPostion.x, minX, maxX);
        float newY = Mathf.Clamp(targetPostion.y, minY, maxY);

        return new Vector3(newX, newY, -20f);
    }
}
