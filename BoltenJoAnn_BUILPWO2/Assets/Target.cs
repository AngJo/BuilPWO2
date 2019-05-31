using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float health = 50f;
    private int points = 0;
    public GameObject ingredientPrefab;
    private Transform pos;
    public Transform milkPos;
    public Transform eggPos;
    public Transform breadPos;
    private GameObject ingredient;
    private string bullet;
    private string ingredientName;
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

        CheckTarget();
        //When you hit the egg target, grow the main egg
        if (collision.collider.tag == bullet)
        {
            points += 1;
            //check if ingredient is already instantiated otherwise dont make it
            if (ingredient != null)
            {
                //increase it's size to get a giant egg
                ingredient.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
                ValuesScript ingredientValue = ingredient.GetComponent<ValuesScript>();
                ingredientValue.value += 1;
            }
            else
            {
                //create the egg ingredient and set it's gravity to false
                ingredient = (GameObject)Instantiate(ingredientPrefab, pos);
                ingredient.tag = ingredientName; //Not working?
                Rigidbody ingredientRb = ingredient.GetComponent<Rigidbody>();
                ingredientRb.useGravity = false;
                
            }         
            
            Debug.Log(points);
        }
    }

    void CheckTarget()
    {
        switch (this.name)
        {
            case "MilkTarget": bullet = "Milk"; ingredientName = "Milk Ingredient"; pos = milkPos; break;
            case "EggTarget": bullet = "Egg"; ingredientName = "Egg Target"; pos = eggPos; break;
            case "BreadTarget": bullet = "Bread"; ingredientName = "Bread Target"; pos = breadPos; break;
            default: bullet = "Egg"; ingredientName = "Egg Target"; break;
        }
    }

    
}
