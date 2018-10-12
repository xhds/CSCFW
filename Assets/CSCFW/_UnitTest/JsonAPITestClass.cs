using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

namespace CSCFW
{
	public class ClassA
	{
		public int a = 0;
		public float b = 0.0f;
		public bool c = false;
		public string d = "";
		public List<int> e = new List<int>();
		public Dictionary<string, string> f = new Dictionary<string, string>();
		public ClassB g;
		public Vector3 h;
	}
	
	public class ClassB
	{
		public int a;
		public float b;
	}

	[CreateAssetMenu(menuName = "CSCFW/CreateTestC")]
	public class ClassC
	{
		public float x = 1f;
		public float y = 2f;
		public float z = 3f;
	}
}