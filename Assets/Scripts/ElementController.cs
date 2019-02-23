using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementController
{
    public float m_TotalEnergy = 0.0f;

    public virtual void Absorb(GameSystemObject gso)
    {
        float absorbedAmount = gso.AbsorbEnergy();
        m_TotalEnergy += absorbedAmount;

        Debug.Log("Total Energy: " + m_TotalEnergy);
    }

    public virtual void Expel(Vector3 originPos, Vector3 direction)
    {
        
    }

    public virtual void Charge()
    {

    }

    public virtual void Dispel()
    {

    }
}
