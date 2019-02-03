using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WettableObject : GameSystemObject
{
    public float m_Slipperyness = 3000000f;

    public override void HandleElementalEnergy(Element element)
    {
        base.HandleElementalEnergy(element);

        if (element.m_ElementType == Element.ElementType.Water)
        {
            //burn boy
            Debug.Log("Go witht the flow of the ocean man!");
        }
    }
}
