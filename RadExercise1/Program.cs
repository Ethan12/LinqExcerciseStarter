using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadExercise1
{
    class Program
    {
       static TestDbContext db = new TestDbContext();
        static void Main(string[] args)
        {

            using (TestDbContext db = new TestDbContext())
            {
                foreach(Club c in db.Clubs )
                {
                    Console.WriteLine(c.Info);
                }
                Console.ReadKey();
            }

            string error;
            if (!db.addMember("ITS FC", new Student { StudentId = Guid.NewGuid(), FirstName = "Ellie" }, out error))
            {
                Console.WriteLine(error);
            }
            else Console.WriteLine(error);

        }

        static List<Club> Question1()
        {
            return db.Clubs.ToList();
        }

        static public List<ClubEvent> Question2(DateTime start, DateTime end)
        {
            //Get the events across all the clubs
            List<ClubEvent> AllClubEvents =
                (List<ClubEvent>)db.Clubs.SelectMany(c =>
                    c.ClubEvents).ToList();
            //Get those events in Range
            return AllClubEvents
                .Where(e => e.StartDateTime >= start &&
                e.EndDateTime <= end).ToList();
        }

        static List<ClubEvent> Question3(string ClubName)
        {
            return db.Clubs.Where(c => c.ClubName == ClubName)
                .SelectMany(c => c.ClubEvents).ToList();
        }

        static public dynamic Question4(string clubName)
        {
            List<Member> clubMembers = db.Clubs.FirstOrDefault(c => c.ClubName == clubName).ClubMembers;

            var NamedMembers = (from clubmember in clubMembers
                       join student in db.Students
                       on clubmember.StudentID equals student.StudentId
                       select new { student.FirstName, student.SecondName });

            return NamedMembers;
        }



    }
}
