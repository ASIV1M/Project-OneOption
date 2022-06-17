using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Item/Consumables/MedicineBottle", order = 53)]

public class MedicineBottle : Consumables
{
    public TypeConsumables TypeConsumables;
    public int AmountOfTreatment;
    
    public int HealUnit(int heath)
    {
        if(heath > 0)
        {
            AmountOfTreatment += heath;
        }

        return AmountOfTreatment;
    }
}
