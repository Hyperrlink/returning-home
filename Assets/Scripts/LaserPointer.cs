using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{

    public Transform startPos;
    public LineRenderer laserLineRenderer;

    public int maxReflectionCount = 30;
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
        }
        else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag != "Mirror" && hit.collider.gameObject.tag != "Sensor")
        {
            //Debug.Log("Check2");
            endPosition = hit.point;
            stopped = true;

            GameObject[] s = GameObject.FindGameObjectsWithTag("Sensor");
            if (s.Length > 0)
            {
                foreach (GameObject g in s)
                {

                    GameObject o = g.GetComponent<Sensor>().objToTrigger.gameObject;

                    if (o.name == "Door")
                    {

                        o.GetComponent<Door>().triggered = false;

                    } else if (o.name == "End Level")
                    {
                        o.GetComponent<EndLevel>().triggered = false;
                    }

                }
            }
            

        }
        else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Sensor")
        {
            Debug.Log("Sensor");
            endPosition = hit.point;
            stopped = true;

            GameObject o = hit.collider.gameObject.GetComponent<Sensor>().objToTrigger.gameObject;

            if (o.name == "Door")
            {

                o.GetComponent<Door>().triggered = true;

            }
            else if (o.name == "End Level")
            {
                o.GetComponent<EndLevel>().triggered = true;
            }

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
