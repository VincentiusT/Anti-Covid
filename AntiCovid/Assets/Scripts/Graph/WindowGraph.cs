using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform dashLineContainer;
    private RectTransform labelContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;

    private List<GameObject> dotPointList, connectorList;
    private List<RectTransform> labelYContainer;

    public float yMax = 100f;
    public float xGapSize = 5f;
    float graphHeight;
    List<int> valueList = new List<int>();
    List<string> formatList = new List<string>() { "", "K", "M", "B" };
    int seperatorYCount = 10;
    int seperatorXCount = 80;

    private void OnEnable()
    {
        Debug.Log("Update graph");
        UpdateGraph();
    }

    public WindowGraph()
    {
        for (int i = 0; i < seperatorXCount; i++)
        {
            valueList.Add(0);
        }
    }

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        dashLineContainer = graphContainer.Find("DashLineContainer").GetComponent<RectTransform>();
        labelContainer = graphContainer.Find("LabelContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("DashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("DashTemplateY").GetComponent<RectTransform>();

        dotPointList = new List<GameObject>();
        connectorList = new List<GameObject>();
        labelYContainer = new List<RectTransform>();

        xGapSize = (graphContainer.sizeDelta.x - (xGapSize * 2)) / seperatorXCount;
        graphHeight = graphContainer.sizeDelta.y;

        ShowGraph(valueList);
        UpdateGraph();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddGraphData(Random.Range(0, 100));
            //ShowGraph(valueList);
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject go = new GameObject("circle", typeof(Image));
        go.transform.SetParent(graphContainer, false);
        go.GetComponent<Image>().sprite = circleSprite;
        RectTransform goRectTransform = go.GetComponent<RectTransform>();
        //goRectTransform.anchoredPosition = anchoredPosition;
        goRectTransform.sizeDelta = new Vector2(2, 2);
        goRectTransform.anchorMin = Vector2.zero;
        goRectTransform.anchorMax = Vector2.zero;

        dotPointList.Add(go);
        return go;
    }

    private void ShowGraph(List<int> values)
    {
        //float graphHeight = graphContainer.sizeDelta.y;
        //float yMax = 100f;
        //float xGapSize = 50f;
        GameObject prevCircleGO = null;

        for (int i = 0; i < values.Count; i++)
        {
            float xPos = xGapSize + i * xGapSize;
            float yPos = (values[i] / yMax) * graphHeight;
            GameObject circleGO = CreateCircle(new Vector2(xPos, yPos));

            if(prevCircleGO != null)
            {
                CreatePointConnector(prevCircleGO.GetComponent<RectTransform>().anchoredPosition, circleGO.GetComponent<RectTransform>().anchoredPosition);
            }

            prevCircleGO = circleGO;


            //RectTransform labelX = Instantiate(labelTemplateX);
            //labelX.SetParent(labelContainer, false);
            //labelX.gameObject.SetActive(true);
            //labelX.anchoredPosition = new Vector2(xPos, -7f);
            //labelX.GetComponent<TextMeshProUGUI>().text = i.ToString();

            //RectTransform dashX = Instantiate(dashTemplateX);
            //dashX.SetParent(dashLineContainer, false);
            //dashX.gameObject.SetActive(true);
            //dashX.anchoredPosition = new Vector2(xPos, -7f);
        }

        for (int i = 0; i <= seperatorYCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(labelContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / seperatorYCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            //labelY.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(normalizedValue * yMax).ToString();
            labelYContainer.Add(labelY);

            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(dashLineContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
        }
    }

    private void CreatePointConnector(Vector2 pointA, Vector2 pointB)
    {
        GameObject go = new GameObject("pointConnector", typeof(Image));
        go.transform.SetParent(graphContainer, false);
        go.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform goRectTransform = go.GetComponent<RectTransform>();

        //Vector2 pointDirection = (pointB - pointA).normalized;
        //float distance = Vector2.Distance(pointA, pointB);

        //goRectTransform.sizeDelta = new Vector2(distance, 3f);
        goRectTransform.anchorMin = Vector2.zero;
        goRectTransform.anchorMax = Vector2.zero;

        //goRectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromDirection(pointDirection));
        //goRectTransform.anchoredPosition = pointA + pointDirection * distance / 2f;

        connectorList.Add(go);
    }

    private void UpdateGraph()
    {
        yMax = valueList.Max();

        for (int i = 0; i < dotPointList.Count; i++)
        {
            float xPos = xGapSize + i * xGapSize;
            float yPos = (valueList[i] / yMax) * graphHeight;
            dotPointList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

            if(i != 0)
            {
                Vector2 pointB = dotPointList[i].GetComponent<RectTransform>().anchoredPosition, pointA = dotPointList[i-1].GetComponent<RectTransform>().anchoredPosition;
                Vector2 pointDirection = (pointB - pointA).normalized;
                float distance = Vector2.Distance(pointA, pointB);
                connectorList[i - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(distance, 3f);
                connectorList[i - 1].GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, GetAngleFromDirection(pointDirection));
                connectorList[i - 1].GetComponent<RectTransform>().anchoredPosition = pointA + pointDirection * distance / 2f;
            }
        }

        for (int i = 0; i <= seperatorYCount; i++)
        {
            RectTransform labelY = labelYContainer[i];
            float normalizedValue = i * 1f / seperatorYCount;
            labelY.GetComponent<TextMeshProUGUI>().text = ConvertToNumberFormat(Mathf.RoundToInt(normalizedValue * yMax));
        }
    }

    public void AddGraphData(int value)
    {
        valueList.RemoveAt(0);
        valueList.Add(value);

        if(transform.gameObject.activeInHierarchy)
            UpdateGraph();
    }

    private float GetAngleFromDirection(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private string ConvertToNumberFormat(int value)
    {
        int num = 0;
        while (value >= 1000)
        {
            num++;
            value /= 1000;
        }

        return value.ToString() + formatList[num];
    }
}
