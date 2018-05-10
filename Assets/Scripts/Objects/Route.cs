using System;
using System.Collections.Generic;

public class Route {
	public List<StationConnection> connections;
	public List<Station> stationList = new List<Station>();
	public int difficulty;

	public Route(List<StationConnection> connections, int difficulty) {
		this.connections = connections;
		foreach (StationConnection connection in connections) {
			stationList.Add(connection.origin);
		}
		this.difficulty = difficulty;
	}
}

