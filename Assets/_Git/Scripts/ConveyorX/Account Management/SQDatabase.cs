using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ConveyorX.UserAccounts
{
    public class SQDatabase
    {
        private SQLiteConnection connection;

        public SQDatabase(string path)
        {
            if (!File.Exists(path))
            {
                connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
                CreateDB();
                Debug.Log("Database created at: " + path);
            }
            else
            {
                Debug.LogError($"ERROR! A Database already exists on path: {path}");
            }
        }

        public void CreateDB()
        {
            connection.DropTable<User_CX>();
            connection.CreateTable<User_CX>();
        }

        public int AddUser(string username, string password, int i) 
        {
            return connection.Insert(new User_CX { Username = username, hashedPassword = password, index = i + 1 });
        }

        public void RemoveUser(int primaryIndex) 
        {
            //remove the user
            connection.Delete(primaryIndex);
        }

        public User_CX GetUserNamed(int primaryKey) 
        {
            User_CX user = connection.Find<User_CX>(primaryKey) as User_CX;

            if (user != null)
                return user;
            else
            {
                Debug.LogError("ERROR! User is a null and cannot be retrieved! Returning a null");
                return null;
            }
        }
    }
}