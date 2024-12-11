using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private GameObject TornadoGameObject;
    [SerializeField] private float GroundCheckSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray TerrainDown = new Ray(transform.position, Vector3.down);
        Ray TerrainUp = new Ray(transform.position, Vector3.up);
        if (Physics.Raycast(TerrainDown, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                TornadoGameObject.transform.Translate(new Vector3(0, -hit.distance/GroundCheckSpeed, 0));
                //TornadoGameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal); //Copy rotation from the ground
            }
        }
        else if (Physics.Raycast(TerrainUp, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                TornadoGameObject.transform.Translate(new Vector3(0, hit.distance / GroundCheckSpeed, 0));
            }
        }
    }
}
