using System;
using System.Collections.Generic;

public class DesignedRoutes {

	public DesignedRoutes() {
		List<StationConnection> stationConnections = new List<StationConnection>();
		stationConnections.Add(new StationConnection(hermannstrasse, leinestrasse, 2000f, 40f));
		stationConnections.Add(new StationConnection(leinestrasse, boddinstrasse, 1500f, 50f));
		stationConnections.Add(new StationConnection(boddinstrasse, hermannplatz, 3000, 30f));
		stationConnections.Add(new StationConnection(hermannplatz, schonleinstrasse, 2000f, 30f));
		stationConnections.Add(new StationConnection(schonleinstrasse, kotbusserTor, 2500f, 20f));
		stationConnections.Add(new StationConnection(kotbusserTor, moritzplatz, 1000f, 40f));

		u8 = new Route(stationConnections, 5);
	}

	public Route u8;

	private Station moritzplatz = new Station("Moritzplatz", 3);
	private Station kotbusserTor = new Station("Kotbusser Tor", 4);
	private Station schonleinstrasse = new Station("Schoeleinstrasse", 2);
	private Station hermannplatz = new Station("Hermannplatz", 4);
	private Station boddinstrasse = new Station("Boddinstrasse", 1);
	private Station leinestrasse = new Station("Leinestrasse", 1);
	private Station hermannstrasse = new Station("Hermannstrasse", 3);
}