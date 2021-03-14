using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain
{
    [XmlInclude(typeof(Domestic)), XmlInclude(typeof(International)), XmlInclude(typeof(LicenseRenew))]
    public class Appointment : IComparable<Appointment>
    {
        private string appointmentTime;
        private Candidate candidateData;
        private int slotNumber;

        private string testLocation;


        public string AppointmentTime { get => appointmentTime; set => appointmentTime = value; }
        public int SlotNumber { get => slotNumber; set => slotNumber = value; }

        public Candidate CandidateData { get => candidateData; set => candidateData = value; }
        public string TestLocation { get => testLocation; set => testLocation = value; }

        public int CompareTo(Appointment other)
        {
            return candidateData.AmountPaid.CompareTo(other.CandidateData.AmountPaid);
        }


        public override string ToString()
        {
            return string.Format("Appointment Time : {0}, {1}\n\n", appointmentTime, candidateData);
        }
    }
}
