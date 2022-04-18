using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private ControllManager CMScript;//Controller Manager Script
    public bool isPlayerLeft;

    public Vector2 WalkDirection;
    void Start()
    {
        CMScript = GameObject.Find("Manager").GetComponent<ControllManager>();

        if (!CMScript.hasPlayer_LeftSide)
        {
            CMScript.hasPlayer_LeftSide = true;
            isPlayerLeft = true;
        }
        else
        {
            isPlayerLeft = false;
        }

    }

    public void walk(InputAction.CallbackContext walkContext)
    {
        WalkDirection = walkContext.ReadValue<Vector2>();
        //Debug.Log(WalkDirection.ToString());

        if (isPlayerLeft)
        {
            CMScript.PlayerOneDirection = WalkDirection;
        }
        else
        {
            CMScript.PlayerTwoDirection = WalkDirection;
        }
    }

    public void FootStep(InputAction.CallbackContext FootContext)
    {
        //Debug.Log("FootStep");
        if (FootContext.performed)
        {
            if (isPlayerLeft)
            {
                //Debug.Log("Left Feet");
                CMScript.MoveLeftFeet();
            }
            else
            {
                // Debug.Log("Right Feet");
                CMScript.MoveRightFeet();
            }
        }
    }

    public void PickUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CMScript.PickUpTool();
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CMScript.Interact(isPlayerLeft);
        }
        else if (context.canceled)
        {
            CMScript.Interact_Canceled(isPlayerLeft);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
