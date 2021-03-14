using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain
{
    [XmlRoot("AppointmentList")]
    public class AppointmentList : IEnumerable<Appointment>,IDisposable
    {
        [XmlArray("Appointments")]
        [XmlArrayItem("Appointment", typeof(Appointment))]
        public List<Appointment> MyAppointmentList { get => appointmentList; set => appointmentList = value; }

        private List<Appointment> appointmentList = null;

        public AppointmentList()
        {

            appointmentList = new List<Appointment>();
        }

        public void Add(Appointment data)
        {
            appointmentList.Add(data);
        }

        public IEnumerator<Appointment> GetEnumerator()
        {
            return ((IEnumerable<Appointment>)appointmentList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Appointment>)appointmentList).GetEnumerator();
        }

        public int Count
        {
            get { return appointmentList.Count; }
        }

        public Appointment this[int i]
        {
            get { return appointmentList[i]; }
            set { appointmentList[i] = value; }
        }

        public void Sort()
        {
            appointmentList.Sort();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
