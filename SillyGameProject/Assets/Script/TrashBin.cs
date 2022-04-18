using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class TrashBin : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> TrashList;
    public Transform TrashSpill_Anchor;
    public float TrashProcess, TrashBinMax;

    public void PickupTrash(GameObject newTrash)
    {
        //Debug.Log("Pickup Trash");
        TrashList.Add(newTrash);
        newTrash.SetActive(false);
        newTrash.GetComponent<Outlinable>().enabled = false;
        TrashProcess = TrashList.Count / TrashBinMax;
    }

    public void Spill()
    {
        if (TrashList.Count >= 1)
        {
            GameObject LastTrash = TrashList[TrashList.Count - 1];
            TrashList.Remove(LastTrash);
            LastTrash.transform.position = TrashSpill_Anchor.position;
            LastTrash.SetActive(true);
            TrashProcess = TrashList.Count / TrashBinMax;
        }
        else
        {
            Debug.Log("Fall but nothing in it");
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
