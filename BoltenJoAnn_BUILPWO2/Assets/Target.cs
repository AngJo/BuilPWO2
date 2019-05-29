using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float health = 50f;
    private int points = 0;
    public GameObject ingredientPrefab;
    public Transform pos;
    private GameObject ingredient;
    //public void TakeDamage(float amount)
    //   {
    //       health -= amount;
    //       if (health <= 0)
    //       {
    //           Destroy(gameObject);
    //       }
    //   }

    private void OnCollisionEnter(Collision collision)
    {
        //When you hit the egg target, grow the main egg
        if(collision.collider.tag == "Egg")
        {
            points += 1;
            //check if ingredient is already instantiated otherwise dont make it
            if (ingredient != null)
            {
                //increase it's size to get a giant egg
                ingredient.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
            }
            else
            {
                //create the egg ingredient and set it's gravity to false
                ingredient = (GameObject)Instantiate(ingredientPrefab, pos);
                ingredient.tag = "Egg Ingredient";
                Rigidbody ingredientRb = ingredient.GetComponent<Rigidbody>();
                ingredientRb.useGravity = false;
            }         
            
            Debug.Log(points);
        }
    }

    
}
