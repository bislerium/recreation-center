using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


//Package contains supporting classes, enums and structs for this project.
namespace Backend
{

    //Auth package handles authentication and authorization.
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

        //Class to handle auth process.
        public class Authy
        {
            private readonly string fileSource;
            private Dictionary<int, User> Credentials;
            Random r;

            public Authy(string credentialSource = @"..\..\json\credentials.json")
            {
                fileSource = Path.GetFullPath(credentialSource);
                Credentials = new Dictionary<int, User>();
            }

            //Generate codes of specific length.
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

            //Read credentials from specified source file given through constructor.
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

            //Appends User credential to a credential List and persists it locally at a specified location.
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

            //Returns File Location
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

            //Takes User object to authnticate it by commparing thorugh credentials list.
            public bool IsAuthenticated(User user) => HasUser(user.UserID) && (user.HashedPassword == (Credentials[user.UserID].HashedPassword));
           
        }
    }

    // Used as a value-type to visualize the Ticket pricing process in Win-Form for Staff-clerk
    public class Bill
    {
        public decimal InitialPrice { get; set; }
        public AgeGroupE C_Age { get; set; }
        public decimal C_AgeRate { get; set; }
        public decimal C_AgeRating { get; set; }
        public decimal AfterCAR { get; set; }
        public AgeGroupE Y_Age { get; set; }
        public decimal Y_AgeRate { get; set; }
        public decimal Y_AgeRating { get; set; }
        public decimal AfterYAR { get; set; }
        public AgeGroupE M_Age { get; set; }
        public decimal M_AgeRate { get; set; }
        public decimal M_AgeRating { get; set; }
        public decimal AfterMAR { get; set; }
        public AgeGroupE O_Age { get; set; }
        public decimal O_AgeRate { get; set; }
        public decimal O_AgeRating { get; set; }
        public decimal AfterOAR { get; set; }
        public decimal TotalAgeGroupPrice { get; set; }
        public int Group { get; set; }
        public decimal GroupRate { get; set; }
        public decimal GroupRating { get; set; }
        public decimal AfterGR { get; set; }
        public int DurationHour { get; set; }
        public decimal DurationRate { get; set; }
        public decimal DurationRating { get; set; }
        public decimal AfterDNR { get; set; }
        public DayOfWeek Day { get; set; }
        public decimal DayRate { get; set; }
        public decimal DayRating { get; set; }
        public decimal AfterDYR { get; set; }
        public decimal TotalRating { get; set; }
        public decimal FinalPrice { get; set; }

        public override string ToString()
        {
            return $@"""
                        InitialPrice = {InitialPrice}
                        C_Age = {C_Age},
                        C_AgeRate = {C_AgeRate}
                        C_AgeRating = {C_AgeRating}
                        AfterCAR = {AfterCAR}
                        Y_Age = {Y_Age}
                        Y_AgeRate = {Y_AgeRate}
                        Y_AgeRating = {Y_AgeRating}
                        AfterYAR = {AfterYAR}
                        M_Age = {M_Age}
                        M_AgeRate = {M_AgeRate}
                        M_AgeRating = {M_AgeRating}
                        AfterMAR = {AfterMAR}
                        O_Age = {O_Age}
                        O_AgeRate = {O_AgeRate}
                        O_AgeRating = {O_AgeRating}
                        AfterOAR = {AfterOAR}
                        TotalAgeGroupPrice = {TotalAgeGroupPrice}
                        Group = {Group}
                        GroupRate = {GroupRate}
                        GroupRating = {GroupRating}
                        AfterGR = {AfterGR}
                        DurationHour = {DurationHour}
                        DurationRate = {DurationRate}
                        DurationRating = {DurationRating}
                        AfterDNR = {AfterDNR}
                        Day =  {Day}
                        DayRate = {DayRate}
                        DayRating = {DayRating}
                        AfterDYR = {AfterDYR}
                        FinalPrice = {FinalPrice}
                    """;
        }
    }

    //Represents a range of AgeGroup in ticketing.
    public enum AgeGroupE: Int16
    {
        CHILD = 0,
        YOUNG_ADULT = 17,
        MIDDLE_ADULT = 31,
        OLD_ADULT = 46    
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

        public Bill Bill { get; set; }

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
                    Bill = {Bill}
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

        public (int, decimal, decimal, decimal) GetGroupPricing(short groupOf, decimal basePrice)
        {
            short appropriateGroup = Group.Keys.Aggregate((x, y) => (groupOf >= x && groupOf < y) ? x : y);
            decimal rating = (Group[appropriateGroup]/100) * basePrice;
            return (appropriateGroup, Group[appropriateGroup], rating, basePrice + rating);
        }

        public (int, decimal, decimal, decimal) GetDurationPricing(short durationInHour, decimal basePrice)
        {
            short appropriateDuration = Duration.Keys.Aggregate((x, y) => (durationInHour >= x && durationInHour < y) ? x : y);
            decimal rating = (Duration[appropriateDuration]/100) * basePrice;
            return (appropriateDuration, Duration[appropriateDuration], rating, basePrice + rating);
        }

        public (AgeGroupE, decimal, decimal, decimal) GetAgePricing(short age, decimal basePrice)
        {
            AgeGroupE appropriateAge = Age.Keys.Aggregate((x, y) => (age >= (short)x && age < (short)y) ? x : y);
            decimal rating = (Age[appropriateAge]/100) * basePrice;
            return (appropriateAge, Age[appropriateAge], rating, basePrice + rating);
        }

        public (DayOfWeek, decimal, decimal, decimal) GetDayPricing(DayOfWeek day, decimal basePrice)
        {
            decimal rating = (Day[day]/100) * basePrice;
            return (day, Day[day], rating, basePrice + rating);
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

    //For Ticketting-related Processes.
    public class TicketProcess 
    {
        private Ticket ticket;
        private readonly String fileSource;

        public TicketProcess(string fileSource = @"..\..\json\ticket.json")
        {
            this.fileSource = Path.GetFullPath(fileSource);
            ticket = new Ticket()
            {
                BasePrice = 0,
                Group = new SortedDictionary<short, decimal>() 
                {
                    {0, 0}
                },
                Duration = new SortedDictionary<short, decimal>()
                {
                    {0, 0}
                },
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

        //Read ticket from a specified location given at the time of this object instantiation.
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

        //Write Ticket locally to persist it.
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

        public String getFileSource() =>fileSource;


        //Generates the strucutre of Bill.
        public Bill GenerateBill(Visitor visitor)
        {
            /*(   ,decimal Rate, decimal Rating, decimal RatedPrice)*/

            decimal initialPrice = ticket.BasePrice;
            var cad = ticket.GetAgePricing((short)AgeGroupE.CHILD, initialPrice * visitor.GroupOf[AgeGroupE.CHILD]);
            decimal c_AgeDrate = cad.Item2;
            decimal c_AgeRating = cad.Item3;
            decimal afterCAR = cad.Item4;
            var yad = ticket.GetAgePricing((short)AgeGroupE.YOUNG_ADULT, initialPrice * visitor.GroupOf[AgeGroupE.YOUNG_ADULT]);
            decimal y_AgeDrate = yad.Item2;
            decimal y_AgeRating = yad.Item3;
            decimal afterYAR = yad.Item4;
            var mad = ticket.GetAgePricing((short)AgeGroupE.MIDDLE_ADULT, initialPrice * visitor.GroupOf[AgeGroupE.MIDDLE_ADULT]);
            decimal m_AgeDrate = mad.Item2;
            decimal m_AgeRating = mad.Item3;
            decimal afterMAR = mad.Item4;
            var oad = ticket.GetAgePricing((short)AgeGroupE.OLD_ADULT, initialPrice * visitor.GroupOf[AgeGroupE.OLD_ADULT]);
            decimal o_AgeDrate = oad.Item2;
            decimal o_AgeRating = oad.Item3;
            decimal afterOAR = oad.Item4;
            decimal totalAgeGroupPrice = afterCAR + afterYAR + afterMAR + afterOAR;
            var gd = ticket.GetGroupPricing((short)visitor.GroupOf.Sum(x => x.Value), totalAgeGroupPrice);
            decimal groupDRate = gd.Item2;
            decimal groupRating = gd.Item3;
            decimal afterGR = gd.Item4;
            decimal duration = (decimal)(visitor.OutTime-visitor.InTime)?.TotalHours;
            var dnd = ticket.GetDurationPricing((short)duration, afterGR * duration);
            decimal durationDRate = dnd.Item2;
            decimal durationRating = dnd.Item3;
            decimal afterDNR = dnd.Item4;
            var dyd = ticket.GetDayPricing(visitor.InTime.DayOfWeek, afterDNR);
            decimal dayDRate = dyd.Item2;
            decimal dayRating = dyd.Item3;
            decimal afterDYR = dyd.Item4;
            decimal totalDiscount = c_AgeRating + y_AgeRating + m_AgeRating + o_AgeRating + groupRating + durationRating + dayRating;
            decimal finalPrice = afterDYR;
            return new Bill() {
                InitialPrice = initialPrice,
                C_Age = cad.Item1,
                C_AgeRate = c_AgeDrate,
                C_AgeRating = Math.Round(c_AgeRating, 2),
                AfterCAR = Math.Round(afterCAR, 2),
                Y_Age = yad.Item1,
                Y_AgeRate = y_AgeDrate,
                Y_AgeRating = Math.Round(y_AgeRating, 2),
                AfterYAR = Math.Round(afterYAR, 2),
                M_Age = mad.Item1,
                M_AgeRate = m_AgeDrate,
                M_AgeRating = Math.Round(m_AgeRating, 2),
                AfterMAR = Math.Round(afterMAR, 2),
                O_Age = oad.Item1,
                O_AgeRate = o_AgeDrate,
                O_AgeRating = Math.Round(o_AgeRating, 2),
                AfterOAR = Math.Round(afterOAR, 2),
                TotalAgeGroupPrice = Math.Round(totalAgeGroupPrice, 2),
                Group = gd.Item1,
                GroupRate = groupDRate,
                GroupRating = Math.Round(groupRating, 2),
                AfterGR = Math.Round(afterGR, 2),
                DurationHour = dnd.Item1,
                DurationRate = durationDRate,
                DurationRating = Math.Round(durationRating, 2),
                AfterDNR = Math.Round(afterDNR, 2),
                Day = dyd.Item1,
                DayRate = dayDRate,
                DayRating = Math.Round(dayRating, 2),
                AfterDYR = Math.Round(afterDYR, 2),
                TotalRating = Math.Round(totalDiscount, 2),
                FinalPrice = Math.Round(finalPrice, 2),
            };
        }
    }

    //Performs Visitor-related Processing.
    public class VisitorProcess
    {

        private Dictionary<int, Visitor> visitors;
        private readonly String fileSource;
        private Random random;

        public VisitorProcess(string fileSource = @"..\..\json\visitors.json")
        {
            this.fileSource = Path.GetFullPath(fileSource);
            visitors = new Dictionary<int, Visitor>();
            random = new Random();
        }

        //Used to export visitors dictionary to a specified location.
        public VisitorProcess(string fileSource, Dictionary<int, Visitor> visitors): this(fileSource)
        {
            this.visitors = visitors;
        }

        //Reads Visitors data from a specified filesource.
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
                            foreach (var v in visitors_) {
                                Console.WriteLine(v.Value);
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

        //Write a specific visitor data locally.
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

        //Write visitors dictionary data locally.            
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

        public String GetFileSource() => fileSource;

        public Dictionary<int, Visitor> GetVisitors() => visitors;

        public Visitor GetVisitor(int identifier) => visitors[identifier];

        public bool HasEmptyData() => visitors == null || visitors.Count == 0;

        public bool HasVisitor(int identifier) => visitors.ContainsKey(identifier);
    }
}
