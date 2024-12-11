using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesWaveController : MonoBehaviour
{
    //Default Properties, WindDensity 0.63, WindStrenght 0.16, WindMovement x=6
    //Under the Storm, WindDensity 0.91, WindStrenght 3.29, WindMovement x=4.31
    [SerializeField] private Material PlantMaterial, TVMaterial;
    [SerializeField] private GameObject Tornado;
    [SerializeField] private Animator DoorAnimation;
    private float timer = 0;
    private bool TornadoSpawn = false;

    private void Awake()
    {
        PlantMaterial.SetFloat("WindDensity_", 0.63f);
        PlantMaterial.SetFloat("WindStrenght_", 0.16f);
        PlantMaterial.SetVector("WindMovement_", new Vector2(6, 0));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3 && !TornadoSpawn)//Random.Range(50, 80))
        {
            TornadoSpawn = true;
            SetPlantInStormMotion();
            SpawnTornado();
            TVMaterial.SetFloat("TVNoise_", 800);
        }
    }
        void SetPlantInStormMotion()
    {
        PlantMaterial.SetFloat("WindDensity_", 0.91f);
        PlantMaterial.SetFloat("WindStrenght_", 3.29f);
        PlantMaterial.SetVector("WindMovement_", new Vector2(6, 0));
    }

        void SpawnTornado()
    {
        Tornado.SetActive(true);
        DoorAnimation.SetTrigger("Storm");
    }
}
