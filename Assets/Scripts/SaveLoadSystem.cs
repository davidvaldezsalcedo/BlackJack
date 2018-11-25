using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
		[SerializeField]
		private ListVariable[] _ListsToSave;
		[SerializeField]
		private IntVariable[] _IntsToSave;
		[SerializeField]
		private DictVariable[] _DictsToSave;

	#region SaveLoadLists
		
		public void SaveLists()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "SavedLists.text");
			
			for (int i = 0; i < _ListsToSave.Length; ++i)
			{
				var json = JsonUtility.ToJson(_ListsToSave[i]);

				formatter.Serialize(file, json);
			}
			file.Close();
		}

		public void LoadList()
		{
			if(File.Exists(Application.persistentDataPath + "SavedLists.text"))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "SavedLists.text", FileMode.Open);

				for (int i = 0; i < _ListsToSave.Length; ++i)
				{
					JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), _ListsToSave[i]);
				}
				file.Close();
			}
			else
			{
				Debug.LogError("File doesn't exist");
			}
		}

	#endregion SaveLoadLists

	#region SaveLoadInts

		public void SaveInts()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "SavedInts.text");

			for (int i = 0; i < _IntsToSave.Length; ++i)
			{
				var json = JsonUtility.ToJson(_IntsToSave[i]);
				formatter.Serialize(file, json);
			}
			file.Close();
		}

	    public void LoadInts()
		{
			if(File.Exists(Application.persistentDataPath + "SavedInts.text"))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "SavedInts.text", FileMode.Open);

				for (int i = 0; i < _IntsToSave.Length; ++i)
				{
					JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), _IntsToSave[i]);
				}
				file.Close();
			}
			else
			{
				Debug.LogError("File doesn't exist");
			}
		}
	#endregion SaveLoadInts

	#region  SaveLoadDicts

		public void SaveDicts()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "SavedDict.text");
			
			for (int i = 0; i < _DictsToSave.Length; ++i)
			{
				var json = JsonUtility.ToJson(_DictsToSave[i]);
				formatter.Serialize(file, json);
			}
			file.Close();
		}

		public void LoadDicts()
		{
			if(File.Exists(Application.persistentDataPath + "SavedDict.text"))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "SavedDict.text", FileMode.Open);

				for (int i = 0; i < _DictsToSave.Length; ++i)
				{
					JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), _DictsToSave[i]);
				}
				file.Close();
			}
			else
			{
				Debug.LogError("File doesn't exist");
			}
		}
	#endregion SaveLoadDicts
}
