using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbLeveler : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private GameObject xpLiquid;
    [SerializeField] private GameObject healthLiquid;

    private Material xpLiquidMaterial;
    private Material healthLiquidMaterial;

    // Start is called before the first frame update
    void Start()
    {
        xpLiquidMaterial = xpLiquid.GetComponent<Renderer>().material;
        healthLiquidMaterial = healthLiquid.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthLiquidMaterial.SetFloat("_Liquid_Level", (playerStats.CurrentLife / playerStats.MaxLife));
        xpLiquidMaterial.SetFloat("_Liquid_Level", (playerStats.CurrentExp / playerStats.ExpNextLevel));
    }
}
