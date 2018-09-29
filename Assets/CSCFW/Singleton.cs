using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CSCFW
{
	public class Singleton<T> where T : class
	{
		private static T _instance = null;

		static Singleton(){}

		public static void Create()
		{
			if (null == _instance)
			{
				_instance = Activator.CreateInstance<T>();
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

