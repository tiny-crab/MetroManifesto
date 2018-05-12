using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour {

	private Train train;
	private List<GameObject> tracks = new List<GameObject>();
	private UnityEngine.Object trackPrefab, carriagePrefab;
	public Camera camera;
	public float camHeight, camWidth;
	public Vector3 camCenter;

	public void setTrain(Train train) {
		this.train = train;
	}

	// Use this for initialization
	void Start () {
		trackPrefab = Resources.Load("TrackPrefab");
		carriagePrefab = Resources.Load("CarriagePrefab");

		camHeight = 2f * camera.orthographicSize;
		camWidth = camHeight * camera.aspect;
		camCenter = camera.transform.position;

		var trackWidth = 0f;
		var trackLeftOrigin = camCenter.x - (camWidth / 2);
		while (trackWidth < camWidth) {
			var trackPiece = createInstance(trackLeftOrigin + trackWidth, 5f, trackPrefab);
			tracks.Add(trackPiece);
			trackWidth += trackPiece.GetComponent<Renderer>().bounds.size.x;
		}

		createInstance(0f, 5.5f, carriagePrefab);
	}
	
	// Update is called once per frame
	void Update () {
		moveTrack();
	}

	private GameObject createInstance(float x, float y, UnityEngine.Object prefab) {
		GameObject instance = Instantiate(prefab) as GameObject;
		instance.transform.position = new Vector3(x, y);
		return instance;
	}

	private void moveTrack() {
		foreach (GameObject track in tracks) {
			var newPositionX = track.transform.position.x - train.getVelocity() * Time.deltaTime / 2; 
			var newPositionY = track.transform.position.y;
			track.transform.position = new Vector3(newPositionX, newPositionY);
		}
		var rightTrackBound = tracks[0].transform.position.x + tracks[0].GetComponent<Renderer>().bounds.size.x / 2;
		if (rightTrackBound < camCenter.x - camWidth / 2) {
			var leftTrack = tracks[0];
			tracks.Remove(leftTrack);
			Destroy(leftTrack);
			var rightTrack = tracks[tracks.Count - 1];
			var nextTrackCenterX = rightTrack.transform.position.x + rightTrack.GetComponent<Renderer>().bounds.size.x;
			tracks.Add(createInstance(nextTrackCenterX, rightTrack.transform.position.y, trackPrefab));
		}
	}


}
