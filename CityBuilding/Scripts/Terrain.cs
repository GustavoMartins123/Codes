using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    Mesh mesh;

    public Vector3[] points;
    int[] triangles;
    public int xBlocks = 30;
    public int zblocks = 30;
    public float y;

    [Range(0.1f, 10.0f)]
    public float xOffSet = 0.5f;
    [Range(0.1f, 10.0f)]
    public float zOffSet = 0.5f;
    [Range(0.1f, 10.0f)]
    public float yOffSet = 1.5f;
    int p = 0;
    public int numberOfTress;
    public int numberOfRocks;
    public GameObject tree, tree2, rock,midRock, bigRock, flower, grass;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        TerrainGeometry();
        UpdateMesh();
        GenerateTree();
        GenerateRock();
    }

    void TerrainGeometry() 
    {
        points = new Vector3[(xBlocks + 1) * (zblocks + 1)];
        int i = 0;
        for(int z= 0; z <= zblocks; z++) 
        {
            for(int x = 0; x<= xBlocks; x++) 
            {
                y = Mathf.PerlinNoise(x * xOffSet, z * zOffSet) * yOffSet;
                points[i] = new Vector3(x * 2, y, z * 2);
                i++;
            }
        }
        triangles = new int[xBlocks * zblocks * 6];
        int vertex = 0;
        int trianglesCount = 0;
        for (int z = 0; z < zblocks; z++)
        {
            for (int x = 0; x < xBlocks; x++)
            {
                triangles[0 + trianglesCount] = vertex;
                triangles[1 + trianglesCount] = vertex + xBlocks + 1;
                triangles[2 + trianglesCount] = vertex +1;
                triangles[3 + trianglesCount] = vertex +1;
                triangles[4 + trianglesCount] = vertex + xBlocks + 1;
                triangles[5 + trianglesCount] = vertex + xBlocks + 2;
                vertex++;
                trianglesCount += 6;
            }
            vertex++;
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = points;
        mesh.triangles = triangles;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }
    /*private void OnDrawGizmos()
    {
        if (points == null) return;

        for(int i = 0; i < points.Length; i++) 
        {
            Gizmos.DrawSphere(points[i], 0.5f);
        }
    }*/

    public Vector3 RandomPoints() 
    {
        return points[Random.Range(0, xBlocks * zblocks)];
    }
    public Vector3 GridPoint(Vector3 point) 
    {
        int xPoint = (int)Mathf.Floor(point.x);
        int zPoint = (int)Mathf.Floor(point.z);
        return new Vector3(xPoint, 1, zPoint);
    }
    public void GenerateTree() 
    {
        if(tree == null|| tree2 == null) 
        {
            return;
        }

        GameObject tmp;
        Vector3 spawnPoint;

        for(int e = 0; e < numberOfTress; e++) 
        {
            p = Random.Range(0, 3);
            if (p == 0) 
            {
                tmp = Instantiate(tree);
                spawnPoint = GridPoint(RandomPoints());
                tmp.transform.Rotate(new Vector3(0,Random.Range(-360, 360),0));
                tmp.transform.localScale = new Vector3(Random.Range(0.9f, 1.2f), Random.Range(1, 1.3f), Random.Range(0.9f, 1.2f));
                if (tmp.transform.localScale.y >= 1.2f)
                {
                    spawnPoint.y = 4.3f;
                }
                else if(tmp.transform.localScale.y >= 1.35f) 
                {
                    spawnPoint.y = 4.6f;
                }
                else
                {
                    spawnPoint.y = 4f;
                }
                tmp.transform.position = spawnPoint;
            }
            if (p ==1)
            {
                tmp = Instantiate(tree2);
                spawnPoint = GridPoint(RandomPoints());
                tmp.transform.Rotate(new Vector3(0, Random.Range(-360, 360), 0));
                tmp.transform.localScale = new Vector3(Random.Range(0.9f, 1.2f), Random.Range(1, 1.3f), Random.Range(0.9f, 1.2f));
                if (tmp.transform.localScale.y > 1.2f)
                {
                    spawnPoint.y = 4f;
                }
                else if (tmp.transform.localScale.y > 1.35f)
                {
                    spawnPoint.y = 4.5f;
                }
                else
                {
                    spawnPoint.y = 3.5f;
                }
                tmp.transform.position = spawnPoint;
            }
        }
    }
    public void GenerateRock()
    {
        if (rock == null || bigRock == null)
        {
            return;
        }

        GameObject tmp;
        Vector3 spawnPoint;

        for (int e = 0; e < numberOfRocks; e++)
        {
            p = Random.Range(0, 3);
            if (p == 0)
            {
                tmp = Instantiate(rock);
                spawnPoint = GridPoint(RandomPoints());
                spawnPoint.y = 1.5f;
                tmp.transform.Rotate(new Vector3(0, 0, Random.Range(-360, 360)));
                tmp.transform.localScale = new Vector3(Random.Range(50, 120), Random.Range(50, 120), Random.Range(50, 120));
                tmp.transform.position = spawnPoint;

            }
            if (p == 1)
            {
                tmp = Instantiate(bigRock);
                spawnPoint = GridPoint(RandomPoints());
                spawnPoint.y = 3f;
                tmp.transform.Rotate(new Vector3(0, 0, Random.Range(-360, 360)));
                tmp.transform.localScale = new Vector3(Random.Range(210, 320), Random.Range(210, 320), Random.Range(210, 320));
                tmp.transform.position = spawnPoint;
            }
            if (p == 2)
            {
                tmp = Instantiate(midRock);
                spawnPoint = GridPoint(RandomPoints());
                spawnPoint.y = 1.5f;
                tmp.transform.Rotate(new Vector3(0, 0, Random.Range(-360, 360)));
                tmp.transform.localScale = new Vector3(Random.Range(0.9f, 1.5f), Random.Range(0.9f, 1.5f), Random.Range(0.9f, 1.5f));
                tmp.transform.position = spawnPoint;
            }
        }
    }
}
