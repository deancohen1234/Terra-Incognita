using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private const float HEATMAXRANGE = 5.0f;
    private const float MAXTEMP = 75f;

    public Color m_HotColor;
    public float m_ImpulseThreshold = 300f; //above this threshold, the object will break
    public float m_CatchOnFireTemp = 30f;
    public float m_StartTemperature = 0.0f;
    public float m_HeatTransferScalar = 0.1f;

    public GameObject m_HeatAreaPrefab;

    [SerializeField]
    private float m_Temperature = 0; //temp of object, when this reaches certain amount, it will light on fire
    private Color m_EmissiveStartColor;

    private SphereCollider m_HeatArea; //area of heat emitting from interactable object


    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO make this a getter
    void CalculateHeatRange()
    {
        float heatRangeRadius = Map(m_Temperature, 0, MAXTEMP, 0.001f, HEATMAXRANGE);
        m_HeatArea.radius = heatRangeRadius;
    }

    void UpdateMaterialEmission()
    {
        float clampedTemp = Mathf.Clamp(m_Temperature, 0, m_CatchOnFireTemp);
        float lerpVal = Map(m_Temperature, 0.0f, m_CatchOnFireTemp, 0, 1.0f);

        Color newColor = Color.Lerp(m_EmissiveStartColor, m_HotColor, lerpVal);

        GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", newColor);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.impulse);

        if (collision.collider.gameObject.GetComponent<Element>())
        {
            if (collision.collider.gameObject.GetComponent<Element>().m_ElementType == Element.ElementType.Fire)
            {
                m_Temperature += collision.collider.gameObject.GetComponent<Element>().ElementalEnergy;
                CalculateHeatRange();
                UpdateMaterialEmission();
            }
        }

        if (collision.impulse.magnitude >= m_ImpulseThreshold)
        {
            Destroy(gameObject);
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
            float objectHeat = other.transform.parent.gameObject.GetComponent<InteractableObject>().GetTemperature();

            float heatPerFrame = (objectHeat / Mathf.Pow(distanceFromSource, 2)) * Time.deltaTime * m_HeatTransferScalar;

            m_Temperature =  Mathf.Clamp(m_Temperature + heatPerFrame, 0, MAXTEMP);


            CalculateHeatRange();
            UpdateMaterialEmission();

        }
    }

    private void OnDestroy()
    {
        GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", m_EmissiveStartColor);
    }

    private float Map(float value, float startMin, float startMax, float endMin, float endMax)
    {
        float diff = (value - startMin) / (startMax - startMin);

        float newValue = (endMin * (1 - diff)) + (endMax * diff);

        return newValue;
    }

    public float GetTemperature()
    {
        return m_Temperature;
    }
}
