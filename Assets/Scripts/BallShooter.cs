using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public GameObject m_ThrowingObject;
    public float m_ThrowForce = 20f;

    public Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_Text.text = "Force: " + m_ThrowForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            m_ThrowForce += 100f;
            m_Text.text = "Force: " + m_ThrowForce;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            m_ThrowForce -= 100f;
            m_Text.text = "Force: " + m_ThrowForce;
        }
    }

    void FireProjectile()
    {
        GameObject g = Instantiate(m_ThrowingObject);
        g.transform.position = transform.position + transform.forward * 1.5f; //position object a little in front of player
        g.transform.rotation = Quaternion.identity;

        g.GetComponent<Rigidbody>().AddForce(transform.forward * m_ThrowForce, ForceMode.Force);
    }
}
