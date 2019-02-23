using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireInteraction : ElementController
{
    public GameObject m_AirBox;
    public float m_Force = 0.0f;
    public float m_EnergyRequired = 7.5f;
    public float m_EnergyTransfered = 30.0f;

    private GameObject m_InstantiatedAirbox;

    public override void Absorb(GameSystemObject gso)
    {
        base.Absorb(gso);
    }

    public override void Expel(Vector3 originPos, Vector3 direction)
    {
        if (m_InstantiatedAirbox == null)
        {
            m_InstantiatedAirbox = MonoBehaviour.Instantiate(m_AirBox, originPos, Quaternion.identity);
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
