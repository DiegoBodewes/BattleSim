using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectPlacement : MonoBehaviour
{
    public Transform spawnpointL1;
    public Transform spawnpointL2;
    public Transform spawnpointR1;
    public Transform spawnpointR2;

    public GameObject ShadowWizard;
    public GameObject Golem;

    private void Start()
    {
        Instantiate(ShadowWizard, spawnpointL1.position, Quaternion.identity);
        Instantiate(Golem, spawnpointR2.position, Quaternion.identity);

        // Onclick button selected Unit(Golem/Wizard)
        // Onclick L1,L2, R1 Or R2 spawn at pressed button

    }




}
