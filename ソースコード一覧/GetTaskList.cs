using System.Data.SqlClient;

namespace ConsoleAppSQL;

/// <summary>
/// タスクを取得する処理をまとめたクラス　（今後増やしていく予定　月ごとに取得する等）
/// </summary>
public class GetTaskList
{
	/// <summary>
	/// DBに接続し、すべてのタスクを取得する
	/// </summary>
	/// <param name="connection">SqlConnection</param>
	/// <returns>DBに登録されているすべてのタスク</returns>
	public static List<TaskModel> GetAllTaskList(SqlConnection connection) 
	{
		string getSql = "SELECT * FROM [InputSqlTest].[dbo].[TaskList]";
		var taskList = new List<TaskModel>();
		using (SqlCommand command = new SqlCommand(getSql, connection))
		{
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					var task = new TaskModel(
						reader.GetString(1),
						reader.GetDateTime(2).ToString("yyyy/MM/dd"),
						SqlDataReaderExtensions.GetDateToyyyyMMdd(reader, 3),
						SqlDataReaderExtensions.GetString(reader, 4),
						reader.GetString(5),
						reader.GetDouble(6),
						SqlDataReaderExtensions.GetDouble(reader, 7),
						SqlDataReaderExtensions.GetDouble(reader, 8),
						SqlDataReaderExtensions.GetString(reader, 9)
					);

					taskList.Add(task);
				}
			}
		}
		/// 開始日 昇順 > 終了日 昇順 > タスク名　昇順
		return taskList
			.OrderBy(a => a.StartDate)
			.ThenBy(a => a.EndDate)
			.ThenBy(a => a.TaskName)
			.ToList();
	}
}
