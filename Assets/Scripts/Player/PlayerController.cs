using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int roadWidth;
    private bool canMove;
    private PlayerAnimator playerAnimator;

    [Header("Control")]
    [SerializeField] private float slidSpeed;
    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;
    private CrowdSystem crowdSystem;


    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        crowdSystem = GetComponent<CrowdSystem>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameStateChanged += GameStateChangeCallback;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallback;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveSpeedForward();
            ManageControl();
        }
    }

    private void GameStateChangeCallback(GameState gameState)
    {
        if (gameState == GameState.Game)
            StartMoving();
        else
            StopMoving();
    }

    private void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
    }

    private void MoveSpeedForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickScreenPosition = Input.mousePosition;
            clickPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slidSpeed;

            Vector3 position = transform.position;
            position.x = clickPlayerPosition.x + xScreenDifference;
            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2 + crowdSystem.GetCrowdRadius());

            transform.position = position;

            // transform.position = clickPlayerPosition + Vector3.right * xScreenDifference;
        }
    }
}
