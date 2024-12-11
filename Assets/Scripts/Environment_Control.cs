using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Environment_Control : MonoBehaviour
{
    [SerializeField] private List<GameObject> FanBlades = new List<GameObject>();
    [SerializeField] private float FanSpeed;
    private bool SwapFinish = true;
    [SerializeField] private Material ScreenMaterial;
    [SerializeField] private List<Texture> ScreenTextures;
    [SerializeField] private GameObject TornadoGameObject;
    public List<GameObject> SpawnLocations;

    // Start is called before the first frame update

    private void Awake()
    {
        ScreenMaterial.SetFloat("TVNoise_", 100);
    }

    void Start()
    {
        SetRandomTornadoLocation();
    }

    // Update is called once per frame
    void Update()
    {
        FanBlades[0].transform.Rotate(0, 0, FanSpeed * Time.deltaTime);
        FanBlades[1].transform.Rotate(0, 0, FanSpeed * Time.deltaTime);
        if (SwapFinish)
        {
            SwapFinish = false;
            StartCoroutine("CameraTextureSwap");
        }
    }
    IEnumerator CameraTextureSwap()
    {
        ScreenMaterial.SetTexture("Texture2D_0d1a2c8b3c11422ab2153ec9b24fc09d", ScreenTextures[0]);
        yield return new WaitForSeconds(3);
        ScreenMaterial.SetTexture("Texture2D_0d1a2c8b3c11422ab2153ec9b24fc09d", ScreenTextures[1]);
        yield return new WaitForSeconds(3);
        ScreenMaterial.SetTexture("Texture2D_0d1a2c8b3c11422ab2153ec9b24fc09d", ScreenTextures[2]);
        yield return new WaitForSeconds(3);
        SwapFinish = true;
        yield return null;
    }

    void SetRandomTornadoLocation()
    {
        TornadoGameObject.transform.position = SpawnLocations[Random.Range(0, 19)].transform.position;//From Range 0 to 19 are locations far from the house
    }
}
