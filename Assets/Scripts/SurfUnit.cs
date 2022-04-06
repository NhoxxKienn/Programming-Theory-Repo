using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfUnit : Unit
{
    

    // Start is called before the first frame update
    void Start()
    {
        m_speed = 2f;
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

    protected override void Move()
    {
        int randomIndex;
        GameObject nextHex;
        do
        {
            randomIndex = Random.Range(0, currentHex.GetComponent<Hex>().SurroundingHexs.Count);
            nextHex = currentHex.GetComponent<Hex>().SurroundingHexs[randomIndex];
        } while (nextHex.GetComponent<Hex>().isOccupied || !nextHex.CompareTag("Sea"));
        Vector3 relativePos = nextHex.transform.position - currentHex.transform.position;

        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.position += relativePos;
        currentHex.GetComponent<Hex>().isOccupied = false;
        currentHex = nextHex;
        currentHex.GetComponent<Hex>().isOccupied = true;
    }

    public override string GetName()
    {
        return "Boat";
    }

}
