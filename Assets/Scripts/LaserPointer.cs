using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{

    public Transform startPos;
    public LineRenderer laserLineRenderer;

    public int maxReflectionCount = 10;
    public float maxStepDistance = 200f;
    public float laserWidth = 1f;

    public int currentReflection = 0;


    private void Start()
    {

        laserLineRenderer.useWorldSpace = true;

        laserLineRenderer.positionCount = maxReflectionCount * 2;
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
        laserLineRenderer.startColor = Color.blue;
        laserLineRenderer.endColor = Color.blue;

    }

    private void Update()
    {

        currentReflection = 0;
        DrawPredictedReflectionPattern(startPos.position + startPos.forward, startPos.forward, maxReflectionCount);

    }

    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining == 0)
        {
            return;
        }

        bool stopped = false;

        Vector3 startingPosition = position;
        Vector3 endPosition;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Mirror")
        {
            //Debug.Log("Check1");
            direction = Vector3.Reflect(direction, hit.normal);
            endPosition = hit.point;
        } else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag != "Mirror")
        {
            //Debug.Log("Check2");
            endPosition = hit.point;
            stopped = true;
        }
        else
        {
            position += direction * maxStepDistance;
            endPosition = position;
        }
        

        //Debug.DrawLine(startingPosition, position);

        laserLineRenderer.SetPosition(currentReflection, startingPosition);
        laserLineRenderer.SetPosition(currentReflection + 1, endPosition);

        currentReflection += 2;

        if (!stopped)
            DrawPredictedReflectionPattern(endPosition, direction, reflectionsRemaining - 1);
        else
            DrawPredictedReflectionPattern(startingPosition, direction, reflectionsRemaining - 1);
    }

}
