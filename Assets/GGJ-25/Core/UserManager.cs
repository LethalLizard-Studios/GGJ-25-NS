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
    private UserData _loggedInUser;
    private int _fishIndex = 0;

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "UserData.json");
        LoadUserData();
        DisplayUserData();
    }

    public void AddUser(string username)
    {
        // Check if user already exists in dataList.
        foreach (var user in _userDataList.users)
        {
            if (user.username == username)
            {
                return;
            }
        }

        UserData newUser = new UserData
        {
            username = username,
            bestTime = 0f,
            bestTimeFishUsed = 0,
            timesPlayed = 0
        };

        _loggedInUser = newUser;
        _userDataList.users.Add(newUser);

        SavePlayerData();
    }

    public void SelectFish(int index)
    {
        _fishIndex = index;
    }

    public void UpdateUserInfo(float newTime)
    {
        if (!_userDataList.users.Contains(_loggedInUser))
        {
            return;
        }

        int index = _userDataList.users.IndexOf(_loggedInUser);

        _userDataList.users[index].timesPlayed++;

        if (newTime < _userDataList.users[index].bestTime)
        {
            _userDataList.users[index].bestTime = newTime;
            _userDataList.users[index].bestTimeFishUsed = _fishIndex;
        }
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
        _userDataList.users.Sort((a, b) =>
        {
            if (a.bestTime == 0 && b.bestTime == 0) return 0;
            if (a.bestTime == 0) return 1;
            if (b.bestTime == 0) return -1;
            return a.bestTime.CompareTo(b.bestTime);
        });

        for (int i = 0; i < _entries.Count; i++)
        {
            Destroy(_entries[i]);
        }
        _entries.Clear();

        for (int i = 0; i < _userDataList.users.Count; i++)
        {
            UserData data = _userDataList.users[i];
            GameObject userObject = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            userObject.GetComponent<EntryView>().Initialize(data.username, data.bestTime, 
                i + 1, data.bestTimeFishUsed);
            _entries.Add(userObject);
        }
    }
}
