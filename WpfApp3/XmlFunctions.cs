using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using Domain;

namespace WpfApp3
{
    class XmlFunctions
    {
        public void WriteToXMLFile(AppointmentList appointmentList)
        {
            XmlSerializer mySerializer =
            new XmlSerializer(typeof(AppointmentList));
            TextWriter myWriter = new StreamWriter("AppointmentList.xml");
            mySerializer.Serialize(myWriter, appointmentList);
            myWriter.Close();
        }

        public AppointmentList ReadFromXMLFile()
        {
            AppointmentList appointmentList = null;
            string path = "AppointmentList.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));
            StreamReader reader = new StreamReader(path);
            appointmentList = (AppointmentList)serializer.Deserialize(reader);
            reader.Close();
            return appointmentList;
        }

        private void ValidationHandler(object sender,
            ValidationEventArgs e)
        {
            Console.WriteLine(e.Severity);
        }
    }
}
