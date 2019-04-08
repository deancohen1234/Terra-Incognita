using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//takes in player inputs and puts them to interactions
public class PlayerInteractionManager : MonoBehaviour
{
    //Hand Transforms
    public Transform m_RightHand;
    public Transform m_LeftHand;

    public float m_MaxInteractDistance = 10.0f;

    //classes dictating how elements are used, based on ElementController
    public FireInteraction m_FireElementController;
    public AirInteraction m_AirElementController;
    public EarthInteraction m_EarthElementController;
    public WaterInteraction m_WaterElementController;

    //current element being wielded
    private ElementController m_CurrentElementController;
    private ParticleSystemForceField m_ForceField;

    private ParticleSystem.MinMaxCurve m_ForceFieldGravity;

    private void Start()
    {
        m_CurrentElementController = m_AirElementController;

        m_ForceField = GetComponent<ParticleSystemForceField>();
        m_ForceFieldGravity = m_ForceField.gravity;
        m_ForceField.gravity = 0;
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            TryAbsorb();
        }

        else if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            DispelElement();
        }

        else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            ExpelElement();
        }

        else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            DispelElement();
        }

    }

    //raycasts to try and absorb elemental health from objects
    void TryAbsorb()
    {
        RaycastHit hit;
        Ray ray = new Ray(m_RightHand.position, m_RightHand.forward);

        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            //if hit object
            GameSystemObject gso = hit.collider.gameObject.GetComponent<GameSystemObject>();

            if (gso)
            {
                m_ForceField.gravity = m_ForceFieldGravity;
                //check to see which element the player is wielding
                if (gso.m_ElementType == GameSystemObject.ElementType.Fire)
                {
                    m_CurrentElementController = m_FireElementController;
                }
                else if (gso.m_ElementType == GameSystemObject.ElementType.Earth)
                {
                    m_CurrentElementController = m_EarthElementController;
                }
                else if (gso.m_ElementType == GameSystemObject.ElementType.Water)
                {
                    m_CurrentElementController = m_WaterElementController;
                }

                m_CurrentElementController.Absorb(gso); //can absorb any element
                //draw in element
            }
        }

        else
        {
            m_CurrentElementController = m_AirElementController;
        }
    }

    void ExpelElement()
    {
        m_CurrentElementController.Expel(m_RightHand.position, m_RightHand.forward);
        m_ForceField.gravity = 0;
    }

    void DispelElement()
    {
        m_CurrentElementController.Dispel();

        m_ForceField.gravity = 0;
    }
}
