using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class FollowParticles : MonoBehaviour
{
    public VisualEffect effect;
    public Transform m_Target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = m_Target.position - transform.position;
        effect.SetVector3("Direction", direction);
    }
}
