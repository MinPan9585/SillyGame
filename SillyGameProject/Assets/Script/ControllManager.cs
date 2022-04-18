using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class ControllManager : MonoBehaviour
{
    // public int PlayerOneConID, PlayerTwoConID;
    public Vector2 PlayerOneDirection, PlayerTwoDirection, CharacterDir;
    public Vector3 CharacterDir3D;
    public float ForceStrength, MoveStepTime;
    public bool hasPlayer_LeftSide;
    //public string hasToolName;
    public GameObject hasToolObj, TrashBin_Mesh, RealInteractiveObj, RealToolObj;
    public Transform PutDownPlace;

    public List<GameObject> canSeeObj, canInteractObj;



    private GameObject Character;
    // Start is called before the first frame update
    private Rigidbody CharacterRgbd;
    private bool leftFeetDown, rightFeetDown, canMove, leftInteract, rightInteract;

    private float canMoveTime, PressTogetherTime;


    private void MoveCharacter()
    {
        CharacterDir = PlayerOneDirection + PlayerTwoDirection;
        CharacterDir3D = new Vector3(CharacterDir.x, 0, CharacterDir.y);
        if (canMove)
        {
            //Debug.Log("move");
            CharacterRgbd.AddForce(CharacterDir3D * ForceStrength, ForceMode.Force);
        }

        if (CharacterDir3D.magnitude != 0)
        {
            Vector3 LookAtPos = CharacterDir3D + Character.transform.position;
            Character.transform.LookAt(LookAtPos, Vector3.up);
        }
        //Character.transform.LookAt(Target.transform, Vector3.up);
    }
    private void Update_canMove()
    {
        if (Time.time >= canMoveTime && canMove == true)
        {
            canMove = false;
        }
    }

    public void MoveLeftFeet()
    {
        if (!leftFeetDown)
        {
            leftFeetDown = true;
            if (rightFeetDown)
            {
                canMove = true;
                canMoveTime = Time.time + MoveStepTime;
                rightFeetDown = false;
            }
        }
        else
        {
            leftFeetDown = false;
        }
    }

    public void MoveRightFeet()
    {
        if (!rightFeetDown)
        {
            rightFeetDown = true;
            if (leftFeetDown)
            {
                canMove = true;
                canMoveTime = Time.time + MoveStepTime;
                leftFeetDown = false;
            }
        }
        else
        {
            rightFeetDown = false;
        }
    }

    public void PickUpTool()
    {
        //if already have tool in hand ---> drop off
        if (hasToolObj != null)
        {
            Tool Tool_Script = hasToolObj.GetComponent<Tool>();

            if (Tool_Script.Name == "TrashBin")
            {
                TrashBin_Mesh.SetActive(false);

                //disable trash outline when put down trash bin
                if (RealInteractiveObj != null)
                {
                    RealInteractiveObj.GetComponent<Outlinable>().enabled = false;
                    RealInteractiveObj = null;
                }
            }

            hasToolObj.transform.position = PutDownPlace.position;
            hasToolObj.transform.rotation = PutDownPlace.rotation;
            hasToolObj.SetActive(true);
            //add drop off tool to can see obj list
            if (!canSeeObj.Contains(hasToolObj))
            {
                canSeeObj.Add(hasToolObj);
            }
            hasToolObj = null;
        }

        else if (canSeeObj.Count >= 1 && hasToolObj == null)
        {
            // GameObject realPickUpObj = PickRealObj(canSeeObj);

            // //pick up
            // Tool Tool_Script = realPickUpObj.GetComponent<Tool>();
            // //Tool_Script.Mesh.SetActive(false);
            // realPickUpObj.SetActive(false);
            RealToolObj.SetActive(false);

            if (RealToolObj.GetComponent<Tool>().Name == "TrashBin")
            {
                TrashBin_Mesh.SetActive(true);
            }

            hasToolObj = RealToolObj;
            canSeeObj.Remove(hasToolObj);
            //hasToolName = Tool_Script.Name;
        }
    }

    private GameObject PickRealObj(List<GameObject> canSeeObj)
    {
        GameObject realPickUpObj = canSeeObj[0];
        float DotRecord = 0;

        for (int i = 0; i < canSeeObj.Count - 1; i++)
        {
            Vector3 ObjDir = (canSeeObj[i].transform.position - Character.transform.position).normalized;
            Vector3 CharDir = CharacterDir3D.normalized;

            float Dot = Vector3.Dot(ObjDir, CharDir);
            if (i == 0)
            {
                DotRecord = Dot;
            }
            else if (Dot > DotRecord)
            {
                DotRecord = Dot;
                realPickUpObj = canSeeObj[i];
            }
        }

        return realPickUpObj;
    }

    private void SetInteract_Left()
    {
        leftInteract = false;
    }
    private void SetInteract_Right()
    {
        rightInteract = false;
    }

    public void Interact(bool isLeft)
    {
        if (isLeft)
        {
            leftInteract = true;
            //nvoke("SetInteract_Left", PressTogetherTime);
            Debug.Log("interact left");
        }
        else
        {
            rightInteract = true;
            //Invoke("SetInteract_Right", PressTogetherTime);
            Debug.Log("interact right");
        }

    }

    public void Interact_Canceled(bool isLeft)
    {
        if (leftInteract && rightInteract)
        {
            //Debug.Log("interact together");
            //if (hasToolName == "TrashBin")
            if (hasToolObj != null && hasToolObj.GetComponent<Tool>().Name == "TrashBin")
            {
                canInteractObj.Remove(RealInteractiveObj);
                hasToolObj.GetComponent<TrashBin>().PickupTrash(RealInteractiveObj);
                RealInteractiveObj = null;
            }
        }

        if (isLeft)
        {
            leftInteract = false;
        }
        else
        {
            rightInteract = false;
        }
    }

    private void FindRealInteractive()
    {
        if (canInteractObj.Count > 0)
        {
            if (hasToolObj != null)
            {
                //Trash
                if (hasToolObj.GetComponent<Tool>().Name == "TrashBin")
                {
                    List<GameObject> TrashList = new List<GameObject>();
                    for (int i = 0; i < canInteractObj.Count; i++)
                    {
                        if (canInteractObj[i].GetComponent<Interactive>().Name == "Trash"
                        && !TrashList.Contains(canInteractObj[i]))
                        {
                            TrashList.Add(canInteractObj[i]);
                        }
                    }

                    if (TrashList.Count > 0)
                    {
                        GameObject RealTrash = PickRealObj(TrashList);

                        if (RealInteractiveObj == null)
                        {
                            RealTrash.GetComponent<Outlinable>().enabled = true;
                            RealInteractiveObj = RealTrash;

                        }
                        if (RealTrash != RealInteractiveObj)
                        {
                            RealInteractiveObj.GetComponent<Outlinable>().enabled = false;
                            RealTrash.GetComponent<Outlinable>().enabled = true;
                            RealInteractiveObj = RealTrash;
                        }

                    }
                }

            }
        }
        else
        {
            if (RealInteractiveObj != null)
            {
                RealInteractiveObj.GetComponent<Outlinable>().enabled = false;
                RealInteractiveObj = null;
            }
        }

    }

    private void FindRealTool()
    {
        if (canSeeObj.Count > 0 && hasToolObj == null)
        {
            GameObject _realTool = PickRealObj(canSeeObj);

            if (RealToolObj == null)
            {
                RealToolObj = _realTool;
                RealToolObj.GetComponent<Outlinable>().enabled = true;
            }
            else if (RealToolObj != _realTool)
            {
                RealToolObj.GetComponent<Outlinable>().enabled = false;
                RealToolObj = _realTool;
                RealToolObj.GetComponent<Outlinable>().enabled = true;
            }
        }
        else
        {
            if (RealToolObj != null)
            {
                RealToolObj.GetComponent<Outlinable>().enabled = false;
                RealToolObj = null;
            }
        }
    }

    private void Awake()
    {
        Character = GameObject.Find("Character");
        CharacterRgbd = Character.GetComponent<Rigidbody>();
        hasPlayer_LeftSide = false;
    }
    void Start()
    {
        CharacterRgbd = Character.GetComponent<Rigidbody>();

    }

    private

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Update_canMove();
        FindRealInteractive();
        FindRealTool();
    }
}
