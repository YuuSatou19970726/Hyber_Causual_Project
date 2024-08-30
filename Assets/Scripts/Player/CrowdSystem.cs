using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;
    private PlayerAnimator playerAnimator;

    [Header("Setting")]
    [SerializeField] private float radius;
    [SerializeField] private float angel;

    void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameState())
            return;

        PlaceRunner();

        if (runnersParent.childCount <= 0)
            GameManager.instance.SetGameState(GameState.GameOver);
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

    public void ApplyBonus(BonusType _bonusType, int _bonusAmount)
    {
        switch (_bonusType)
        {
            case BonusType.Addition:
                AddRunner(_bonusAmount);
                break;
            case BonusType.Difference:
                RemoveRunner(_bonusAmount);
                break;
            case BonusType.Product:
                int runnersToAdd = (runnersParent.childCount * _bonusAmount) - runnersParent.childCount;
                AddRunner(runnersToAdd);
                break;
            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount - (runnersParent.childCount / _bonusAmount);
                RemoveRunner(runnersToRemove);
                break;
        }
    }

    private void AddRunner(int _amount)
    {
        for (int i = 0; i < _amount; i++)
            Instantiate(runnerPrefab, runnersParent);

        playerAnimator.Run();
    }

    private void RemoveRunner(int _amount)
    {
        int runnerAmount = runnersParent.childCount;

        if (_amount > runnerAmount)
            _amount = runnerAmount;

        for (int i = runnerAmount - 1; i >= runnerAmount - _amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
