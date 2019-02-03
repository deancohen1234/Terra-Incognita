using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemObject : MonoBehaviour
{

    public float m_ForceThreshold = 50f;
    public float m_ElementalHealth = 25f;

    private void OnCollisionEnter(Collision collision)
    {
        //measure both force and elemental effects
        if (collision.impulse.magnitude >= m_ForceThreshold)
        {
            Destroy(gameObject);
        }

        Element elementType = collision.gameObject.GetComponent<Element>();

        HandleElementalEnergy(elementType);
    }

    public virtual void HandleElementalEnergy(Element element)
    {
        //play same sound effect for all elements
        Debug.Log("Handling Elemental Energy");
    }
}
