namespace ConsoleApp
{
	internal class Program
	{
		static void Main()
		{
			AdoNetDb db = new("exampleSqlite.db");

			// --- ONLY RUN ONCE!
			//db.CreateDbAndTable();
			// ---

			// --- Create and then read users from db.
			int firstUserId = db.InsertUser("Someone Somewhere");
			int secondUserId = db.InsertUser("Anotherone Somewhereelse");

			string firstNameCreated = db.ReadUserName(firstUserId);
			string secondNameCreated = db.ReadUserName(secondUserId);

			//string firstNameCreated = db.ReadUserName(1);
			//string secondNameCreated = db.ReadUserName(2);

			Console.WriteLine($"First value inserted got id: {firstUserId}. Name retrived for this pk was: '{firstNameCreated}'");
			Console.WriteLine($"Second value inserted got id: {secondUserId}. Name retrived for this pk was: '{secondNameCreated}'");
			// ---

			// --- Only run to DELETE DB!
			// --- Comment out everything else to run it.
			//db.DropDb();
			// ---
		}
	}
}