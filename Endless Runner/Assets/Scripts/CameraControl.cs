using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public PlayerControl thePlayer;

    private Vector3 lastPlayerLocation;
    private float distanceToMove;

    //setting the camera to follow the player
    //altering x value only - dont want the camera to jump @ jump


	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerControl>();
        lastPlayerLocation = thePlayer.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        distanceToMove = thePlayer.transform.position.x - lastPlayerLocation.x;

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerLocation = thePlayer.transform.position;
	}
}
