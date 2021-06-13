using System.IO;
using System.Threading.Tasks;

namespace System.Text.Json
{
	public static class JsonSerialization
	{
		public static string Serialize<T>(this T toSerialize, bool prettyPrint = false) => 
			JsonSerializer.Serialize<T>(
				toSerialize, 
				new JsonSerializerOptions { 
					WriteIndented = prettyPrint });

		public static T Deserialize<T>(this string toDeserialize) => 
			JsonSerializer.Deserialize<T>(toDeserialize);

		public static async Task<T> DeserializeFileAsync<T>(string pathAndFile)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = File.OpenRead(pathAndFile);
				return
					await JsonSerializer
						.DeserializeAsync<T>(fileStream);
			}
			catch
			{
				throw;
			}
			finally
			{
				fileStream?.Dispose();
			}
		}

		public static T DeserializeFile<T>(string pathAndFile)
		{
			try
			{
				string jsonFile = File.ReadAllText(pathAndFile);
				T result =
					JsonSerializer.Deserialize<T>(jsonFile);
				return result;
			}
			catch
			{
				throw;
			}
		}

	}
}
