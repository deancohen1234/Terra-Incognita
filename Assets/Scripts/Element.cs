using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public enum ElementType {Fire, Water, Earth, Air}

    public ElementType m_ElementType = ElementType.Fire;
    public float ElementalEnergy = 20.0f;
}
