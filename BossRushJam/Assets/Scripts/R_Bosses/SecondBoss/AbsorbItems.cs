using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbItems : MonoBehaviour
{
    [SerializeField] string _targetItemId;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LoockNearestObjects()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(_targetItemId);
        GameObject[] distantObjects, nearObjects;
        float distance = Vector3.Distance(items[0].transform.position, transform.position);
        for (int i = 0;  i < items.Length; i++)
        {
            if(Vector3.Distance(items[0].transform.position, transform.position) < distance)
            {
                //distantObjects[i] =  
            }
        }
        
        
    }
}
