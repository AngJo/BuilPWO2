using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public GameObject ingredient;

    private void OnMouseDown()
    {
        
        if(this.CompareTag("ResetButton"))
        {
            //Check if object exists
            if (GameObject.Find("Egg Ingredient") != null)
            {
                ingredient = GameObject.Find("Egg Ingredient");
                Destroy(ingredient);
            }
        }

        if (this.CompareTag("DropButton"))
        {
            //Check if object exists
            if(GameObject.Find("Egg Ingredient") != null)
            {
                ingredient = GameObject.Find("Egg Ingredient");
                Rigidbody ingredientRb = ingredient.GetComponent<Rigidbody>();
                ingredientRb.useGravity = true;
            }
        }
    }
}
