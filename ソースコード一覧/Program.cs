using ConsoleAppSQL;
using System.Data.SqlClient;

class Program
{
	static void Main()
	{
		// 接続文字列
		//string connectionString = "Data Source=localhost;Initial Catalog=master;User ID=YourName;Password=YourPassword"; // YourName = User名 YourPassword = パスワード

		// 接続成功時
		try
		{
			// SqlConnectionのインスタンス
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// DB接続
				connection.Open();
				Console.WriteLine("タスクをDBに登録、出力するアプリケーションです。");
				Console.WriteLine("実施する操作を次の数値から選択してください。\n【1】:タスクの追加【2】:タスク一覧の取得");
				var consoleValue = GetConsoleValue();

				if (consoleValue == "1")
				{
					InsertTastList.InsertTask(connection);
				}
				else if (consoleValue == "2")
				{
					TaskGetProcess(connection);
				}
                else
                {

					Console.WriteLine("正しい数値が入力されませんでした。");
                }

            }
		}
		// 接続失敗時
		catch (SqlException e)
		{
			Console.WriteLine("接続エラー: " + e.Message);
		}

		Console.ReadKey(true);
	}

	/// <summary>
	/// タスクを一覧を取得する処理
	/// </summary>
	/// <param name="connection">SqlConnection</param>
	private static void TaskGetProcess(SqlConnection connection)
	{
		Console.WriteLine("実施する操作を次の数値から選択してください。\n【1】:タスク一覧の表示、更新【2】:タスク一覧をExcelファイルに出力");
		var consoleValue = GetConsoleValue();
		if (consoleValue == "1")
		{
			while (true)
			{
				DiplayTaskList.DisplayTaskList(connection);

				Console.WriteLine("表示されているタスクを更新する場合は1を入力してください");

				if (GetConsoleValue() == "1")
				{
					UpdateTask.UpdateTaskItem(connection);
				}
			}
		}
		else if (consoleValue == "2")
		{
			OutputExcelTaskList.OutPutExcelTaskList(connection);
		}
	}

	/// <summary>
	/// 入力された文字列を取得する
	/// </summary>
	/// <returns>入力された文字列</returns>
	private static string GetConsoleValue()
	{		
		while (true)
		{
			var str = Console.ReadLine();
			if (str == "1" || str == "2")
			{
				return str;
			}
			Console.WriteLine("正しい数値が入力されませんでした。もう一度入力してください。");
		}
	}
}