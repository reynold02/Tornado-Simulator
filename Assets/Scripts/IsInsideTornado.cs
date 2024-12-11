using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInsideTornado : MonoBehaviour
{
    [SerializeField] private bool BreakAfter;
    public bool InsideTornado = false;
    public float Lifespan;

    private void Update()
    {
        if (this.transform.position.y <= -9)
        {
            Destroy(this.gameObject);
        }
        //Detect if a mesh breaks after a lifespan
        if (BreakAfter && Lifespan <= 0)
        {
            this.gameObject.tag = "AffectedTornado";
            this.GetComponent<Rigidbody>().isKinematic = false;
            InsideTornado = true;
            this.GetComponent<MeshCollider>().convex = true;
        }
    }
}
