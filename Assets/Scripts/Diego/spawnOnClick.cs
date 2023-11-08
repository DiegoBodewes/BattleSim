using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOnClick : MonoBehaviour
{
    public GameObject[] unitPrefabs;  // Prefab to spawn.
    private GameObject selectedUnit;  // Selected unit.

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
                    // Click position to spawn.
                    SpawnUnit(hit.point);
                }
                else
                {
                    // Select a unit by clicking on it (hier gaat doet ie raar).
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
