using System;
using UnityEngine;

public static class Utils {

	public static GameObject createInstance(float x, float y, UnityEngine.Object prefab) {
		GameObject instance = UnityEngine.Object.Instantiate(prefab) as GameObject;
		instance.transform.position = new Vector3(x, y);
		return instance;
	}

}