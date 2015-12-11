using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

    private bool doublePoints;
    private bool powerUpActive;
    private float powerUpLengthCounter;
    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;
    private float normalPointsPerSecond;
    public AudioSource coinBoostSound;

    // Use this for initialization
    void Start () {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (powerUpActive)
        {
            powerUpLengthCounter -= Time.deltaTime;

            if(doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond = 2;
                theScoreManager.doubleCoin = true;
            }

            if(powerUpLengthCounter <= 0)
            {
                powerUpActive = false;
                theScoreManager.pointsPerSecond = normalPointsPerSecond;
                theScoreManager.doubleCoin = false;
            }
        }
	
	}

    public void ActivatePowerUp(bool points, float time)
    {
        coinBoostSound.Play();
        doublePoints = points;
        powerUpLengthCounter = time;

        normalPointsPerSecond = theScoreManager.pointsPerSecond;
        powerUpActive = true;
    }
}
