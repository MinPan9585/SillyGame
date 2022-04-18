using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerArea : MonoBehaviour
{
    // Start is called before the first frame update
    public ControllManager ManagerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CanPickUp" && !ManagerScript.canSeeObj.Contains(other.gameObject))
        {
            ManagerScript.canSeeObj.Add(other.gameObject);
        }
        if (other.tag == "CanInteract" && !ManagerScript.canInteractObj.Contains(other.gameObject))
        {
            //Debug.Log("see can interact");
            ManagerScript.canInteractObj.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CanPickUp" && ManagerScript.canSeeObj.Contains(other.gameObject))
        {
            ManagerScript.canSeeObj.Remove(other.gameObject);
        }
        if (other.tag == "CanInteract" && ManagerScript.canInteractObj.Contains(other.gameObject))
        {
            ManagerScript.canInteractObj.Remove(other.gameObject);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
