using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject car;
    public GameObject plane;
    public GameObject boat;
    public GameObject car2;

    public Camera GameCamera;
    public InfoPopup InfoPopup;

    public Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        plane.GetComponent<Unit>().currentHex = GameObject.Find("Ground_Hex_Tile (14)");
        plane.GetComponent<Unit>().currentHex.GetComponent<Hex>().isOccupied = true;
        boat.GetComponent<Unit>().currentHex = GameObject.Find("Sea_Hex_Tile (1)");
        boat.GetComponent<Unit>().currentHex.GetComponent<Hex>().isOccupied = true;
        car.GetComponent<Unit>().currentHex = GameObject.Find("Ground_Hex_Tile");
        car.GetComponent<Unit>().currentHex.GetComponent<Hex>().isOccupied = true;
        car2.GetComponent<Unit>().currentHex = GameObject.Find("Ground_Hex_Tile (6)");
        car2.GetComponent<Unit>().currentHex.GetComponent<Hex>().isOccupied = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            HandleSelection();
        }
    }

    void HandleSelection()
    {
        var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //the collider could be children of the unit, so we make sure to check in the parent
            unit = hit.collider.GetComponentInParent<Unit>();


            //check if the hit object have a IUIInfoContent to display in the UI
            //if there is none, this will be null, so this will hid the panel if it was displayed
            if (unit == null)
            {
                InfoPopup.gameObject.SetActive(false);
            }
            else
            {
                InfoPopup.Name.text = unit.GetName();
                InfoPopup.SpeedText.text = unit.GetData();
                InfoPopup.ToggleButton.onClick.RemoveAllListeners();
                InfoPopup.ToggleButton.onClick.AddListener(unit.ToggleActive);
                InfoPopup.gameObject.SetActive(true);
            }
        }
        else
        {
            InfoPopup.gameObject.SetActive(false);
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
