using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    // Start is called before the first frame update
    //private Rigidbody shpereRig;
    public int conOneID, conTwoID;
    public Vector2 PlayerXDirection, PlayerYDirection, PlayersFinalDirection2D;

    public float forceStrength;

    public void recordID(InputAction.CallbackContext context)
    {
        if (conOneID == 0)
        {
            conOneID = context.control.device.deviceId;
        }
        /*if (context.control.device.deviceId != conOneID)
        {
            conTwoID = context.control.device.deviceId;
        }*/
    }
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.performed)
        {
            Debug.Log("Jump-------");
            Debug.Log(context.control.device.deviceId);

            //shpereRig.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Debug.Log("walk");
        Debug.Log(context.ReadValue<Vector2>().ToString() + "  " + context.control.device.deviceId);
        PlayerXDirection = context.ReadValue<Vector2>();

        /* if (context.control.device.deviceId == conOneID)
         {

         }
         if (context.control.device.deviceId == conTwoID)
         {
             PlayerYDirection = context.ReadValue<Vector2>();
         }*/
        //PlayersFinalDirection2D = PlayerXDirection + PlayerYDirection;
        //Vector3 PlayersFinalDirection3D = new Vector3(PlayersFinalDirection2D.x, PlayersFinalDirection2D.y, 0);


        //shpereRig.AddForce(PlayersFinalDirection3D * forceStrength, ForceMode.Force);
    }


    void Start()
    {
        Debug.Log("Start");
        //shpereRig = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
