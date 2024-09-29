using System.Data.SqlClient;

namespace ConsoleAppSQL;

/// <summary>
/// タスクを追加する処理のクラス
/// </summary>
public class InsertTastList
{
	/// <summary>
	/// DBに接続しタスクを追加する
	/// </summary>
	/// <param name="connection">SqlConnection</param>
	public static void InsertTask(SqlConnection connection)
	{
		string insertSql = "INSERT INTO [InputSqlTest].[dbo].[TaskList] (TaskName, StartDate, EndDate, Contents, Status, PlanManHour, ResultManHour, Deviation, Note) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9)";
		using (SqlCommand command = new SqlCommand(insertSql, connection))
		{
			Console.WriteLine("必須：タスク名を入力してください。※未入力の場合は「未入力」として処理します。");
			var taskName = Console.ReadLine() ?? "未入力";

			Console.WriteLine("必須：タスクの開始日をyyyy/MM/dd形式で入力してください。");
			var startDate = ReadDateTime(false);

			Console.WriteLine("タスクの終了日をyyyy/MM/dd形式で入力してください。※未入力の場合は日付を「9999/12/31」として処理します。");
			var endDate = ReadDateTime(true);

			Console.WriteLine("タスクの内容を入力してください。");
			var contents = Console.ReadLine() ?? string.Empty;

			Console.WriteLine("タスクの状態を選択してください。※未入力の場合は「未着手」として処理します。\br次の数値を入力【 [0]：未着手 [1]：処理中 [2]：完了】");
			var status = GetStatus();

			Console.WriteLine("必須：タスクの予定工数を数値で入力してください。");
			var planManHour = ReadDecimal(false);
			Console.WriteLine("タスクの実績工数を数値で入力してください。※未入力の場合は0として処理します。");
			var resultManHour = ReadDecimal(true);

			var deviation = CalculateDeviation(planManHour, resultManHour);

			Console.WriteLine("備考を入力してください。");
			var note = Console.ReadLine() ?? string.Empty;

			// パラメータを設定
			command.Parameters.AddWithValue("@Value1", taskName); 
			command.Parameters.AddWithValue("@Value2", startDate);
			command.Parameters.AddWithValue("@Value3", endDate);
			command.Parameters.AddWithValue("@Value4", contents);
			command.Parameters.AddWithValue("@Value5", status);
			command.Parameters.AddWithValue("@Value6", planManHour);
			command.Parameters.AddWithValue("@Value7", resultManHour);
			command.Parameters.AddWithValue("@Value8", deviation);
			command.Parameters.AddWithValue("@Value9", note);

			// INSERT文を実行
			int rowsAffected = command.ExecuteNonQuery();
			Console.WriteLine($"Insert処理: {rowsAffected}");
		}
	}

	/// <summary>
	/// 入力された文字列が正しい日付かどうか判定し、DateTime型に変換する
	/// </summary>
	/// <param name="isNotRequired">入力必須の項目ではない場合、true</param>
	/// <returns>入力された文字列をDateTime型に変換した日付、入力されていないかつ入力必須ではない場合9999/12/31</returns>
	private static DateTime ReadDateTime(bool isNotRequired)
	{
		var date = new DateTime();
		while(true)
		{
			var dateStr = Console.ReadLine();
			if (dateStr == null && isNotRequired)
			{
				date = DateTime.MaxValue;
				break;
			}

			if (DateTime.TryParse(dateStr, out date))
			{
				break;
			}
			Console.WriteLine("入力された文字列を日付として処理できませんでした。もう一度入力してください。");
			if (isNotRequired) { Console.WriteLine("または、終了日が決まっていなければ、Enterを押してください。"); };
		};
		return date;
	}

	/// <summary>
	/// 入力された文字列が正しい数値かどうか判定し、Decimal型に変換する
	/// </summary>
	/// <param name="isNotRequired">入力必須の項目ではない場合、true</param>
	/// <returns>入力された文字列をDecimal型に変換した値、入力されていないかつ入力必須ではない場合、0</returns>
	private static decimal ReadDecimal(bool isNotRequired) 
	{
		decimal result = 0;
		while (true) {
			var str = Console.ReadLine();
			if(str == null && isNotRequired)
			{
				break;
			}
			if (decimal.TryParse(str, out result))
			{
				break ;
			}
			Console.WriteLine("入力された文字列を数値として処理できませんでした。もう一度入力してください。");
			if (isNotRequired) { Console.WriteLine("または、実績工数が決まっていなければ、Enterを押してください。"); };
		}
		return result;
	}

	/// <summary>
	/// 入力された値からタスクの状況を取得する
	/// </summary>
	/// <returns>入力された値に対応するタスクの状況、未入力の場合は「未着手」</returns>
	private static string GetStatus()
	{
		var value = Console.ReadLine();

		switch (value) {
			case "0" :
				return "未着手";
			case "1" :
				return "処理中";
			case "2" :
				return "完了";
			default :
				return "未着手";
		}
	}

	/// <summary>
	/// 予実乖離値（%）を取得する
	/// </summary>
	/// <param name="planValue">予定工数</param>
	/// <param name="resultValue">実績工数</param>
	/// <returns>予実乖離値（%）小数第3位以下切り捨て</returns>
	private static decimal CalculateDeviation(decimal planValue, decimal resultValue) 
	{
		var calculateResult = ((resultValue / planValue) * 100) - 100;
		return Math.Truncate(calculateResult * 100) / 100;
	}
}
