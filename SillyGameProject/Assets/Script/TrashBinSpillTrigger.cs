using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinSpillTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public TrashBin TrashBin_Script;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            TrashBin_Script.Spill();
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
