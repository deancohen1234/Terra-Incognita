using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float m_ImpulseThreshold = 300f; //above this threshold, the object will break
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.impulse);

        if (collision.impulse.magnitude >= m_ImpulseThreshold)
        {
            Destroy(gameObject);
        }
    }
}
