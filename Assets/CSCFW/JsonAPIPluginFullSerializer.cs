using FullSerializer;
using System;

namespace CSCFW
{
	public static class JsonAPIPluginFullSerializer
	{
		private static readonly fsSerializer _serializer = new fsSerializer();


		public static string ToJsonString<T>(T value)
		{
			// serialize the data
			fsData data;
			_serializer.TrySerialize<T>(value, out data).AssertSuccessWithoutWarnings();

			// emit the data via JSON
			return fsJsonPrinter.CompressedJson(data);
		}

		public static T FromJsonString<T>(string jsonString)
		{
			// step 1: parse the JSON data
			fsData data = fsJsonParser.Parse(jsonString);

			// step 2: deserialize the data
			T deserialized = default(T);
			_serializer.TryDeserialize<T>(data, ref deserialized).AssertSuccessWithoutWarnings();

			return deserialized;
		}
	}
}
