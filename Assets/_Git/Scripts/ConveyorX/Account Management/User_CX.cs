using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

namespace ConveyorX.UserAccounts
{
    public class User_CX
    {
        [PrimaryKey, AutoIncrement]
        public int index { get; set; }

        //User data!
        public string Username;
        public string hashedPassword;

        //add additional fields if needed!
    }
}