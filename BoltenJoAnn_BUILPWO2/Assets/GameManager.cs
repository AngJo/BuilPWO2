using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
        
    }

    [SerializeField]
    private int eggSize = 0;
    private int milkSize = 0;
    private int breadSize = 0;

    private const int correctEggValue = 4;
    private const int correctMilkValue = 6;
    private const int correctBreadValue = 5;

    private Transform pos;
    public Transform milkPos;
    public Transform eggPos;
    public Transform breadPos;
    private GameObject ingredient;
    private GameObject eggIngredient;
    private GameObject milkIngredient;
    private GameObject breadIngredient;
    private string bullet;
    private string ingredientName;
    private string parent;

    public GameObject cake;
    public GameObject blob;
    public Transform winSpawnPos;
    public GameObject mainCam;
    public GameObject winLoseCam;

    public GameObject textbox;
    public GameObject winLoseMenu;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There can only be one GameManager");
        }
        instance = this;
    }
    
    //When the target is hit, create an ingredient and assign a value
    public void TargetHit(string targetName, Collision collision, GameObject ingredientPrefab)
    {
        CheckTarget(targetName);
        //Check if the correct bullet has hit the correct target
        if (collision.collider.tag == bullet)
        {
            //check if ingredient is already instantiated otherwise dont make it
            if (DoesIngredientExist(targetName))
            {
                //increase it's size to get a giant egg
                ChangeSize(targetName);
            }
            else
            {
                Debug.Log(pos.position);
                //create the egg ingredient and set it's gravity to false
                ingredient = (GameObject)Instantiate(ingredientPrefab, pos.position, pos.rotation);
                ingredient.tag = ingredientName;
                ingredient.transform.parent = GameObject.Find(parent).transform;
                Rigidbody ingredientRb = ingredient.GetComponent<Rigidbody>();
                ingredientRb.useGravity = false;
                AssignValues(targetName);
            }
        }
    }
    
    void ChangeCamera()
    {
        mainCam.SetActive(false);
        winLoseCam.SetActive(true);
    }

    void AssignValues(string targetName)
    {
        switch (targetName)
        {
            case "Egg Target": eggSize = 1; eggIngredient = ingredient; break;
            case "Milk Target": milkSize = 1; milkIngredient = ingredient; break;
            case "Bread Target": breadSize = 1; breadIngredient = ingredient; break;
            default:
                break;
        }
    }

    void ChangeSize(string targetName)
    {
        switch (targetName)
        {
            case "Egg Target": eggIngredient.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); eggSize += 1; Debug.Log("Egg size is " + eggSize); break;
            case "Milk Target": milkIngredient.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); milkSize += 1; Debug.Log("Milk size is " + milkSize); break;
            case "Bread Target": breadIngredient.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F); breadSize += 1; Debug.Log("Bread size is " + breadSize); break;
            default:
                break;
        }
    }

    bool DoesIngredientExist(string targetName)
    {
        if (targetName == "Egg Target")
        {
            if (eggIngredient != null) { return true; }
        }
        else if (targetName == "Milk Target")
        {
            if (milkIngredient != null) { return true; }
        }
        else if (targetName == "Bread Target")
        {
            if (breadIngredient != null) { return true; }
        }
        return false;
    }
   
    void CheckTarget(string targetName)
    {
        switch (targetName)
        {
            case "Milk Target": bullet = "Milk"; ingredientName = "Milk Ingredient"; pos = milkPos; parent = "milkSpawn";  break;
            case "Egg Target": bullet = "Egg"; ingredientName = "Egg Ingredient"; pos = eggPos; parent = "eggSpawn"; break;
            case "Bread Target": bullet = "Bread"; ingredientName = "Bread Ingredient"; pos = breadPos; parent = "breadSpawn"; break;
            default: bullet = "Egg"; ingredientName = "Egg Ingredient"; break;
        }
    }

    void EndGame()
    {
        winLoseMenu.SetActive(true);
        ChangeCamera();
        GameObject player = GameObject.FindWithTag("Player");
        Player pl = player.GetComponent<Player>();
        pl.alive = false;
        Time.timeScale = 0;
        //Show Start Menu or Try again Button
    }
    public void DropIngredient(string buttonName)
    {
        Debug.Log(buttonName);

        if (buttonName == "DropEgg" && eggIngredient != null)
        {
            Debug.Log("Dropping Egg");
            Rigidbody eggRb = eggIngredient.GetComponent<Rigidbody>();
            eggRb.useGravity = true;
        }
        else if (buttonName == "DropMilk" && milkIngredient != null)
        {
            Debug.Log("Dropping Milk");
            Rigidbody milkRb = milkIngredient.GetComponent<Rigidbody>();
            milkRb.useGravity = true;
        }
        else if (buttonName == "DropBread" && breadIngredient != null)
        {
            Rigidbody breadRb = breadIngredient.GetComponent<Rigidbody>();
            breadRb.useGravity = true;
        }
    }

    public void ResetIngredient(string buttonName)
    {
        if (buttonName == "ResetChickens" && eggIngredient != null)
        {
            Debug.Log("Dropping Egg");
            Rigidbody eggRb = eggIngredient.GetComponent<Rigidbody>();
            eggRb.useGravity = true;
        }
        else if (buttonName == "ResetCows" && milkIngredient != null)
        {
            Debug.Log("Dropping Milk");
            Rigidbody milkRb = milkIngredient.GetComponent<Rigidbody>();
            milkRb.useGravity = true;
        }
        else if (buttonName == "ResetBread" && breadIngredient != null)
        {
            Rigidbody breadRb = breadIngredient.GetComponent<Rigidbody>();
            breadRb.useGravity = true;
        }
    }

    public void BakeCake()
    {
        Debug.Log("Egg size is " + eggSize);
        if (BowlTrigger.Instance.EggInTrigger && BowlTrigger.Instance.BreadInTrigger && BowlTrigger.Instance.MilkInTrigger)
        {
            // check ingredients in Trigger
            if (eggSize == correctEggValue && milkSize == correctMilkValue && breadSize == correctBreadValue)
            {
                //Instantiate a Cake
                GameObject winCake = (GameObject)Instantiate(cake, winSpawnPos);
                textbox.GetComponent<TextMeshProUGUI>().text = "You Win!";
                FindObjectOfType<AudioManager>().Play("winner");
                //Show You Win text
                //play winning audio
                EndGame();
            }
            else
            {
                //Instantiate Black Blob
                GameObject loseCake = (GameObject)Instantiate(blob, winSpawnPos);
                textbox.GetComponent<TextMeshProUGUI>().text = "Too bad...";
                FindObjectOfType<AudioManager>().Play("loser");
                //Show you lose text
                //play losing audio
                EndGame();
            }
        }
        else
        {
            //Instantiate Black Blob
            GameObject loseCake = (GameObject)Instantiate(blob, winSpawnPos);
            textbox.GetComponent<TextMeshProUGUI>().text = "Too bad...";
            FindObjectOfType<AudioManager>().Play("loser");
            //Show you lose text
            //play losing audio
            EndGame();
        }

    }

    
}
