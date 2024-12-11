using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSignal : MonoBehaviour
{
    [SerializeField] private MeshRenderer TVNoiseMesh;
    [SerializeField] private Material WhiteMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InterruptTVSignal()
    {
        TVNoiseMesh.material = WhiteMaterial;
    }
}
