using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected GameObject m_currentHex;
    public GameObject currentHex { get { return m_currentHex; } set { m_currentHex = value; } }

    protected GameObject targetHex;
    protected bool isActive;

    protected float waitTime = 0;
    protected float timerToMove = 150;

    protected float m_speed = 3f;
    public float speed { get { return m_speed; } set
        { 
            if (value <= 0.0f)
            {
                Debug.LogError("You can't set a negative speed");
            }
            else
            {
                m_speed = value;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            waitTime += speed;
            if (waitTime > timerToMove)
            {
                Move();
                waitTime = 0;
            }
        }
    }

    // POLYMORPHISM, ABSTRACTION, 
    protected virtual void Move()
    {
        int randomIndex;
        GameObject nextHex;
        do
        {
            randomIndex = Random.Range(0, currentHex.GetComponent<Hex>().SurroundingHexs.Count);
            nextHex = currentHex.GetComponent<Hex>().SurroundingHexs[randomIndex];
        } while (nextHex.GetComponent<Hex>().isOccupied);
        Vector3 relativePos = nextHex.transform.position - currentHex.transform.position;

        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.position += relativePos;
        currentHex.GetComponent<Hex>().isOccupied = false;
        currentHex = nextHex;
        currentHex.GetComponent<Hex>().isOccupied = true;
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }

    public virtual string GetName()
    {
        return "Plane";
    }

    public virtual string GetData()
    {
        return "Speed: " + m_speed;
    }
}
