using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnergyBox : MonoBehaviour
{

    private ElementController m_ParentElementController;
    private float m_Force;
    private float m_Energy;

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
            body.AddForce(transform.forward * m_Force * Time.deltaTime);
        }

        if (other.gameObject.GetComponent<FlammableObject>())
        {
            other.gameObject.GetComponent<FlammableObject>().AddHeat(m_Energy * Time.deltaTime); //add heat to object per frame
        }
    }

    public void SetupECBox(ElementController ec, float force, float energy)
    {
        m_ParentElementController = ec;
        m_Force = force;
        m_Energy = energy;
    }
}
