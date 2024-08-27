using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParent;

    [Header("Setting")]
    [SerializeField] private float radius;
    [SerializeField] private float angel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlaceRunner();
    }

    private void PlaceRunner()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = PlayerRunnerLocalPositions(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 PlayerRunnerLocalPositions(int index)
    {
        float mathfDeg2Rad = Mathf.Deg2Rad * index * angel;
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(mathfDeg2Rad);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(mathfDeg2Rad);
        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }
}
