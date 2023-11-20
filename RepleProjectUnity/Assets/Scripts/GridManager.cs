using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject quadPrefab;
    public int gridSize = 3;
    public float spacing = 1.1f;

    void Start()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject quad = Instantiate(quadPrefab, new Vector3(x * spacing, y * spacing, 0), Quaternion.identity, transform);
                QuadController qc = quad.GetComponent<QuadController>();
                if (qc)
                {
                    qc.originalPosition = quad.transform.position;
                }
                quad.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            }
        }
    }
}