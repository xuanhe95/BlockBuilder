using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject ob = GameObject.FindWithTag("Te");
        Unit<GameObject, GameObject> u = new Unit<GameObject, GameObject>(ob);
        Instantiate(u.getP(), new Vector3(0, 0 , 0), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
