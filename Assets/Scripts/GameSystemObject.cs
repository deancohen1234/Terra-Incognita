using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//base class for all interactable objects
public class GameSystemObject : MonoBehaviour
{
    public const float MAXELEMENTALHEALTH = 1000.0f;
    public enum ElementType { Fire, Water, Earth, Air }

    [Header("GSO Properties")]
    public ElementType m_ElementType = ElementType.Fire;
    public float m_ForceThreshold = 50f; //threshold before object is destroyed
    public float m_ElementalHealth = 25f;
    public float m_AbsorpionRate = 1.0f; //rate at which energy is taken from object

    public UnityEvent m_OnElementEnergyDepleted;

    private bool m_EnergyDepleted = false;

    protected float m_StartingHealth;

    private void Awake()
    {
        m_StartingHealth = m_ElementalHealth;
    }

    //TODO don't use update
    public virtual void Update()
    {
        if (m_ElementalHealth <= 0)
        {
            if (!m_EnergyDepleted)
            {
                m_EnergyDepleted = true;
                m_OnElementEnergyDepleted.Invoke();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //measure both force and elemental effects
        if (collision.impulse.magnitude >= m_ForceThreshold)
        {
            Destroy(gameObject);
        }

        //TODO phase out element so everything is a GSO
        Element elementType = collision.gameObject.GetComponent<Element>();

        HandleElementalEnergy(elementType);
    }

    public virtual void HandleElementalEnergy(Element element)
    {
        //TODO caluclate energy from collision hits
        m_ElementalHealth += element.ElementalEnergy;
    }

    public virtual float AbsorbEnergy()
    {
        if (m_ElementalHealth <= 0)
        {
            return 0.0f;
        }

        float elementDrain = m_AbsorpionRate * Time.deltaTime;

        //make sure health is in correct range
        m_ElementalHealth = Mathf.Clamp(m_ElementalHealth - elementDrain, 0, MAXELEMENTALHEALTH);

        return elementDrain; //return amount of health lost
    }

    protected float Map(float value, float startMin, float startMax, float endMin, float endMax)
    {
        float diff = (value - startMin) / (startMax - startMin);

        float newValue = (endMin * (1 - diff)) + (endMax * diff);

        return newValue;
    }
}
