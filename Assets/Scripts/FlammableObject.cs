using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlammableObject : GameSystemObject
{
    public float m_MaxHeat = 300f;

    public override void HandleElementalEnergy(Element element)
    {
        base.HandleElementalEnergy(element);

        if (element.m_ElementType == Element.ElementType.Fire)
        {
            //burn boy
            Debug.Log("Burrrrning the Fields!");
        }
    }
}
