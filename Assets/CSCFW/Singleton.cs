using System;

namespace CSCFW
{
	public class Singleton<T> where T : class
	{
		private static T _instance = null;

		private Singleton(){}

		public static void Create()
		{
			if (null == _instance)
			{
				_instance = (T)Activator.CreateInstance(typeof(T), true);
			}
		}

		public static void Destroy()
		{
			_instance = null;
		}

		public static T Instance
		{
			get
			{
				return _instance;
			}
		}
	}
}

