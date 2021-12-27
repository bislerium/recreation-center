using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Collections.Generic.Dictionary<int, decimal>;

namespace Track
{
    public struct Price
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

        public Price(decimal initialPrice, decimal c_AgeDrate, decimal c_AgeDiscount, decimal afterCAD, decimal y_AgeDrate, decimal y_AgeDiscount, decimal afterYAD, decimal m_AgeDrate, decimal m_AgeDiscount, decimal afterMAD, decimal o_AgeDrate, decimal o_AgeDiscount, decimal afterOAD, decimal totalAgeGroupPrice, decimal groupDRate, decimal groupDiscout, decimal afterGD, decimal durationDRate, decimal durationDiscount, decimal afterDND, decimal dayDRate, decimal dayDiscount, decimal afterDYD, decimal finalPrice)
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

    public struct DiscountPrice { 
        public decimal DiscountRate { get; }
        public decimal Discount { get; }
        public decimal DiscountedPrice { get; }
        public DiscountPrice(decimal discountRate, decimal discount, decimal discountedPrice)
        {
            DiscountRate = discountRate;
            Discount = discount;
            DiscountedPrice = discountedPrice;
        }
    }

    public enum AgeGroupE: Int16
    {
        CHILD = 0,
        YOUNG_ADULT = 17,
        MIDDLE_ADULT = 31,
        OLD_ADULT = 45     
    }

    class Ticket
    {
        public int BasePrice { get; set; }

        public SortedDictionary<short, decimal> Group { get; set; }

        public SortedDictionary<short, decimal> Duration { get; set; }

        public SortedDictionary<AgeGroupE, decimal> Age { get; set; }

        public SortedDictionary<DayOfWeek, decimal> Day { get; set; }

        public DiscountPrice GetGroupDiscount(short groupOf, decimal basePrice)
        {
            short appropriateGroup = Group.Keys.Aggregate((x, y) => (groupOf >= x && groupOf < y) ? x : y);
            decimal discount = Group[appropriateGroup] * basePrice;
            return new DiscountPrice(Group[appropriateGroup], discount, basePrice - discount);
        }

        public DiscountPrice GetDurationDiscount(short durationInHour, decimal basePrice)
        {
            short appropriateDuration = Duration.Keys.Aggregate((x, y) => (durationInHour >= x && durationInHour < y) ? x : y);
            decimal discount = Duration[appropriateDuration] * basePrice;
            return new DiscountPrice(Duration[appropriateDuration], discount, basePrice - discount);
        }

        public DiscountPrice GetAgeDiscount(short age, decimal basePrice)
        {
            AgeGroupE appropriateAge = Age.Keys.Aggregate((x, y) => (age >= (short)x && age < (short)y) ? x : y);
            decimal discount = Age[appropriateAge] * basePrice;
            return new DiscountPrice(Age[appropriateAge], discount, basePrice - discount);
        }

        public DiscountPrice GetDayDiscount(DayOfWeek day, decimal basePrice)
        {
            decimal discount = Day[day] * basePrice;
            return new DiscountPrice(Day[day], discount, basePrice - discount);
        }
    }

    class Visitor
    {
        public Guid Identifier { get; set; }

        public String Name { get; set; }

        public string Phone { get; set; }

        public short Age { get; set; }

        public Dictionary<AgeGroupE, short> GroupOf { get; set; }

        public DayOfWeek Day {get; set;}

        public DateTime InTime { get; set; }

        public DateTime OutTime { get; set; }
    }

    class TicketProcess 
    {

        Ticket ticket;
        readonly String fileSource;

        public TicketProcess(string fileSource = "D:\\ticket.json")
        {
            this.fileSource = fileSource;
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

        public bool WriteTicket(Ticket ticket_)
        {
            try
            {
                using (FileStream fs = new FileStream(fileSource, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(sw, ticket_);
                    }
                }
                ticket = ticket_;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }

    class VisitorProcess
    {

        Dictionary<Guid, Visitor> visitors;
        readonly String fileSource;

        public VisitorProcess(string fileSource = "D:\\visitor.json")
        {
            this.fileSource = fileSource;
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
                            Dictionary<Guid, Visitor> visitors_ = (Dictionary<Guid, Visitor>) serializer.Deserialize(sr, typeof(Dictionary<Guid, Visitor>));
                            if (visitors_ == null)
                            {
                                return false;
                            }
                            visitors = visitors_;
                        }
                    }
                    Console.WriteLine(visitors);
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

        public bool WriteCredential(Visitor visitor)
        {
            visitors.Add(visitor.Identifier, visitor);
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
                visitors.Remove(visitor.Identifier);
                return false;
            }
        }

        public Dictionary<Guid, Visitor> GetVisitors() => visitors;

        public bool IsEmpty() => visitors == null || visitors.Count == 0;

        public Visitor GetVisitor(Guid identifier) => visitors[identifier];

        public bool HasVisitor(Guid identifier) => visitors.ContainsKey(identifier);

        public Price? GenerateBill(Guid identifier, Ticket ticket)
        {
            if (IsEmpty() || !HasVisitor(identifier))
            {
                return null;
            }             
            Visitor v = GetVisitor(identifier);
            decimal initialPrice = ticket.BasePrice;
            DiscountPrice cad = ticket.GetAgeDiscount((short) AgeGroupE.CHILD, initialPrice * v.GroupOf[AgeGroupE.CHILD]);
            decimal c_AgeDrate = cad.DiscountRate;
            decimal c_AgeDiscount = cad.Discount;
            decimal afterCAD = cad.DiscountedPrice; 
            DiscountPrice yad = ticket.GetAgeDiscount((short) AgeGroupE.YOUNG_ADULT, initialPrice * v.GroupOf[AgeGroupE.YOUNG_ADULT]);
            decimal y_AgeDrate = yad.DiscountRate;
            decimal y_AgeDiscount = yad.Discount;
            decimal afterYAD = yad.DiscountedPrice;
            DiscountPrice mad = ticket.GetAgeDiscount((short) AgeGroupE.MIDDLE_ADULT, initialPrice * v.GroupOf[AgeGroupE.MIDDLE_ADULT]);
            decimal m_AgeDrate = mad.DiscountRate;
            decimal m_AgeDiscount = mad.Discount;
            decimal afterMAD = mad.DiscountedPrice;
            DiscountPrice oad = ticket.GetAgeDiscount((short) AgeGroupE.OLD_ADULT, initialPrice * v.GroupOf[AgeGroupE.OLD_ADULT]);
            decimal o_AgeDrate = oad.DiscountRate;
            decimal o_AgeDiscount = oad.Discount;
            decimal afterOAD = oad.DiscountedPrice;
            decimal totalAgeGroupPrice = afterCAD + afterYAD + afterMAD + afterOAD;
            DiscountPrice gd = ticket.GetGroupDiscount((short)v.GroupOf.Sum(x => x.Value), totalAgeGroupPrice);
            decimal groupDRate = gd.DiscountRate;
            decimal groupDiscout = gd.Discount;
            decimal afterGD = gd.DiscountedPrice;
            decimal duration = (decimal)(v.OutTime - v.InTime).TotalHours;
            DiscountPrice dnd = ticket.GetDurationDiscount((short) duration, afterGD*duration);
            decimal durationDRate = dnd.DiscountRate;
            decimal durationDiscount = dnd.Discount;
            decimal afterDND = dnd.DiscountedPrice;
            DiscountPrice dyd = ticket.GetDayDiscount(v.InTime.DayOfWeek, afterDND);
            decimal dayDRate = dyd.DiscountRate;
            decimal dayDiscount = dyd.Discount;
            decimal afterDYD = dyd.DiscountedPrice;
            decimal finalPrice = afterDYD;
            return new Price(
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
    }
}
