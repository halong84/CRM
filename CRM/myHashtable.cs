using System;
using System.Collections;

namespace CRM
{
	/// <summary>
	/// Summary description for myHashtable.
	/// </summary>
	/// 
	#region khai bao Hashtable trong chuong trinh
	public class myHashtable:System.Collections.Hashtable
	{
		public ArrayList uu_tien;
		public myHashtable(){
		  uu_tien=new ArrayList();
		}
		
//		public string getValue(int i)
//		{
//			IDictionaryEnumerator ide=GetEnumerator();
//			int j=Count-1;
//			while(ide.MoveNext())
//			{
//				if(j==i)return (string)ide.Value;
//				j--;
//			}
//			return "";
//		}
//		public string getKey(int i)
//		{
//			IDictionaryEnumerator ide=GetEnumerator();
//			int j=Count-1;
//			while(ide.MoveNext())
//			{
//				if(j==i)return (string)ide.Key;
//			    j--;
//			}
//			return "";
//		}

	}
	#endregion

	public struct myStruct
	{
		public string key;
		public string val;
		public int id;
        public string name;
	}
}
