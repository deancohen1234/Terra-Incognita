using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public float m_MaxInteractDistance = 10.0f;

    public FireInteraction m_FireElementController;

    private ElementController m_CurrentElementController;
    //make single gamesystem element that is used for all interactions

    private void Start()
    {
        m_CurrentElementController = m_FireElementController;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryAbsorb();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            ExpelElement();
        }

    }

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
                m_CurrentElementController.Absorb(gso); //can absorb any element
                //draw in element
            }
        }
    }

    void ExpelElement()
    {
        m_CurrentElementController.Expel(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);
    }
}
