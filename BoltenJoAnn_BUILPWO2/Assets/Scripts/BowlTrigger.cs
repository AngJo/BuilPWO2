using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlTrigger : MonoBehaviour {

    private static BowlTrigger instance;
    public static BowlTrigger Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    public bool EggInTrigger
    {
        get; private set;
    }

    public bool MilkInTrigger
    {
        get; private set;
    }

    public bool BreadInTrigger
    {
        get; private set;
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Nee");
        }
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Egg Ingredient": EggInTrigger = true; break;
            case "Milk Ingredient": MilkInTrigger = true; break;
            case "Bread Ingredient": BreadInTrigger = true; break;
            default:
                break;
        }
    }
}
