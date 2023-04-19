using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scale = (float)(1.1 + 0.1 * Mathf.Sin(Time.time*3f));;
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
}
