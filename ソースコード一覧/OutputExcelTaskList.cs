using ClosedXML.Excel;
using System.Data.SqlClient;

namespace ConsoleAppSQL;

/// <summary>
/// タスク一覧をExcelに出力するクラス
/// </summary>
public class OutputExcelTaskList
{
	/// <summary>
	/// タスク一覧をExcelに出力する
	/// </summary>
	/// <param name="connection">SqlConnection</param>
	public static void OutPutExcelTaskList(SqlConnection connection)
	{
		var taskList = GetTaskList.GetAllTaskList(connection); 
		
		var workbook = CreateExcel(taskList);
		workbook.SaveAs(@"出力フォルダ\タスク管理.xlsx");
		Console.WriteLine("出力フォルダにExcelファイルの出力が完了しました。");
	}

	/// <summary>
	/// Excelファイルを作成する
	/// </summary>
	/// <param name="taskList">タスクのリスト</param>
	/// <returns> 作成したExcelファイルのワークブック情報</returns>
	private static XLWorkbook CreateExcel(List<TaskModel> taskList)
	{
		var workbook = new XLWorkbook();
		IXLWorksheet worksheet = workbook.AddWorksheet("タスク一覧");

		// 1行目にタイトルをセットする
		int row = 1;
		worksheet.Cell(row, 1).SetValue("タスク");
		worksheet.Cell(row, 2).SetValue("開始日");
		worksheet.Cell(row, 3).SetValue("終了日");
		worksheet.Cell(row, 4).SetValue("内容");
		worksheet.Cell(row, 5).SetValue("状態");
		worksheet.Cell(row, 6).SetValue("予定工数");
		worksheet.Cell(row, 7).SetValue("実績工数");
		worksheet.Cell(row, 8).SetValue("予実乖離値（%）");
		worksheet.Cell(row, 9).SetValue("備考");
		row++;

		// 2行目以降にタスクのデータをセットする
		foreach (var task in taskList) {
			worksheet.Cell(row, 1).SetValue(task.TaskName);
			worksheet.Cell(row, 2).SetValue(task.StartDate);
			worksheet.Cell(row, 3).SetValue(task.EndDate);
			worksheet.Cell(row, 4).SetValue(task.Contents);
			worksheet.Cell(row, 5).SetValue(task.Status);
			worksheet.Cell(row, 6).SetValue(task.PlanManHour);
			worksheet.Cell(row, 7).SetValue(task.ResultManHour);
			worksheet.Cell(row, 8).SetValue(task.Deviation);
			worksheet.Cell(row, 9).SetValue(task.Note);
			row++;
		}

		//出力した表の内側に罫線を引く
		worksheet.Range(1, 1, row, 9).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

		//出力した表の外側に罫線を引く
		worksheet.Range(1, 1, row, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

		// ヘッダー(1行目)に背景色を付ける
		worksheet.Range(1, 1, 1, 9).Style.Fill.BackgroundColor = XLColor.SkyBlue;

		return workbook;
	}
}
