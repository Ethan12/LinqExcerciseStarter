using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadExercise1
{
    [Serializable]

    //TESTS
    internal class Club
    {
        public Guid id;
        public string ClubName;
        public DateTime CreationDate;
        public Guid adminID;
        public List<Member> ClubMembers;
        public List<ClubEvent> ClubEvents;
        public string Info { get { return "Id:" + id.ToString() + " Club Name: " + ClubName; } }
        
    }
}
