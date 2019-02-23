using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnergyBox : MonoBehaviour
{
    public float m_ForceMultiplier = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody body = other.gameObject.GetComponent<Rigidbody>();

        if (body)
        {
            Debug.Log(transform.forward);
            body.AddForce(transform.parent.TransformDirection(transform.forward) * m_ForceMultiplier * Time.deltaTime);
        }
    }
}
