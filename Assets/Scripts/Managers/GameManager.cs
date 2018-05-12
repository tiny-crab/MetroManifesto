using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The GameManager will handle instantiation and win conditions of the main scene.
public class GameManager : MonoBehaviour {

	public UIManager uiManager;
	public RouteManager routeManager;

	void Start() { 
		uiManager.setRouteManager(routeManager); 
	}
}
