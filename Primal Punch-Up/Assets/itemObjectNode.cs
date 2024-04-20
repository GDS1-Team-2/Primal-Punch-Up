using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObjectNode : MonoBehaviour
{
    public GameObject lightingPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newPrefab = Instantiate(lightingPrefab, this.transform.position, this.transform.rotation);
           newPrefab.transform.parent = this.transform;
           newPrefab.transform.localScale = new Vector3(3,3,3) ;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
