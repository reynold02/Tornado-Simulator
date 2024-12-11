using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SideDoorOpener : MonoBehaviour
{
    private Vector3 difference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            difference = other.transform.position - this.transform.position;
            if (difference.z > 0)
            {
                this.transform.DORotate(new Vector3(-90, -90, -26.93f), 2);
            }
            else if (difference.z < 0)
            {
                this.transform.DORotate(new Vector3(-90, -90, 160), 2);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.transform.DORotate(new Vector3(-90, -90, 90), 2);
            
        }
    }
}
