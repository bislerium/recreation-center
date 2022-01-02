using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Collections.Generic.Dictionary<int, decimal>;

namespace Backend
{
    namespace Auth
    {
        public enum UserGroup { 
            Admin,
            CheckinStaff,
            CheckoutStaff,
        }

        public class User
        {
            public int UserID { get; set; }

            public string UserName { get; set; }

            public int HashedPassword { get; set; }

            public UserGroup UserGroup { get; set; }

            public int HashedRecoveryCode { get; set; }

            public override string ToString()
            {
                return $"{UserID}, {UserName}, {HashedPassword}, {UserGroup}, {HashedRecoveryCode}";
            }
        }

        public class Auth
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
                    char a = (char)r.Next(48, 123);
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

            public String getFileSource() => fileSource;

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

    // Used as a value-type to visualize the Ticket pricing process in Win-Form for Staff-clerk
    public struct Bill
    {
        public decimal InitialPrice { get; }
        public decimal C_AgeDrate { get; }
        public decimal C_AgeDiscount { get; }
        public decimal AfterCAD { get; }
        public decimal Y_AgeDrate { get; }
        public decimal Y_AgeDiscount { get; }
        public decimal AfterYAD { get; }
        public decimal M_AgeDrate { get; }
        public decimal M_AgeDiscount { get; }
        public decimal AfterMAD { get; }
        public decimal O_AgeDrate { get; }
        public decimal O_AgeDiscount { get; }
        public decimal AfterOAD { get; }
        public decimal TotalAgeGroupPrice { get; }
        public decimal GroupDRate { get; }
        public decimal GroupDiscout { get; }
        public decimal AfterGD { get; }
        public decimal DurationDRate { get; }
        public decimal DurationDiscount { get; }
        public decimal AfterDND { get; }
        public decimal DayDRate { get; }
        public decimal DayDiscount { get; }
        public decimal AfterDYD { get; }
        public decimal FinalPrice { get; }

        public Bill(decimal initialPrice,
            decimal c_AgeDrate,
            decimal c_AgeDiscount,
            decimal afterCAD,
            decimal y_AgeDrate,
            decimal y_AgeDiscount,
            decimal afterYAD,
            decimal m_AgeDrate,
            decimal m_AgeDiscount,
            decimal afterMAD,
            decimal o_AgeDrate,
            decimal o_AgeDiscount,
            decimal afterOAD,
            decimal totalAgeGroupPrice,
            decimal groupDRate,
            decimal groupDiscout,
            decimal afterGD,
            decimal durationDRate,
            decimal durationDiscount,
            decimal afterDND,
            decimal dayDRate,
            decimal dayDiscount,
            decimal afterDYD,
            decimal finalPrice)
        {
            InitialPrice = initialPrice;
            C_AgeDrate = c_AgeDrate;
            C_AgeDiscount = c_AgeDiscount;
            AfterCAD = afterCAD;
            Y_AgeDrate = y_AgeDrate;
            Y_AgeDiscount = y_AgeDiscount;
            AfterYAD = afterYAD;
            M_AgeDrate = m_AgeDrate;
            M_AgeDiscount = m_AgeDiscount;
            AfterMAD = afterMAD;
            O_AgeDrate = o_AgeDrate;
            O_AgeDiscount = o_AgeDiscount;
            AfterOAD = afterOAD;
            TotalAgeGroupPrice = totalAgeGroupPrice;
            GroupDRate = groupDRate;
            GroupDiscout = groupDiscout;
            AfterGD = afterGD;
            DurationDRate = durationDRate;
            DurationDiscount = durationDiscount;
            AfterDND = afterDND;
            DayDRate = dayDRate;
            DayDiscount = dayDiscount;
            AfterDYD = afterDYD;
            FinalPrice = finalPrice;
        }
    }

    public struct Price { 
        public decimal Rate { get; }
        public decimal Rating { get; }
        public decimal RatedPrice { get; }
        public Price(decimal Rate,
            decimal Rating,
            decimal RatedPrice)
        {
            this.Rate = Rate;
            this.Rating = Rating;
            this.RatedPrice = RatedPrice;
        }
    }

    public enum AgeGroupE: Int16
    {
        CHILD = 0,
        YOUNG_ADULT = 17,
        MIDDLE_ADULT = 31,
        OLD_ADULT = 45     
    }

    public class Visitor
    {
        public int TicketCode { get; set; }

        public String Name { get; set; }

        public string Phone { get; set; }

        public short Age { get; set; }

        public Dictionary<AgeGroupE, short> GroupOf { get; set; }

        public DayOfWeek Day { get; set; }

        public DateTime InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public Bill? Bill { get; set; }

        public override string ToString()
        {
            return $@"
                    id = { TicketCode},
                    name = { Name },
                    phone = { Phone },
                    age = { Age },
                    groupof = { string.Join( " | ", GroupOf.Select(x => x.Key + " : " + x.Value).ToList()) },
                    day = { Day },
                    intime = { InTime },
                    outTime = { OutTime },
                    Price = {Bill}
                ";
        }
    }

    public class Ticket
    {
        public int BasePrice { get; set; }

        public SortedDictionary<short, decimal> Group { get; set; }

        public SortedDictionary<short, decimal> Duration { get; set; }

        public SortedDictionary<AgeGroupE, decimal> Age { get; set; }

        public SortedDictionary<DayOfWeek, decimal> Day { get; set; }

        public Price GetGroupDiscount(short groupOf,
            decimal basePrice)
        {
            short appropriateGroup = Group.Keys.Aggregate((x, y) => (groupOf >= x && groupOf < y) ? x : y);
            decimal discount = Group[appropriateGroup] * basePrice;
            return new Price(Group[appropriateGroup], discount, basePrice - discount);
        }

        public Price GetDurationDiscount(short durationInHour, decimal basePrice)
        {
            short appropriateDuration = Duration.Keys.Aggregate((x, y) => (durationInHour >= x && durationInHour < y) ? x : y);
            decimal discount = Duration[appropriateDuration] * basePrice;
            return new Price(Duration[appropriateDuration], discount, basePrice - discount);
        }

        public Price GetAgeDiscount(short age, decimal basePrice)
        {
            AgeGroupE appropriateAge = Age.Keys.Aggregate((x, y) => (age >= (short)x && age < (short)y) ? x : y);
            decimal discount = Age[appropriateAge] * basePrice;
            return new Price(Age[appropriateAge], discount, basePrice - discount);
        }

        public Price GetDayDiscount(DayOfWeek day, decimal basePrice)
        {
            decimal discount = Day[day] * basePrice;
            return new Price(Day[day], discount, basePrice - discount);
        }

        public override string ToString()
        {
            return $@"
                    Base Price = { BasePrice },
                    Group = { string.Join(" | ", Group.Select(x => x.Key + " : " + x.Value).ToList()) },
                    Duration = { string.Join(" | ", Duration.Select(x => x.Key + " : " + x.Value).ToList()) },
                    Age = { string.Join(" | ", Age.Select(x => x.Key.ToString() + " : " + x.Value).ToList()) },
                    Day = { string.Join(" | ", Day.Select(x => x.Key.ToString() + " : " + x.Value).ToList()) },
                ";
        }
    }

    public class TicketProcess 
    {

        private Ticket ticket;
        private readonly String fileSource;

        public TicketProcess(string fileSource = "D:\\ticket.json")
        {
            this.fileSource = fileSource;
            ticket = new Ticket()
            {
                BasePrice = 0,
                Group = new SortedDictionary<short, decimal>(),
                Duration = new SortedDictionary<short, decimal>(),
                Age = new SortedDictionary<AgeGroupE, decimal>()
                {
                    {AgeGroupE.CHILD, 0 },
                    {AgeGroupE.YOUNG_ADULT, 0 },
                    {AgeGroupE.MIDDLE_ADULT, 0 },
                    {AgeGroupE.OLD_ADULT, 0 },
                },
                Day = new SortedDictionary<DayOfWeek, decimal>()
                {
                    {DayOfWeek.Sunday, 0 },
                    {DayOfWeek.Monday, 0 },
                    {DayOfWeek.Tuesday, 0 },
                    {DayOfWeek.Wednesday, 0 },
                    {DayOfWeek.Thursday, 0 },
                    {DayOfWeek.Friday, 0 },
                    {DayOfWeek.Saturday, 0 },
                },
            };
        }

        // For writing a specific ticket to a specific location
        public TicketProcess(string fileSource, Ticket ticket) : this(fileSource)
        {
            this.ticket = ticket;
        }

        public bool ReadTicket()
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
                            Ticket ticket_ = (Ticket)serializer.Deserialize(sr, typeof(Ticket));
                            if (ticket_ == null)
                            {
                                return false;
                            }
                            ticket = ticket_;
                        }
                    }
                    Console.WriteLine(ticket);
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

        public bool WriteTicket()
        {
            try
            {
                using (FileStream fs = new FileStream(fileSource, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(sw, ticket);
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

        public Ticket GetTicket() => ticket;

        public String getFileSource() => fileSource;

        public Bill GenerateBill(Visitor visitor)
        {
            decimal initialPrice = ticket.BasePrice;
            Price cad = ticket.GetAgeDiscount((short)AgeGroupE.CHILD, initialPrice * visitor.GroupOf[AgeGroupE.CHILD]);
            decimal c_AgeDrate = cad.Rate;
            decimal c_AgeDiscount = cad.Rating;
            decimal afterCAD = cad.RatedPrice;
            Price yad = ticket.GetAgeDiscount((short)AgeGroupE.YOUNG_ADULT, initialPrice * visitor.GroupOf[AgeGroupE.YOUNG_ADULT]);
            decimal y_AgeDrate = yad.Rate;
            decimal y_AgeDiscount = yad.Rating;
            decimal afterYAD = yad.RatedPrice;
            Price mad = ticket.GetAgeDiscount((short)AgeGroupE.MIDDLE_ADULT, initialPrice * visitor.GroupOf[AgeGroupE.MIDDLE_ADULT]);
            decimal m_AgeDrate = mad.Rate;
            decimal m_AgeDiscount = mad.Rating;
            decimal afterMAD = mad.RatedPrice;
            Price oad = ticket.GetAgeDiscount((short)AgeGroupE.OLD_ADULT, initialPrice * visitor.GroupOf[AgeGroupE.OLD_ADULT]);
            decimal o_AgeDrate = oad.Rate;
            decimal o_AgeDiscount = oad.Rating;
            decimal afterOAD = oad.RatedPrice;
            decimal totalAgeGroupPrice = afterCAD + afterYAD + afterMAD + afterOAD;
            Price gd = ticket.GetGroupDiscount((short)visitor.GroupOf.Sum(x => x.Value), totalAgeGroupPrice);
            decimal groupDRate = gd.Rate;
            decimal groupDiscout = gd.Rating;
            decimal afterGD = gd.RatedPrice;
            decimal duration = (decimal)(visitor.OutTime - visitor.InTime)?.TotalHours;
            Price dnd = ticket.GetDurationDiscount((short)duration, afterGD * duration);
            decimal durationDRate = dnd.Rate;
            decimal durationDiscount = dnd.Rating;
            decimal afterDND = dnd.RatedPrice;
            Price dyd = ticket.GetDayDiscount(visitor.InTime.DayOfWeek, afterDND);
            decimal dayDRate = dyd.Rate;
            decimal dayDiscount = dyd.Rating;
            decimal afterDYD = dyd.RatedPrice;
            decimal finalPrice = afterDYD;
            return new Bill(
                initialPrice,
                c_AgeDrate,
                c_AgeDiscount,
                afterCAD,
                y_AgeDrate,
                y_AgeDiscount,
                afterYAD,
                m_AgeDrate,
                m_AgeDiscount,
                afterMAD,
                o_AgeDrate,
                o_AgeDiscount,
                afterOAD,
                totalAgeGroupPrice,
                groupDRate,
                groupDiscout,
                afterGD,
                durationDRate,
                durationDiscount,
                afterDND,
                dayDRate,
                dayDiscount,
                afterDYD,
                finalPrice
            );
        }
    }

    public class VisitorProcess
    {

        private Dictionary<int, Visitor> visitors;
        private readonly String fileSource;
        private Random random;

        public VisitorProcess(string fileSource = "D:\\visitors.json")
        {
            this.fileSource = fileSource;
            visitors = new Dictionary<int, Visitor>();
            random = new Random();
        }
        public VisitorProcess(string fileSource, Dictionary<int, Visitor> visitors): this(fileSource)
        {
            this.visitors = visitors;
        }

        public bool ReadVisitors()
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
                            Dictionary<int, Visitor> visitors_ = (Dictionary<int, Visitor>) serializer.Deserialize(sr, typeof(Dictionary<int, Visitor>));
                            if (visitors_ == null)
                            {
                                return false;
                            }
                            visitors = visitors_;
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

        public bool WriteVisitor(Visitor visitor)
        { 
            visitors[visitor.TicketCode] = visitor;
            bool success = WriteVisitors();
            if (!success)
            {
                visitors.Remove(visitor.TicketCode);
            }
            return success;
        }
            
        public bool WriteVisitors()
        {
            try
            {
                using (FileStream fs = new FileStream(fileSource, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(sw, visitors);
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

        public int GenerateID(int length = 10)
        {
            var min = (int)Math.Pow(10, length - 1);
            var max = (int)Math.Pow(10, length) - 1;
            while (true)
            {                
                int id = random.Next(min, max);
                if (!HasVisitor(id)) return id;
            }            
        }

        public String getFileSource() => fileSource;

        public Dictionary<int, Visitor> GetVisitors() => visitors;

        public bool IsEmpty() => visitors == null || visitors.Count == 0;

        public Visitor GetVisitor(int identifier) => visitors[identifier];

        public bool HasVisitor(int identifier) => visitors.ContainsKey(identifier);

        
        public bool GenerateDailyReport()
        {
            if (IsEmpty()) {
                return false;
            }
            return true;
        }

        public bool GenerateWeeklyReport()
        {
            if (IsEmpty())
            {
                return false;
            }
            return true;
        }

/*        static void Main(string[] args)
        {
            VisitorProcess vs = new VisitorProcess();
            Console.WriteLine(vs.ReadVisitors());
            Visitor v = new Visitor()
            {
                TicketCode = vs.GenerateID(),
                Name = "bishal",
                Phone = "97798007465839",
                Age = 34,
                GroupOf = new Dictionary<AgeGroupE, short>() {
                    { AgeGroupE.MIDDLE_ADULT, 3 },
                    { AgeGroupE.CHILD, 2 }
                },
                Day = DayOfWeek.Sunday,
                InTime = DateTime.Now,
            };
            Console.WriteLine(v.OutTime);
            Console.WriteLine(v.OutTime == null);
            vs.GetVisitors().Select(a => a.Key + " " + a.Value).ToList().ForEach(Console.WriteLine);
*//*            if (vs.IsEmpty()) vs.Initialize(); *//*
            Console.WriteLine(vs.WriteVisitors(v));
        }*/
    }
}
