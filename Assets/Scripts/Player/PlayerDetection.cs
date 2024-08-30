using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    private CrowdSystem crowdSystem;

    void Awake()
    {
        crowdSystem = GetComponent<CrowdSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DectectDoors();
    }

    private void DectectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);
        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                Debug.Log("Hit the doors");
                int bonusAmounnt = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.DisableDoorCollider();
                crowdSystem.ApplyBonus(bonusType, bonusAmounnt);
            }
            else if (detectedColliders[i].CompareTag("Finish"))
            {
                Debug.Log("Hit finish line");
                PlayerPrefs.SetInt(PlayerPrefsTag.LEVEL, PlayerPrefs.GetInt(PlayerPrefsTag.LEVEL) + 1);
                SceneManager.LoadScene(0);
            }
        }
    }
}
