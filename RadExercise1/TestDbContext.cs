using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RadExercise1
{

    public class Student
    {
        public Guid StudentId;
        public string FirstName;
        public string SecondName;
    

        public static Student FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Student ImportedStudentRecord = new Student();
            ImportedStudentRecord.StudentId = Guid.NewGuid();
            ImportedStudentRecord.FirstName = values[0];
            ImportedStudentRecord.SecondName = values[1];
            return ImportedStudentRecord;
        }
    }
    // Implement IDisposable to allow using 
    class TestDbContext : IDisposable
    {
        private bool disposed = false;
        public List<Student> Students = new List<Student>();
        // Create a set of Clubs

        // Make a random list of students
        public List<Club> Clubs;
        public TestDbContext()
        {

              Students = File.ReadAllLines(@"random Names.csv")
                                           //.Skip(1) // Only needed if the first row contains the Field names
                                           .Select(v => Student.FromCsv(v))
                                           .ToList();
              seedClubs();
            }


        private Guid GetRandomAdmin()
        {
            // This query will create a random ordered selection based on Guids
            Guid result = Students.Select(s =>
            new { s.StudentId, r = Guid.NewGuid() }) // generate a list of player ids with a 
            .OrderBy(o => o.r)                      // orderby the guid which is a randomly generated unique id
            .ToList()                               // convert the IEnumeral to a list
            .First().StudentId;                      // take the first record and grab th eplayerid Guid field value
            return result;
        }
        private void seedClubs()
        {
            // Create a list of clubs and populate it test data
            Clubs = new List<Club>()
            // Club collection
            {
                // First club record 
                new Club {
                id = Guid.NewGuid(),
                ClubName = "ITS FC",
                // Select a random student
                adminID = GetRandomAdmin(),
                 ClubEvents = new List<ClubEvent>(),
                 ClubMembers = new List<Member>(),
                   CreationDate = DateTime.Now
                    },
                // Second Club record
                new Club {
                id = Guid.NewGuid(),
                ClubName = "ITS GAA ",
                // Select a random student
                adminID = GetRandomAdmin(),
                 ClubEvents = new List<ClubEvent>(),
                 ClubMembers = new List<Member>(),
                   CreationDate = DateTime.Now
                    },
                // Third Club record
                new Club {
                id = Guid.NewGuid(),
                ClubName = "The Chess Club ",
                // Select a random student
                adminID = GetRandomAdmin(),
                 ClubEvents = new List<ClubEvent>(),
                 ClubMembers = new List<Member>(),
                   CreationDate = DateTime.Now
                    },

            };

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposed)
            {
                if (disposing)
                {
                    // Manual release of managed resources.
                }
                // Release unmanaged resources.
                disposed = true;
            }
        }
        public List<Student> getTop(int count)
        {
            return Students.Take(count).ToList();
        }

        public void AddEvent(string ClubName, ClubEvent clubEvent)
        {
            Club clubFound = Clubs.FirstOrDefault(c => c.ClubName == ClubName);
            if (clubFound != null)
            {
                clubFound.ClubEvents.Add(clubEvent);
            }
            else Console.WriteLine("Club Name not found {0}", ClubName);
        }

        public bool addMember(string ClubName, Student s, out string Error)
        {
            Club club = Clubs.Where(c => c.ClubName == ClubName).FirstOrDefault();
            if (club == null) { Error = "Club does not exist"; return false; }
            //Checking club
            Student validStudent = Students
                        .Where(student => student.StudentId == s.StudentId)
                        .FirstOrDefault();
            //Check Studnet
            if (validStudent == null) { Error = "Student does not exist"; return false; }

            Member current = club.ClubMembers.FirstOrDefault(m => m.StudentID == s.StudentId);

            if (current != null) { Error = "Student Already a member"; return false; }
            //Add member

            club.ClubMembers.Add(new Member
            {
                memberID = Guid.NewGuid(),
                StudentID = validStudent.StudentId
            });
            Error = "ok";
            return true;
        }

        public List<Member> GetMembers(string ClubName)
        {
            return Students.Select(s =>
            new Member { memberID = Guid.NewGuid(), StudentID = s.StudentId, })
            .OrderBy(m => m.memberID).Take(10)
            .ToList();
        }

        public object Clone()
        {
            return this.Clone();
        }

    }
}
