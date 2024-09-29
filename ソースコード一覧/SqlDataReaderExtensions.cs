using System.Data.SqlClient;

namespace ConsoleAppSQL;

/// <summary>
/// SqlDataReaderの拡張メソッドをまとめたクラス
/// </summary>
public class SqlDataReaderExtensions
{
	/// <summary>
	/// DBからデータを取得する際にDateTime型の値を"yyyy/MM/dd"形式の文字列に変換する
	/// </summary>
	/// <param name="reader">SqlDataReader</param>
	/// <param name="index">取得するデータのインデックス</param>
	/// <returns>"yyyy/MM/dd"形式の文字列、取得できない場合は空の文字列</returns>
	public static string GetDateToyyyyMMdd(SqlDataReader reader, int index)
	{
		if (reader.IsDBNull(index)) { return string.Empty; }
		return reader.GetDateTime(index).ToString("yyyy/MM/dd");
	}

	/// <summary>
	/// DBから文字列型のデータを取得する
	/// </summary>
	/// <param name="reader">SqlDataReader</param>
	/// <param name="index">取得するデータのインデックス</param>
	/// <returns>取得した文字列、取得できない場合は空の文字列</returns>
	public static String GetString(SqlDataReader reader, int index)
	{
		if (reader.IsDBNull(index)) { return string.Empty; }
		return reader.GetString(index);
	}
	/// <summary>
	/// DBからDouble型のデータを取得する
	/// </summary>
	/// <param name="reader">SqlDataReader</param>
	/// <param name="index">取得するデータのインデックス</param>
	/// <returns>取得したDouble型の値、取得できない場合は0</returns>
	public static Double GetDouble(SqlDataReader reader, int index)
	{
		if (reader.IsDBNull(index)) { return 0; }
		return reader.GetDouble(index);
	}
}
