/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/25/2025
*/

using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    [SerializeField] private Transform leaderboardContent;
    [SerializeField] private GameObject leaderboardEntryPrefab;

    [System.Serializable]
    public class UserDataList
    {
        public List<UserData> users = new List<UserData>();
    }

    private UserDataList _userDataList = new UserDataList();
    private string _filePath;
    private List<GameObject> _entries = new List<GameObject>();

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "UserData.json");
        LoadUserData();
    }

    private void LoadUserData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _userDataList = JsonUtility.FromJson<UserDataList>(json);
        }
    }

    private void SavePlayerData()
    {
        string json = JsonUtility.ToJson(_userDataList, true);
        File.WriteAllText(_filePath, json);
    }

    public void DisplayUserData()
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            Destroy(_entries[i]);
        }
        _entries.Clear();

        for (int i = 0; i < _userDataList.users.Count; i++)
        {
            _entries.Add(Instantiate(leaderboardEntryPrefab, leaderboardContent));
        }
    }
}
