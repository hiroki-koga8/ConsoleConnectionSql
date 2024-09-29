using System.ComponentModel;

/// <summary>
/// タスクのモデルクラス
/// </summary>
public class TaskModel
{
	/// <summary>
	/// タスク名
	/// </summary>
	[DisplayName("タスク")]
	public string TaskName { get; set; }
	/// <summary>
	/// 開始日
	/// </summary>
	public string StartDate { get; set; }
	/// <summary>
	/// 終了日
	/// </summary>
	public string EndDate { get; set; }
	/// <summary>
	/// 内容
	/// </summary>
	public string Contents { get; set; }
	/// <summary>
	/// タスクの状態
	/// </summary>
	public string Status { get; set; }
	/// <summary>
	/// 予定工数
	/// </summary>
	public double PlanManHour { get; set; }
	/// <summary>
	/// 実績工数
	/// </summary>
	public double ResultManHour { get; set; }
	/// <summary>
	/// 予実乖離値（%）
	/// </summary>
	public double Deviation { get; set; }
	/// <summary>
	/// 備考
	/// </summary>
	public string Note { get; set; }

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="taskName">タスク名</param>
	/// <param name="startDate">開始日</param>
	/// <param name="endDate">終了日</param>
	/// <param name="contents">内容</param>
	/// <param name="status">状態</param>
	/// <param name="planManHour">予定工数</param>
	/// <param name="resultManHour">実績工数</param>
	/// <param name="deviation">予実乖離値</param>
	/// <param name="note">備考</param>
	public TaskModel(
		string taskName,
		string startDate,
		string endDate,
		string contents,
		string status,
		double planManHour,
		double resultManHour,
		double deviation,
		string note)
	{
		TaskName = taskName;
		StartDate = startDate;
		EndDate = endDate;
		Contents = contents;
		Status = status;
		PlanManHour = planManHour;
		ResultManHour = resultManHour;
		Deviation = deviation;
		Note = note;
	}
}
