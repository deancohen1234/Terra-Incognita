using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemObject : MonoBehaviour
{
    public const float MAXELEMENTALHEALTH = 1000.0f;
    public enum ElementType { Fire, Water, Earth, Air }

    [Header("GSO Properties")]
    public ElementType m_ElementType = ElementType.Fire;
    public float m_ForceThreshold = 50f;
    public float m_ElementalHealth = 25f;
    public float m_AbsorpionRate = 1.0f;


    private void OnCollisionEnter(Collision collision)
    {
        //measure both force and elemental effects
        if (collision.impulse.magnitude >= m_ForceThreshold)
        {
            Destroy(gameObject);
        }

        Element elementType = collision.gameObject.GetComponent<Element>();

        HandleElementalEnergy(elementType);
    }

    public virtual void HandleElementalEnergy(Element element)
    {
        m_ElementalHealth += element.ElementalEnergy;
    }

    public virtual float AbsorbEnergy()
    {
        if (m_ElementalHealth <= 0)
        {
            return 0.0f;
        }

        float elementDrain = m_AbsorpionRate * Time.deltaTime;
        m_ElementalHealth = Mathf.Clamp(m_ElementalHealth - elementDrain, 0, m_ElementalHealth); //TODO make max energy variable

        return elementDrain; //return amount of health lost
    }

    protected float Map(float value, float startMin, float startMax, float endMin, float endMax)
    {
        float diff = (value - startMin) / (startMax - startMin);

        float newValue = (endMin * (1 - diff)) + (endMax * diff);

        return newValue;
    }
}
