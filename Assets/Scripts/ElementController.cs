using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class of all element interactions called by the player
[System.Serializable]
public class ElementController
{
    public float m_TotalEnergy = 0.0f;
    public float m_Force = 0.0f; //force applied to interacted objects
    public float m_EnergyRequired = 7.5f; //energy required to use element
    public float m_EnergyTransfered = 30.0f; //energy transfered to other objects from interaction

    //how an element is absorbed
    public virtual void Absorb(GameSystemObject gso)
    {
        float absorbedAmount = gso.AbsorbEnergy();
        m_TotalEnergy += absorbedAmount;

        Debug.Log("Total Energy: " + m_TotalEnergy);
    }

    //how element is expelled or shot or directly used
    public virtual void Expel(Vector3 originPos, Vector3 direction)
    {
        
    }

    //how an element is charged
    public virtual void Charge()
    {

    }

    //how an element is dispeled or dispersed with beinge expelled
    public virtual void Dispel()
    {

    }
}
