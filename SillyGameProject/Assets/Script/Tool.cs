using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    // Start is called before the first frame update
    public string Name;
    public GameObject Mesh;
    public bool isThere;

    private Collider thisCollider;

    public void PickUp_And_DropOff(bool isDropOff)
    {
        Mesh.SetActive(isDropOff);
        thisCollider.enabled = isDropOff;
    }

    void Awake()
    {
        thisCollider = GetComponent<Collider>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

 
}
