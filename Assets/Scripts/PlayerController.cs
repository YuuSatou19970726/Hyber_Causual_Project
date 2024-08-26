using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [Header("Control")]
    [SerializeField] private float slidSpeed;
    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveSpeedForward();
        ManageControl();
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
            transform.position = position;

            // transform.position = clickPlayerPosition + Vector3.right * xScreenDifference;
        }
    }
}
