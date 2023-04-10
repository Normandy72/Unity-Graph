using UnityEngine;
using System.Collections.Generic;

public class PenTool : MonoBehaviour
{
    [Header("Pen Canvas")]
    [SerializeField] private PenCanvas penCanvas;

    [Header("Dots")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;      // DotsCanvas

    [Header("Lines")]
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform linesParent;     // LineParent
    private LineController currentLine;

    private void Start()
    {
        penCanvas.OnPenCanvasLeftClickEvent += AddDot;
        penCanvas.OnPenCanvasRightClickEvent += EndCurrentLine;
    }

    private void AddDot()
    {
        if(currentLine == null)
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, linesParent).GetComponent<LineController>();
        }

        DotController dot = Instantiate(dotPrefab, GetMousePosition(), Quaternion.identity, dotsParent).GetComponent<DotController>();
        dot.OnDragEvent += MoveDot;
        dot.OnRightClickEvent += RemoveDot;
        dot.OnLeftClickEvent += SetCurrentLine;

        currentLine.AddPoint(dot);
    }

    private void EndCurrentLine()
    {
        if(currentLine != null)
        {
            currentLine = null;
        }
    }

    private void MoveDot(DotController dot)
    {
        dot.transform.position = GetMousePosition();
    }

    private void RemoveDot(DotController dot)
    {
        LineController line = dot.line;
        line.SplitPointsAtIndex(dot.index, out List<DotController> before, out List<DotController> after);

        Destroy(line.gameObject);
        Destroy(dot.gameObject);

        LineController beforeLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, linesParent).GetComponent<LineController>();

        for(int i = 0; i < before.Count; i++)
        {
            beforeLine.AddPoint(before[i]);
        }

        LineController afterLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, linesParent).GetComponent<LineController>();

        for(int i = 0; i < after.Count; i++)
        {
            afterLine.AddPoint(after[i]);
        }
    }

    private void SetCurrentLine(LineController newLine)
    {
        EndCurrentLine();
        currentLine = newLine;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = 0;

        return worldMousePosition;
    }

    // private void Update()
    // {
    //     if(Input.GetMouseButton(0))         // if left button pressed
    //     {
    //         if(currentLine == null)
    //         {
    //             currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, linesParent).GetComponent<LineController>();
    //         }

    //         GameObject dot = Instantiate(dotPrefab, GetMousePosition(), Quaternion.identity, dotsParent);

    //         currentLine.AddPoint(dot.transform);
    //     }
    // }
}
