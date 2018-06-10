using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool gameEnded = false;
	private CharacterController controller;
	private PlayerHealth playerHealth;
	private Quaternion destRotation;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        controller = GetComponent<CharacterController>();
    }
}
	
