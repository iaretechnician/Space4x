using ConveyorX.UserAccounts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyorX.UI
{
    //This is client-side user account management communication class and should not be useful on server-side!
    public class AccountManager : MonoBehaviour
    {
        private string username, password;

        //use with Input Fields
        public void ReadUsername(string value)
        {
            if (string.IsNullOrEmpty(value) == false) username = value;
        }

        //use with Input Fields
        public void ReadPassword(string value)
        {
            if (string.IsNullOrEmpty(value) == false) password = value;
        }

        public void Login()
        {
            Debug.Log("Logging in...");

            //send data to server to validate

            //login after validation...
            OnUserValidated();
        }

        public void SignUp()
        {
            Debug.Log("Signing up...");

            //send data to server to create an account for this user

            //save an UniqueKey for this user to communicate with server and request account access

            OnUserValidated();
        }

        private void OnUserValidated() 
        {
            Debug.Log("User validated!");
        }
    }
}