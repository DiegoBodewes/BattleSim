using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOnClick : MonoBehaviour
{
    public GameObject[] unitPrefabs;  // An array of unit prefabs you want to spawn.
    private GameObject selectedUnit;  // The currently selected unit.

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if a unit is selected
                if (selectedUnit != null)
                {
                    // Spawn the selected unit at the hit point.
                    SpawnUnit(hit.point);
                }
                else
                {
                    // Select a unit by clicking on it (e.g., select the first unit in the unitPrefabs array).
                    SelectUnit(unitPrefabs[0]);
                }
            }
        }
    }

    void SelectUnit(GameObject unitPrefab)
    {
        selectedUnit = unitPrefab;
    }

    void SpawnUnit(Vector3 spawnPosition)
    {
        if (selectedUnit != null)
        {
            Instantiate(selectedUnit, spawnPosition, Quaternion.identity);
        }
    }
}
