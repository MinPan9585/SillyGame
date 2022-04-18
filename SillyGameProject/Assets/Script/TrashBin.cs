using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> TrashList;
    public Transform DropOff_Anchor;
    public float TrashProcess, TrashBinMax;

    public void PickupTrash(GameObject newTrash)
    {
        //Debug.Log("Pickup Trash");
        TrashList.Add(newTrash);
        newTrash.SetActive(false);
        newTrash.GetComponent<Outline>().enabled = false;
    }

    public void Spill()
    {
        if (TrashList.Count >= 1)
        {
            GameObject LastTrash = TrashList[TrashList.Count - 1];
            TrashList.Remove(LastTrash);
            LastTrash.transform.position = DropOff_Anchor.position;
            LastTrash.SetActive(true);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TrashProcess = TrashList.Count / TrashBinMax;


    }
}
