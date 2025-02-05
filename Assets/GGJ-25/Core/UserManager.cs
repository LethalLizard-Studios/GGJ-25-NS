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

    [SerializeField] private Renderer fishMesh;
    [SerializeField] private Material[] fishMaterials;

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
        _filePath = Path.Combine(Application.dataPath, "UserData.json");
        LoadUserData();
        DisplayUserData();
    }

    public Material GetFishMaterial()
    {
        return fishMaterials[_fishIndex];
    }

    public string GetUsername()
    {
        if (_loggedInUser == null)
        {
            return "fishy";
        }

        return _loggedInUser.username;
    }

    public int GetPosition(float time)
    {
        _userDataList.users.Sort((a, b) =>
        {
            if (a.bestTime == 0 && b.bestTime == 0) return 0;
            if (a.bestTime == 0) return 1;
            if (b.bestTime == 0) return -1;
            return a.bestTime.CompareTo(b.bestTime);
        });

        for (int i = 0; i < _userDataList.users.Count; i++)
        {
            if (_userDataList.users[i].bestTime == 0 || time < _userDataList.users[i].bestTime)
            {
                return i + 1;
            }
        }

        return _userDataList.users.Count + 1;
    }

    public void AddUser(string username)
    {
        // Check if user already exists in dataList.
        foreach (var user in _userDataList.users)
        {
            if (user.username == username)
            {
                _loggedInUser = user;
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

        SaveUserData();
    }

    public void SelectFish(int index)
    {
        _fishIndex = index;
        fishMesh.material = fishMaterials[_fishIndex];
    }

    public bool UserExists(string username)
    {
        foreach (var user in _userDataList.users)
        {
            if (user.username == username)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateUserInfo(float newTime)
    {
        if (!_userDataList.users.Contains(_loggedInUser))
        {
            return;
        }

        int index = _userDataList.users.IndexOf(_loggedInUser);

        _userDataList.users[index].timesPlayed++;

        if (newTime < _userDataList.users[index].bestTime || _userDataList.users[index].bestTime == 0.0f)
        {
            _userDataList.users[index].bestTime = newTime;
            _userDataList.users[index].bestTimeFishUsed = _fishIndex;
        }

        SaveUserData();
        DisplayUserData();
    }

    private void LoadUserData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _userDataList = JsonUtility.FromJson<UserDataList>(json);
        }
    }

    private void SaveUserData()
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
