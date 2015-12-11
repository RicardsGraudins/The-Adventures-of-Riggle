using UnityEngine;
using System.Collections;

public class coinBoost : MonoBehaviour {

    public bool doublePoints;
    public float coinTimer;
    private PowerUpManager thePowerUpManager;

	// Use this for initialization
	void Start () {
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //on trigger if, player activate the boost
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            thePowerUpManager.ActivatePowerUp(doublePoints, coinTimer);
            
        }
        gameObject.SetActive(false);
    }
}
