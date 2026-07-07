using System;
using System.Collections.Generic;
using System.IO;
using GymManagement.Classes;

namespace GymManagement.Classes
{
    public class FileHandler
    {
        private string filePath;
        private string lockedFilePath;

        public FileHandler()
        {
            // store in application directory
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.txt");
            lockedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "locked_accounts.txt");

            // create default admin if file doesnt exist
            if (!File.Exists(filePath))
            {
                WriteUser("admin", "admin123");
                WriteUser("staff1", "pass123");
            }

            if (!File.Exists(lockedFilePath))
            {
                File.Create(lockedFilePath).Close();
            }
        }

        // reads all users from file
        public List<string[]> ReadAllUsers()
        {
            List<string[]> users = new List<string[]>();

            try
            {
                if (!File.Exists(filePath))
                    return users;

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            users.Add(parts);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                throw new DatabaseException("Could not read users file: " + ex.Message);
            }

            return users;
        }

        // write a new user to text file
        public void WriteUser(string username, string password)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(username + "," + password);
                }
            }
            catch (IOException ex)
            {
                throw new DatabaseException("Could not write to users file: " + ex.Message);
            }
        }

        // check if login is valid
        public bool ValidateLogin(string username, string password)
        {
            List<string[]> users = ReadAllUsers();

            foreach (string[] user in users)
            {
                if (user[0] == username && user[1] == password)
                    return true;
            }

            return false;
        }

        // check if a username already exists
        public bool UserExists(string username)
        {
            List<string[]> users = ReadAllUsers();
            foreach (string[] user in users)
            {
                if (user[0] == username)
                    return true;
            }
            return false;
        }

        // lock an account
        public void LockAccount(string username)
        {
            try
            {
                // read locked accounts
                List<string> locked = new List<string>();
                if (File.Exists(lockedFilePath))
                {
                    locked.AddRange(File.ReadAllLines(lockedFilePath));
                }

                if (!locked.Contains(username))
                {
                    locked.Add(username);
                    File.WriteAllLines(lockedFilePath, locked);
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error locking account: " + ex.Message);
            }
        }

        // unlock account - admin only
        public void UnlockAccount(string username)
        {
            try
            {
                if (!File.Exists(lockedFilePath))
                    return;

                List<string> locked = new List<string>(File.ReadAllLines(lockedFilePath));
                locked.Remove(username);
                File.WriteAllLines(lockedFilePath, locked);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error unlocking account: " + ex.Message);
            }
        }

        public bool IsAccountLocked(string username)
        {
            try
            {
                if (!File.Exists(lockedFilePath))
                    return false;

                string[] locked = File.ReadAllLines(lockedFilePath);
                foreach (string name in locked)
                {
                    if (name == username)
                        return true;
                }
            }
            catch
            {
                // if we cant read, assume not locked
            }

            return false;
        }

        // get all locked accounts
        public List<string> GetLockedAccounts()
        {
            List<string> locked = new List<string>();
            try
            {
                if (File.Exists(lockedFilePath))
                {
                    locked.AddRange(File.ReadAllLines(lockedFilePath));
                }
            }
            catch { }

            return locked;
        }
    }
}
