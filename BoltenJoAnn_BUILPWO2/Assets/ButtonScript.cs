using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public GameObject ingredient;
    private GameObject trigger;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("ResetButton"))
                {
                    Debug.Log("This is the reset Button");
                    if (GameObject.FindWithTag("Egg Ingredient") != null)
                    {
                        Debug.Log("I couldn't find any eggs");
                        ingredient = GameObject.FindWithTag("Egg Ingredient");
                        Debug.Log(ingredient);
                        Destroy(ingredient);
                        Debug.Log("Ingredient has been destroyed");
                    }
                }
                else if (hit.collider.CompareTag("DropButton"))
                {
                    Debug.Log("This is the drop button");
                    if (GameObject.FindWithTag("Egg Ingredient") != null)
                    {
                        ingredient = GameObject.FindWithTag("Egg Ingredient");
                        Rigidbody ingredientRb = ingredient.GetComponent<Rigidbody>();
                        ingredientRb.useGravity = true;
                    }
                }
                else if (hit.collider.CompareTag("RestartButton"))
                {
                    //reload current scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    //Light Problems on Reload
                }

                else if (hit.collider.CompareTag("BakeButton"))
                {
                    //trigger = GameObject.FindWithTag("Trigger");
                    //BakeScript bake = trigger.GetComponent<BakeScript>();
                    BakeScript bake = GetComponent<BakeScript>();
                    bake.BakeCake();
                }
            }
        }
    }
    
}
