using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ShadowWizard;
    public GameObject Golem;

    private GameObject selectedPrefab;
    private Transform selectedGameObject;

    void Start()
    {
        
    }

    public void SelectWizard()
    {
        selectedPrefab = ShadowWizard;
    }

    public void SelectGolem()
    {
        selectedPrefab = Golem;
    }

    public void SelectSpawnPoint(Transform spawnPoint)
    {
        selectedGameObject = spawnPoint;
        SpawnSelectedPrefab();
    }

    public void SpawnSelectedPrefab()
    {
        if (selectedPrefab != null && selectedGameObject != null)
        {
            Instantiate(selectedPrefab, selectedGameObject.position, selectedGameObject.rotation);
        }
    }
}
