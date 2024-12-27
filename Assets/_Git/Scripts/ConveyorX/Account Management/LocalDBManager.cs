using UnityEngine;
using System.Data;
using SQLite4Unity3d;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConveyorX.UserAccounts
{
    public class LocalDBManager : MonoBehaviour
    {
        private string databasePath;

        private Dictionary<string, int> ExistingUsernames = new();
        private SQDatabase activeDatabase;

        private void Start()
        {
            databasePath = "URI=file:" + Application.persistentDataPath + "/UsersDatabase.db";
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            activeDatabase = new SQDatabase(databasePath);
        }

        public void AddUser(string username, string password) 
        {
            if (ExistingUsernames.ContainsKey(username))
            {
                Debug.LogError($"ERROR! Username {username} already exists in database!");
                return;
            }

            int index = ExistingUsernames.Count;
            ExistingUsernames.Add(username, index);
            string hashedPassword = HashString(password);
            activeDatabase.AddUser(username, hashedPassword, index);
        }

        public void RemoveUser(string username) 
        {
            if (ExistingUsernames.ContainsKey(username))
            {
                ExistingUsernames.Remove(username, out int pKey);
                activeDatabase.RemoveUser(pKey);
            }
            else 
            {
                Debug.LogError($"ERROR! Username {username} does not exist in database!");
            }
        }

        public bool FindUserNamed(string username) 
        {
            return ExistingUsernames.ContainsKey(username);
        }

        public User_CX GetUserNamed(string username) 
        {
            ExistingUsernames.TryGetValue(username, out int pk);
            return activeDatabase.GetUserNamed(pk);
        }

        public bool ValidateUser(string username, string password) 
        {
            ExistingUsernames.TryGetValue(username, out int pk);
            User_CX user = activeDatabase.GetUserNamed(pk);

            if (user != null) 
            {
                bool match = VerifyHash(password, user.hashedPassword);
                Debug.Log("User exsists, validating...");
                return match;
            }

            Debug.Log("User does not exist, cannot compare!");
            return false;
        }

        private string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // "x2" formats bytes as lowercase hexadecimal
                }

                return builder.ToString();
            }
        }

        private bool VerifyHash(string textToCompare, string hashToCompare)
        {
            string inputHash = HashString(textToCompare);
            return string.Equals(inputHash, hashToCompare, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}