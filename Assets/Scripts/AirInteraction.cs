using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AirInteraction : ElementController
{
    public GameObject m_AirBox;
    public float m_Force = 10.0f;

    private GameObject m_InstantiatedAirbox;

    public override void Absorb(GameSystemObject gso)
    {
        base.Absorb(gso);
        if (gso.m_ElementType == GameSystemObject.ElementType.Air)
        {
            //specific fire protocol
        }
    }

    public override void Expel(Vector3 originPos, Vector3 direction)
    {
        if (m_InstantiatedAirbox == null)
        {
            m_InstantiatedAirbox = MonoBehaviour.Instantiate(m_AirBox, originPos, Quaternion.LookRotation(direction));
            return;
        }

    }
}