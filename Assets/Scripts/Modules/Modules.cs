public static class Modules
{
	public static Balance Balance { get; private set; }
	public static Timers Timers { get; private set; }
	public static Localization Localization { get; private set; }
	public static SceneNames SceneNames { get; private set; }

	public static TSVReader TSVReader { get; private set; }
	public static IPathfinding Pathfinding { get; private set; }

	public static void InitializeModules(Balance balance, Timers timers, Localization localization, SceneNames sceneNames)
	{
		Balance = balance;
		Timers = timers;
		Localization = localization;
		SceneNames = sceneNames;

		TSVReader = new TSVReader();
		Pathfinding = new SimplePathfinding();

		Localization.Initialize();
	}
}
