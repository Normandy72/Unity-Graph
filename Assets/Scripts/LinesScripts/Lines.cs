using UnityEngine;

public class Lines : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 mousePos;
    public Material material;       // material for line renderer
    private int currentLine = 0;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))         // left mouse button is pressed and hold
        {
            if(line == null)
            {
                CreateLine();
            }

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            line.SetPosition(0, mousePos);
            line.SetPosition(1, mousePos);
        }
        else if(Input.GetMouseButtonUp(0) && line)       // left mouse button is released
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(1, mousePos);
            line = null;
            currentLine++;
        }
        else if(Input.GetMouseButton(0))        // left mouse button was pressed once
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(1, mousePos);
        }
    }

    private void CreateLine()
    {
        line = new GameObject("Line" + currentLine).AddComponent<LineRenderer>();
        line.material = material;
        line.positionCount = 2;
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        line.useWorldSpace = true;
        line.numCapVertices = 50;
    }
}
