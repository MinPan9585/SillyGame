using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class Trash : MonoBehaviour
{
    Outlinable Outline_Script;
    public void SetOutline(bool _OnOff) 
    {
        Outline_Script.enabled = _OnOff;        
    }
    // Start is called before the first frame update
    void Start()
    {
        Outline_Script = GetComponent<Outlinable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
