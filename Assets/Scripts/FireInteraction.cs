using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireInteraction : ElementController
{
    public GameObject m_EnergyBox; //box collider dictating area of affect of fire energy

    private GameObject m_InstantiatedAirbox;

    public override void Absorb(GameSystemObject gso)
    {
        base.Absorb(gso);
    }

    public override void Expel(Vector3 originPos, Vector3 direction)
    {
        if (m_InstantiatedAirbox == null)
        {
            m_InstantiatedAirbox = MonoBehaviour.Instantiate(m_EnergyBox, originPos, Quaternion.identity);
            m_InstantiatedAirbox.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            m_InstantiatedAirbox.transform.localRotation = Quaternion.identity;

            m_InstantiatedAirbox.GetComponent<EnergyBox>().SetupECBox(this, m_Force, m_EnergyTransfered);

        }
    }

    public override void Dispel()
    {
        if (m_InstantiatedAirbox)
        {
            MonoBehaviour.Destroy(m_InstantiatedAirbox);
            m_InstantiatedAirbox = null;
        }
    }

    public void Tick()
    {
        m_TotalEnergy = Mathf.Clamp(m_TotalEnergy - m_EnergyRequired, 0, 1000); //TODO add max energy const
        if (m_TotalEnergy <= 0) return;
    }
}
