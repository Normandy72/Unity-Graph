using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;       // container for dots
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("DashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("DashTemplateY").GetComponent<RectTransform>();

        List<int> valueList = new List<int>{5, 18, 34, 89, 52, 15, 12, 65, 45, 54, 71, 16, 25, 35, 45};
        ShowGraph(valueList);

        //CreateCircle(new Vector2(200, 200));    // test CreateCircle function        
    }

    // function that creates circle
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));            // now it's empty
        gameObject.transform.SetParent(graphContainer, false);                      // set parent container
        gameObject.GetComponent<Image>().sprite = circleSprite;                     // set sprite for gameObject
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();     // get parameters of gameObject
        rectTransform.anchoredPosition = anchoredPosition;                          // anchoredPosition is position of the pivot of the RectTransform
        rectTransform.sizeDelta = new Vector2(11, 11);                              // sizeDelta is the size of this RectTransform relative to the distances between the anchors
        rectTransform.anchorMin = new Vector2(0, 0);                                // anchor to lower left corner
        rectTransform.anchorMax = new Vector2(0, 0);                                // anchor to upper right corner

        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;                          // max value on y axe
        float xSize = 50f;                              // distance between each point on x axe

        GameObject lastCircleGameObject = null;         // reference to last circle

        for(int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));                          // create circle on new position
            
            if(lastCircleGameObject != null)        // not first circle
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleGameObject = circleGameObject;

            // instantiate labels for x axe
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -12f);
            labelX.GetComponent<TextMeshProUGUI>().text = i.ToString();

            // instantiate dashes for x axe
            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -1f);            
        }

        int separatorCount = 10;
        for(int i = 0; i <= separatorCount; i++)
        {
            // instantiate labels for y axe
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;        // normalized value between 0 and 1
            labelY.anchoredPosition = new Vector2(-12f, normalizedValue * graphHeight);
            labelY.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();

            // instantiate dashes for y axe
            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-1f, normalizedValue * graphHeight); 
        }
    }

    // function that creates lines between dots
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));             // empty gameObject for rectangle (line)
        gameObject.transform.SetParent(graphContainer, false);                              // set parent container
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);                  // set color
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();             // get parameters of gameObject
        Vector2 dir = (dotPositionB - dotPositionA).normalized;                             // direction of line
        float distance = Vector2.Distance(dotPositionA, dotPositionB);                      // distance between two dots
        rectTransform.anchorMin = new Vector2(0, 0);                                        // anchor to lower left corner
        rectTransform.anchorMax = new Vector2(0, 0);                                        // anchor to upper right corner
        rectTransform.sizeDelta = new Vector2(distance, 3f);                                // gameObject size
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;              // pivot position = dotA position + dir * distance * 0.5f (we place line exectly between point A and point B)
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));   // convert dir to angle between 0 and 360
    }

    // function for connection line rotation
    private float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }
}
