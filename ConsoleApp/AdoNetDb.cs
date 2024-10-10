/**************************************************************************************************************/
/* By Tomas Sandnes, but example heavily inspired by:                                                         */
/* https://github.com/dotnet/docs/blob/main/samples/snippets/standard/data/sqlite/HelloWorldSample/Program.cs */
/**************************************************************************************************************/

using Microsoft.Data.Sqlite;

namespace ConsoleApp
{
	public class AdoNetDb
	{
		private readonly string _dbFile;
		private readonly string _connectionString;


		public AdoNetDb(string dbFile)
        {
			_dbFile = dbFile;
			//                    Data Source = exampleSqlite.db
			_connectionString = $"Data Source = {_dbFile}";
		}


        public void CreateDbAndTable()
		{
			using SqliteConnection connection = new(_connectionString);
			connection.Open();
			SqliteCommand command = connection.CreateCommand();
			command.CommandText = @" 
                CREATE TABLE user (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL
                );
            ";
			command.ExecuteNonQuery();
		}


		public int InsertUser(string name)
		{
			int generatedId = -1;

			// Doing an insert
			using SqliteConnection connection = new(_connectionString);
			connection.Open();
			SqliteCommand command = connection.CreateCommand();
			command.CommandText = @"
				INSERT INTO user (name)
				VALUES ($name);
			";
			command.Parameters.AddWithValue("$name", name);
			command.ExecuteNonQuery();

			// Doing a select
			command.CommandText = @"
				SELECT seq
				FROM sqlite_sequence
				WHERE name = 'user';
			";
			using SqliteDataReader reader = command.ExecuteReader();
			if (reader.Read()) {
				generatedId = reader.GetInt32(0);
			}

			return generatedId;
		}


		public string ReadUserName(int id)
		{
			string userName = "";

			using SqliteConnection connection = new(_connectionString);
			connection.Open();
			SqliteCommand command = connection.CreateCommand();
			command.CommandText = @"
				SELECT name
				FROM user
				WHERE id = $id
			";
			command.Parameters.AddWithValue("$id", id);
			using SqliteDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				userName = reader.GetString(0);
			}

			return userName;
		}


		public void DropDb()
		{
			File.Delete(_dbFile);
		}
	}
}
