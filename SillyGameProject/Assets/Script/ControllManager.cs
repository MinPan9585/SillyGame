using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllManager : MonoBehaviour
{
    // public int PlayerOneConID, PlayerTwoConID;
    public Vector2 PlayerOneDirection, PlayerTwoDirection, CharacterDir;
    public Vector3 CharacterDir3D;
    public float ForceStrength, canMoveTime, MoveStepTime, PressTogetherTime;
    public bool hasPlayer_LeftSide;
    public string hasToolName;
    public GameObject hasToolObj, TrashBin_Mesh, RealInteractiveObj;
    public Transform PutDownPlace;

    public List<GameObject> canSeeObj, canInteractObj;



    public GameObject Character;
    // Start is called before the first frame update
    public Rigidbody CharacterRgbd;
    private bool leftFeetDown, rightFeetDown, canMove, leftInteract, rightInteract;


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
        if (hasToolName != "None")
        {
            if (hasToolObj.name == "TrashBin")
            {
                TrashBin_Mesh.SetActive(false);
                if (RealInteractiveObj != null)
                {
                    RealInteractiveObj.GetComponent<Outline>().enabled = false;
                    RealInteractiveObj = null;
                }
            }

            hasToolObj.transform.position = PutDownPlace.position;
            hasToolObj.transform.rotation = PutDownPlace.rotation;
            hasToolObj.SetActive(true);
            hasToolName = "None";

        }
        else if (canSeeObj.Count >= 1 && hasToolName == "None")
        {
            /*GameObject realPickUpObj = canSeeObj[0];
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
            }*/
            GameObject realPickUpObj = PickRealObj(canSeeObj);

            if (realPickUpObj.GetComponent<Tool>().Name == "TrashBin")
            {
                realPickUpObj.SetActive(false);
                TrashBin_Mesh.SetActive(true);
            }

            hasToolObj = realPickUpObj;
            canSeeObj.Remove(hasToolObj);
            hasToolName = hasToolObj.GetComponent<Tool>().Name;
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
            Debug.Log("interact together");
            if (hasToolName == "TrashBin")
            {
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
            if (hasToolName == "TrashBin")
            {
                List<GameObject> TrashList = new List<GameObject>();
                for (int i = 0; i < canInteractObj.Count; i++)
                {
                    if (canInteractObj[i].GetComponent<Interactive>().Name == "Trash" && !TrashList.Contains(canInteractObj[i]))
                    {
                        TrashList.Add(canInteractObj[i]);
                    }
                }

                if (TrashList.Count > 0)
                {
                    GameObject RealTrash = PickRealObj(TrashList);
                    if (RealInteractiveObj == null)
                    {
                        RealTrash.GetComponent<Outline>().enabled = true;
                        RealInteractiveObj = RealTrash;

                    }
                    if (RealTrash != RealInteractiveObj)
                    {
                        RealInteractiveObj.GetComponent<Outline>().enabled = false;
                        RealTrash.GetComponent<Outline>().enabled = true;
                        RealInteractiveObj = RealTrash;
                    }

                }
            }
        }
    }

    private void Awake()
    {
        CharacterRgbd = Character.GetComponent<Rigidbody>();
        hasPlayer_LeftSide = false;
        hasToolName = "None";
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

    }
}
