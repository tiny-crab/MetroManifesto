using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour {

	private Train train;
	private List<GameObject> tracks = new List<GameObject>();
	private UnityEngine.Object trackPrefab;
	public Camera camera;
	public float camHeight;
	public float camWidth;
	public Vector3 camCenter;

	public void setTrain(Train train) {
		this.train = train;
	}

	// Use this for initialization
	void Start () {
		trackPrefab = Resources.Load("TrackPrefab");
		camHeight = 2f * camera.orthographicSize;
		camWidth = camHeight * camera.aspect;
		camCenter = camera.transform.position;
		var trackWidth = 0f;
		var trackLeftOrigin = camCenter.x - (camWidth / 2);
		while (trackWidth < camWidth) {
			var trackPiece = createTrack(trackLeftOrigin + trackWidth, 5f);
			tracks.Add(trackPiece);
			trackWidth += trackPiece.GetComponent<Renderer>().bounds.size.x;
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject track in tracks) {
			var newPositionX = track.transform.position.x - train.getVelocity() * Time.deltaTime; 
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
			tracks.Add(createTrack(nextTrackCenterX, rightTrack.transform.position.y));
		}
	}

	private GameObject createTrack(float x, float y) {
		GameObject track = Instantiate(trackPrefab) as GameObject;
		track.transform.position = new Vector3(x, y);
		return track;
	}


}
