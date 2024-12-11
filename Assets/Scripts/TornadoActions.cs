using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoActions : MonoBehaviour
{
    private Vector3 SpawnLocation;
    [SerializeField] private Environment_Control EnvironmentControl_;
    private float ElapseTime;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private GameObject TornadoCenter, TornadoCenterRotator;
    private Transform TornadoCenterRotatorTransform;
    [SerializeField] private float TornadoRotationSpeed, PullForce, RefreshRate, DesiredDuration = 10, LifeSubstractPower;
    private float PercentageComplete;

    private void Awake()
    {
        TornadoCenterRotatorTransform = TornadoCenterRotator.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetRandomLocation();
    }

    // Update is called once per frame
    void Update()
    {
        TornadoCenterRotatorTransform.position = this.transform.position;
        TornadoCenterRotatorTransform.Rotate(new Vector3(0, Time.deltaTime * TornadoRotationSpeed, 0));
        ElapseTime += Time.deltaTime;
        PercentageComplete = ElapseTime / DesiredDuration;
        this.transform.position = Vector3.Lerp(this.transform.position, SpawnLocation, curve.Evaluate(PercentageComplete));
        if (ElapseTime > 9)
        {
            ElapseTime = 0;
            GetRandomLocation();
        }
    }
    private void GetRandomLocation()
    {
        SpawnLocation = EnvironmentControl_.SpawnLocations[Random.Range(0, 39)].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AffectedTornado"))
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<IsInsideTornado>().InsideTornado = true;
            if (other.TryGetComponent(out TVSignal tvsignal))
            {
                if (tvsignal.gameObject.GetComponent<IsInsideTornado>().Lifespan <= 0)
                {
                    tvsignal.InterruptTVSignal();
                }
            }
            StartCoroutine(IncreasePull(other));
        }
        else if (other.GetComponent<IsInsideTornado>().Lifespan > 0)
        {
            StartCoroutine(LifeSubstract(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AffectedTornado"))
        {
            other.GetComponent<IsInsideTornado>().InsideTornado = false;
        }
    }

    IEnumerator IncreasePull(Collider collider)
    {
        if (collider.GetComponent<IsInsideTornado>().InsideTornado)
        {
            Vector3 ForceDirection = TornadoCenter.transform.position - collider.transform.position;
            collider.GetComponent<Rigidbody>().AddForce(ForceDirection.normalized * PullForce * Time.deltaTime);
            yield return new WaitForSeconds(RefreshRate);
            StartCoroutine(IncreasePull(collider));
        }
        else
        {
            yield break;
        }
    }
    IEnumerator LifeSubstract(Collider other)
    {
        other.GetComponent<IsInsideTornado>().Lifespan -= Time.deltaTime * LifeSubstractPower;
        if (other.GetComponent<IsInsideTornado>().Lifespan <= 0)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(RefreshRate);
            StartCoroutine(LifeSubstract(other));
        }
    }
}
