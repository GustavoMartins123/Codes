using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    public Color patchColor = Color.green;
    Transform[] objArray;
    [Range(1, 20)] public int lineIntensity = 1;
    int overload;
    public List <Transform> patchObj= new List<Transform>();
    public List<Vector3> bezierObj = new List<Vector3>();

    public bool visualizePatch;
    private void Awake()
    {
        CreatePatch();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (visualizePatch)
        {
            Gizmos.color = patchColor;
            objArray = GetComponentsInChildren<Transform>();
            patchObj.Clear();
            foreach (Transform obj in objArray)
            {
                if (obj != this.transform)
                {
                    patchObj.Add(obj);
                }
            }
            for (int i = 0; i < patchObj.Count; i++)
            {
                Vector3 position = patchObj[i].position;
                if (i > 0)
                {
                    Vector3 previous = patchObj[i - 1].position;
                    Gizmos.DrawLine(previous, position);
                    Gizmos.DrawSphere(position, 0.3f);
                }
            }

            if (patchObj.Count % 2 == 0)
            {
                patchObj.Add(patchObj[patchObj.Count - 1]);
                overload = 2;
            }
            else
            {
                patchObj.Add(patchObj[patchObj.Count - 1]);
                patchObj.Add(patchObj[patchObj.Count - 1]);
                overload = 3;
            }

            //CurveCreating
            bezierObj.Clear();

            Vector3 lineStart = patchObj[0].position;
            for (int i = 0; i < patchObj.Count - overload; i += 2)
            {
                for (int j = 0; j <= lineIntensity; j++)
                {
                    Vector3 lineEnd = GetPoint(patchObj[i].position, patchObj[i + 1].position, patchObj[i + 2].position, j / (float)lineIntensity);

                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(lineStart, lineEnd);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(lineStart, 0.1f);
                    lineStart = lineEnd;
                    bezierObj.Add(lineStart);
                }
            }
        }
        else 
        {
            patchObj.Clear();
            bezierObj.Clear();
        }
    }
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) 
    {
        return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
    }

    void CreatePatch() 
    {
        objArray = GetComponentsInChildren<Transform>();
        patchObj.Clear();
        foreach (Transform obj in objArray)
        {
            if (obj != this.transform)
            {
                patchObj.Add(obj);
            }
        }

        if (patchObj.Count % 2 == 0)
        {
            patchObj.Add(patchObj[patchObj.Count - 1]);
            overload = 2;
        }
        else
        {
            patchObj.Add(patchObj[patchObj.Count - 1]);
            patchObj.Add(patchObj[patchObj.Count - 1]);
            overload = 3;
        }

        //CurveCreating
        bezierObj.Clear();

        Vector3 lineStart = patchObj[0].position;
        for (int i = 0; i < patchObj.Count - overload; i += 2)
        {
            for (int j = 0; j <= lineIntensity; j++)
            {
                Vector3 lineEnd = GetPoint(patchObj[i].position, patchObj[i + 1].position, patchObj[i + 2].position, j / (float)lineIntensity);

                lineStart = lineEnd;
                bezierObj.Add(lineStart);
            }
        }
    }
}
