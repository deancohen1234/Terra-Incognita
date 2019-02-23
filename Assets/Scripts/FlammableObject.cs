using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlammableObject : GameSystemObject
{
    //constants
    private const float MAXCOLLIDERRADIUS = 5.0f;
    private const float MAXTEMP = 75f;

    [Header("Fire Properties")]
    public Color m_HotColor;
    public float m_CatchOnFireTemp = 30f;
    public float m_StartTemperature = 0.0f;
    public float m_HeatTransferScalar = 0.1f;
    public float m_MaxHeat = 300f;
    public float m_HealthLossScalar = 10.0f;
    public GameObject m_HeatAreaPrefab;
    public ParticleSystem m_BurningSystemPrefab;

    [SerializeField]
    private float m_Temperature; //temp of object, when this reaches certain amount, it will light on fire

    private bool m_bIsOnFire = false;

    private float m_Fuel
    {
        get { return Map(m_ElementalHealth, 0, MAXELEMENTALHEALTH, 0, MAXTEMP); }

        set { value = m_Fuel; }
    }

    private Color m_EmissiveStartColor;
    private SphereCollider m_HeatArea; //area of heat emitting from interactable object
    private ParticleSystem m_BurningSystem;

    public override void HandleElementalEnergy(Element element)
    {
        base.HandleElementalEnergy(element);

        if (element.m_ElementType == Element.ElementType.Fire)
        {
            //burn boy
            Debug.Log("Burrrrning the Fields!");
            m_Temperature += element.ElementalEnergy;

            UpdateTemperature();
        }
    }

    private void Start()
    {
        m_EmissiveStartColor = GetComponent<MeshRenderer>().material.GetColor("_EmissiveColor");

        GameObject g = Instantiate(m_HeatAreaPrefab);
        g.transform.parent = transform;
        g.transform.localPosition = Vector3.zero;

        ParticleSystem p = Instantiate(m_BurningSystemPrefab, transform);
        p.transform.localPosition = Vector3.zero;
        m_BurningSystem = p;

        m_HeatArea = g.GetComponent<SphereCollider>();
        m_Temperature = m_StartTemperature;

        UpdateTemperature();
    }

    private void Update()
    {
        if (m_bIsOnFire)
        {
            if (m_ElementalHealth <= 0)
            {
                m_ElementalHealth = 0;
                m_bIsOnFire = false;
                m_BurningSystem.Stop();
                m_Temperature = 0;

                CalculateHeatRange();
                UpdateMaterialEmission();

                return;
            }

            m_ElementalHealth -= Time.deltaTime * m_HealthLossScalar;
        }
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

            UpdateTemperature();
        }
    }

    private void UpdateTemperature()
    {
        if (m_Temperature >= m_CatchOnFireTemp)
        {
            m_bIsOnFire = true;
            m_BurningSystem.Play();
        }

        CalculateHeatRange();
        UpdateMaterialEmission();
    }

    private void CalculateHeatRange()
    {
        float heatRangeRadius = Map(m_Temperature, 0, MAXTEMP, 0.001f, MAXCOLLIDERRADIUS);
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

    public void AddHeat(float heat)
    {
        m_Temperature += heat;
        UpdateTemperature();
    }
}
