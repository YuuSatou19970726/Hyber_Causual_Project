using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elemnt")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject bubble;

    [Header("Setting")]
    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private float angel;

    // Start is called before the first frame update
    void Start()
    {
        EnemyGenerate();
        bubble.SetActive(true);
    }

    private void EnemyGenerate()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPosition = EnemyRunnerLocalPositions(i);
            Vector3 enemyWorldPosition = enemyParent.TransformPoint(enemyLocalPosition);

            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, enemyParent);
        }
    }

    private Vector3 EnemyRunnerLocalPositions(int index)
    {
        float mathfDeg2Rad = Mathf.Deg2Rad * index * angel;
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(mathfDeg2Rad);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(mathfDeg2Rad);
        return new Vector3(x, 0, z);
    }
}
