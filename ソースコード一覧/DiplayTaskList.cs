using System.Data.SqlClient;

namespace ConsoleAppSQL;

/// <summary>
/// コンソールアプリ上にタスクの一覧を表示する処理のクラス
/// </summary>
public class DiplayTaskList
{
	/// <summary>
	/// タスクの一覧を表示する
	/// </summary>
	/// <param name="connection">SqlConnection</param>
	public static void DisplayTaskList(SqlConnection connection)
	{
		var taskList = GetTaskList.GetAllTaskList(connection);
		Console.WriteLine("ID, タスク, 開始日, 終了日, 内容, 状態, 予定工数, 実績工数, 予実乖離値（%）, 備考");
		foreach (var task in taskList) {
			Console.WriteLine($"{task.Id}, {task.TaskName}, {task.StartDate} ,{task.StartDate}, {task.EndDate}, {task.Contents}, {task.Status}, {task.PlanManHour}, {task.ResultManHour}, {task.Deviation}, {task.Note}");
		}
	}
}
