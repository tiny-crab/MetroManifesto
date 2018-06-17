using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The GameManager will handle instantiation and win conditions of the main scene.
public class GameManager : MonoBehaviour {

	public static GameManager instance;

	void Awake() { 
		if (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }
		DontDestroyOnLoad(gameObject);
	}
}
