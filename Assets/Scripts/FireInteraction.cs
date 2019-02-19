using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireInteraction : ElementController
{
    public GameObject m_Projectile;
    public float m_Force = 50.0f;
    public float m_EnergyRequired = 7.5f;

    public override void Absorb(GameSystemObject gso)
    {
        base.Absorb(gso);
        if (gso.m_ElementType == GameSystemObject.ElementType.Fire)
        {
            //specific fire protocol
        }
    }

    public override void Expel(Vector3 originPos, Vector3 direction)
    {
        m_TotalEnergy = Mathf.Clamp(m_TotalEnergy - m_EnergyRequired, 0, 1000); //TODO add max energy const
        if (m_TotalEnergy <= 0) return;

        GameObject projectile = MonoBehaviour.Instantiate(m_Projectile, originPos, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(direction * m_Force);
    }
}
