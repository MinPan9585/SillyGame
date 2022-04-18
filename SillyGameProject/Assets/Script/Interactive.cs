using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    // Start is called before the first frame update
    public string Name;
    public bool isReal, hasReset;
    private Outline outline_Script;

    private void Awake()
    {
        outline_Script = GetComponent<Outline>();
        hasReset = true;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isReal && hasReset)
        {
            outline_Script.enabled = true;
            hasReset = false;
        }
        else if (!isReal && !hasReset)
        {
            outline_Script.enabled = false;
            hasReset = true;
        }


    }
}
