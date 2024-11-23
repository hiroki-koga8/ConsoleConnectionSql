using System.Data.SqlClient;

namespace ConsoleAppSQL;

/// <summary>
/// タスクを更新する処理のクラス
/// </summary>
public class UpdateTask
{
	public static void UpdateTaskItem(SqlConnection conn)
	{
		while (true)
		{
			string updateSql = "UPDATE  [InputSqlTest].[dbo].[TaskList] SET @targetItem = @newValue WHERE Id = @targetId";

			Console.WriteLine("更新するタスクのIDを入力してください");
			var targetId = Console.ReadLine();

			Console.WriteLine("更新する項目をしてください");
			var targetItem = Console.ReadLine();

			Console.WriteLine("更新後の値を入力してください");
			var newValue = Console.ReadLine();

			try
			{
				using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
				{
					updateCommand.Parameters.AddWithValue("@targetId", targetId); // 更新するタスクのID
					updateCommand.Parameters.AddWithValue("@targetItem", targetItem); // 更新する項目
					updateCommand.Parameters.AddWithValue("@newValue", newValue); // 更新する値

					// UPDATE文を実行
					int rowsAffected = updateCommand.ExecuteNonQuery();
					Console.WriteLine($"Update処理: {rowsAffected}");
				}
				Console.WriteLine("更新が完了しました");
				break;
			}
			catch (Exception ex)
			{
				Console.WriteLine("更新に失敗しました。再度入力してください。");
			}
		}
	}
}
