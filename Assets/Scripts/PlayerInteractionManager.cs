﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//takes in player inputs and puts them to interactions
public class PlayerInteractionManager : MonoBehaviour
{
    public float m_MaxInteractDistance = 10.0f;

    //classes dictating how elements are used, based on ElementController
    public FireInteraction m_FireElementController;
    public AirInteraction m_AirElementController;
    public EarthInteraction m_EarthElementController;

    //current element being wielded
    private ElementController m_CurrentElementController;

    private void Start()
    {
        m_CurrentElementController = m_AirElementController;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            TryAbsorb();
        }

        else if (Input.GetMouseButtonDown(0))
        {
            ExpelElement();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            DispelElement();
        }

    }

    //raycasts to try and absorb elemental health from objects
    void TryAbsorb()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0)).direction);

        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            //if hit object
            GameSystemObject gso = hit.collider.gameObject.GetComponent<GameSystemObject>();

            if (gso)
            {
                //check to see which element the player is wielding
                if (gso.m_ElementType == GameSystemObject.ElementType.Fire)
                {
                    m_CurrentElementController = m_FireElementController;
                }
                else if (gso.m_ElementType == GameSystemObject.ElementType.Earth)
                {
                    m_CurrentElementController = m_EarthElementController;
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
        m_CurrentElementController.Expel(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);
    }

    void DispelElement()
    {
        m_CurrentElementController.Dispel();
    }
}
