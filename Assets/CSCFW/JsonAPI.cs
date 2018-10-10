using UnityEngine;

namespace CSCFW
{
	public static class JsonAPI
	{
		#region public
		public static T LoadFromFile<T>(string path) 
		{
			try
			{
				//TODO change this after ResourceManager is finished
				var jsonAsset = Resources.Load<TextAsset>(path);
				var jsonString = jsonAsset.text;
				Resources.UnloadAsset(jsonAsset);

				return LoadFromString<T>(jsonString);
			}
			catch
			{
				//TODO change this after LogManager is finished
				Debug.LogError("JsonAPI LoadFromFile: " + path + " Type: " + typeof(T));
				throw;
			}
		}

		public static void ObjectToFile<T>(T obj, string path)
		{

		}
		#endregion

		#region private
		private static T LoadFromString<T>(string jsonString)
		{
			return JsonAPIPluginFullSerializer.FromJsonString<T>(jsonString);
		}
		#endregion
	}
}
