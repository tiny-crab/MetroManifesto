using System;
using System.Collections.Generic;

public class Route {
	public List<StationConnection> connections;
	public int difficulty;

	public Route(List<StationConnection> connections, int difficulty) {
		this.connections = connections;
		this.difficulty = difficulty;
	}
}

