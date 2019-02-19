using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlammableObject : GameSystemObject
{
    private const float HEATMAXRANGE = 5.0f;
    private const float MAXTEMP = 75f;

    public Color m_HotColor;
    public float m_CatchOnFireTemp = 30f;
    public float m_StartTemperature = 0.0f;
    public float m_HeatTransferScalar = 0.1f;

    public float m_MaxHeat = 300f;
    public GameObject m_HeatAreaPrefab;

    [SerializeField]
    private float m_Temperature = 0; //temp of object, when this reaches certain amount, it will light on fire
    private Color m_EmissiveStartColor;

    private SphereCollider m_HeatArea; //area of heat emitting from interactable object

    public override void HandleElementalEnergy(Element element)
    {
        base.HandleElementalEnergy(element);

        if (element.m_ElementType == Element.ElementType.Fire)
        {
            //burn boy
            Debug.Log("Burrrrning the Fields!");
        }
    }

    private void Start()
    {
        m_EmissiveStartColor = GetComponent<MeshRenderer>().material.GetColor("_EmissiveColor");

        GameObject g = Instantiate(m_HeatAreaPrefab);
        g.transform.parent = transform;
        g.transform.localPosition = Vector3.zero;

        m_HeatArea = g.GetComponent<SphereCollider>();
        m_Temperature = m_StartTemperature;

        CalculateHeatRange();
        UpdateMaterialEmission();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == gameObject)
        {
            //colliding with ourself
            return;
        }

        if (other.tag == "HeatArea")
        {
            //heat up object certain amount PER FRAME
            float distanceFromSource = Vector3.Distance(transform.position, other.transform.position);
            float objectHeat = other.transform.parent.gameObject.GetComponent<FlammableObject>().GetTemperature();

            float heatPerFrame = (objectHeat / Mathf.Pow(distanceFromSource, 2)) * Time.deltaTime * m_HeatTransferScalar;

            m_Temperature = Mathf.Clamp(m_Temperature + heatPerFrame, 0, MAXTEMP);


            CalculateHeatRange();
            UpdateMaterialEmission();

        }
    }

    private void CalculateHeatRange()
    {
        float heatRangeRadius = Map(m_Temperature, 0, MAXTEMP, 0.001f, HEATMAXRANGE);
        m_HeatArea.radius = heatRangeRadius;
    }

    private void UpdateMaterialEmission()
    {
        float clampedTemp = Mathf.Clamp(m_Temperature, 0, m_CatchOnFireTemp);
        float lerpVal = Map(m_Temperature, 0.0f, m_CatchOnFireTemp, 0, 1.0f);

        Color newColor = Color.Lerp(m_EmissiveStartColor, m_HotColor, lerpVal);

        GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", newColor);
    }

    public float GetTemperature()
    {
        return m_Temperature;
    }
}
