using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

[System.Serializable]
public class WaterInteraction : ElementController
{
    public VisualEffect m_WaterStream;

    private bool m_IsWaterActive = false;

    public override void Absorb(GameSystemObject gso)
    {
        base.Absorb(gso);
    }

    public override void Expel(Vector3 originPos, Vector3 direction)
    {
        if (!m_IsWaterActive)
        {
            m_WaterStream.SendEvent("ShootWater");
            m_IsWaterActive = true;
        }

        Debug.Log("Expelling Water");
    }

    public override void Dispel()
    {
        m_IsWaterActive = false;
        m_WaterStream.Stop();
    }

    public void Tick()
    {

    }
}