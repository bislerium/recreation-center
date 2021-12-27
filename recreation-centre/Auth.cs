using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Auth
{
    sealed class User
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public int HashedPassword { get; set; }

        public bool IsAdmin { get; set; }

        public int HashedRecoveryCode { get; set; }

        public override string ToString()
        {
            return $"{UserID}, {UserName}, {HashedPassword}, {IsAdmin}, {HashedRecoveryCode}";
        }
    }

    class Auth
    {
        private readonly string fileSource;
        private Dictionary<int, User> Credentials;
        Random r;

        public Auth(string credentialSource = "D:\\credentials.json")
        {
            fileSource = credentialSource;
            Credentials = new Dictionary<int, User>();
        }

        public string GenerateRecoveryCode(int length)
        {
            r = new Random();
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char a = (char) r.Next(48,123);
                sb.Append(a);
            }
            return sb.ToString();
        }

        public bool ReadCredential()
        {
            if (File.Exists(fileSource))
            {
                try
                {
                    using (FileStream fs = new FileStream(fileSource, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            Dictionary<int, User> credentials = (Dictionary<int, User>)serializer.Deserialize(sr, typeof(Dictionary<int, User>));
                            if (credentials == null)
                            {
                                return false;
                            }
                            Credentials = credentials;
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool WriteCredential(User user)
        {
            if (HasUser(user.UserID))
            {
                return false;
            }
            Credentials.Add(user.UserID, user);
            try
            {
                using (FileStream fs = new FileStream(fileSource, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(sw, Credentials);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Credentials.Remove(user.UserID);
                return false;
            }
        }

        public Dictionary<int, User> GetCredentials() => Credentials;

        public bool HasUser(int userID) => Credentials.ContainsKey(userID);

        public User GetUser(int userID) => Credentials[userID];

        public bool VerifyReset(int id, string recoveryCode) => HasUser(id) && (Credentials[id].HashedRecoveryCode == recoveryCode.GetHashCode());

        public string ResetPassword(int id, string recoveryCode, string password)
        {
            if (VerifyReset(id, recoveryCode))
            {
                string newRecoveryCode = GenerateRecoveryCode(16);
                Credentials[id].HashedPassword = password.GetHashCode();
                Credentials[id].HashedRecoveryCode = newRecoveryCode.GetHashCode();
                return newRecoveryCode;
            }
            return null;
        }

        public bool IsAuthenticated(User user) => HasUser(user.UserID) && (user.HashedPassword == (Credentials[user.UserID].HashedPassword));

        public static void Main(string[] args)
        {
            Auth a = new Auth();
            Console.WriteLine(a.GenerateRecoveryCode(16));
            string aa = a.GenerateRecoveryCode(16);
            string bb = a.GenerateRecoveryCode(16);
            Console.WriteLine(aa);
            Console.WriteLine(bb);
            User u = new User
            {
                UserID = 232342,
                UserName = "bishal gc",
                HashedPassword = "apple321".GetHashCode(),
                HashedRecoveryCode = aa.GetHashCode()
            };
            User uu = new User
            {
                UserID = 487383,
                UserName = "Nigga por",
                HashedPassword = "cmjcnn938uc".GetHashCode(),
                HashedRecoveryCode = bb.GetHashCode()
            };
            if (!a.ReadCredential())
            {
                Console.WriteLine("added");
                Console.WriteLine(a.WriteCredential(u));
                Console.WriteLine(a.WriteCredential(uu));
                Console.WriteLine(a.WriteCredential(uu));
            }
            Console.WriteLine("user password -> " + u.HashedPassword);
            Console.WriteLine(a.HasUser(u.UserID));
            Console.WriteLine(a.GetUser(u.UserID));
            Console.WriteLine("verify -> " + a.VerifyReset(u.UserID, "?5vwzA@002>UPl>u"));
            Console.WriteLine("reset -> " + a.ResetPassword(u.UserID, "?5vwzA@002>UPl>u", "hello there"));
            Console.WriteLine(a.GetUser(u.UserID));
            Console.WriteLine("authenticated -> " + a.IsAuthenticated(u));
            Console.WriteLine(a.Credentials.Count);

            foreach (var kv in a.Credentials)
            {
                Console.Write(kv.Key + " -> ");
                Console.WriteLine(kv.Value);
            }
        }
    }
}
