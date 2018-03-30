using System;

public class StationConnection {
	public Station origin;
	public Station destination;
	public float distance;
	public float duration;

	public StationConnection(Station origin, Station destination, float distance, float duration) {
		this.origin = origin;
		this.destination = destination;
		this.distance = distance;
		this.duration = duration;
	}
}
