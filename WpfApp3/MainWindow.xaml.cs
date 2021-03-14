using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Appointment> displayAppointment = null;
        private string alpha;
        public MainWindow()
        {
            InitializeComponent();
            DisplayAppointment = new ObservableCollection<Appointment>();
            DataContext = this;
            AddItemsToComboBox();
            File.Delete("AppointmentList.xml");
        }

        private void AddItemsToComboBox()
        {
            string[] names = Enum.GetNames(typeof(CandidateType));
            foreach (string name in names)
            {
                cmbCandidateType.Items.Add(name);
                cmbCandidateTypeSearch.Items.Add(name);
            }
            cmbIndividualTask.Items.Add("Yes");
            cmbIndividualTask.Items.Add("No");
            string[] testlocations = new string[] { "Guelph", "Kitchener", "Waterloo", "Cambridge", "Brampton" };
            foreach (string location in testlocations)
            {
                cmbTestLocation.Items.Add(location);
            }
        }

        private void CmbAppointmentTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grdCustomer.Visibility = Visibility.Visible;
        }

        private void CmbTestLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                                
        }

        Appointment[] appArray;
        int slotCount = 0;

        private void BtnShowTimings_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you want to confirm the appointment count?\nOnce you set the count, you wont be able to change it", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string totalSlotString = string.Empty;
                int totalSlot = 0;
                totalSlotString = txtAppointmentCount.Text;
                if (!(int.TryParse(totalSlotString, out totalSlot) && totalSlot <= 12 && totalSlot > 0))
                {
                    txtAppointmentCount.Foreground = Brushes.Red;
                }
                else
                {
                    txtAppointmentCount.Foreground = Brushes.Black;
                    cmbAppointmentTime.Items.Clear();
                    cmbAppointmentTime.IsEnabled = true;
                    cmbTestLocation.IsEnabled = true;
                    slotCount = totalSlot;
                    appArray = new Appointment[totalSlot];
                    DateTime theTime = DateTime.Now;
                    DateTime initTime = new DateTime(theTime.Year, theTime.Month, theTime.Day, 9, 0, 0);
                    for (int i = 0; i < appArray.Length; i++)
                    {
                        appArray[i] = new Appointment();
                        appArray[i].AppointmentTime = initTime.ToString("HH:mm tt");
                        appArray[i].SlotNumber = i + 1;
                        initTime = initTime.AddHours(1);
                    }
                    AddToAppointment(appArray);
                    txtAppointmentCount.IsEnabled = false;
                    btnShowTimings.IsEnabled = false;
                }
            } else
            {
                cmbAppointmentTime.IsEnabled = false;
                cmbTestLocation.IsEnabled = false;
                txtAppointmentCount.IsEnabled = true;
                btnShowTimings.IsEnabled = true;
            }
        }

        private void AddToAppointment(Appointment[] appArray)
        {
            cmbAppointmentTime.Items.Clear();
            for (int i = 0; i < appArray.Length; i++)
            {
                if (appArray[i].CandidateData == null)
                {
                    string appointmentString = appArray[i].SlotNumber + ".  " + appArray[i].AppointmentTime;
                    cmbAppointmentTime.Items.Add(appointmentString);
                }
            }
        }

        private void BtnAddFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddXML_Click(object sender, RoutedEventArgs e)
        {

        }

        AppointmentList appointmentListXML = new AppointmentList();
        private void BtnDisplay_Click(object sender, RoutedEventArgs e)
        {
            grdAppointment.ItemsSource = "";
            try
            {

                appointmentListXML = new AppointmentList();
                appointmentListXML = xmlObject.ReadFromXMLFile();
                appointmentListXML.Sort();
                //grdAppointment.ItemsSource = appointmentListXML;
                DisplayAppointment.Clear();
                for (int i = 0; i < appointmentListXML.Count(); i++)
                {
                    Appointment appt = appointmentListXML[i];
                    DisplayAppointment.Add(appt);
                }

                grdAppointment.ItemsSource = displayAppointment;

            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.ToString());
            }
            finally
            {

            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text;

            if (cmbCandidateType.SelectedIndex == -1)
            {

                if (search.Length > 0)
                {
                    var query = from myList in appointmentListXML
                                where myList.CandidateData.CandidateName.ToUpper() == search.ToUpper()
                                select myList;

                    grdAppointment.ItemsSource = query;
                }
            }
            else
            {
                if (search.Length > 0)
                {
                    string searchCandidateType = this.cmbCandidateTypeSearch.SelectedItem.ToString();
                    var query = from myList in appointmentListXML
                                where (myList.CandidateData.CandidateName.ToUpper() == search.ToUpper()
                                && myList.CandidateData.CandidateType.ToUpper() == searchCandidateType.ToUpper())
                                select myList;

                    grdAppointment.ItemsSource = query;
                }
            }
        }

        CandidateType candidateType;
        private void CmbCandidateType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            candidateType = (CandidateType)cmbCandidateType.SelectedIndex + 1;
            switch (candidateType)
            {
                case CandidateType.Domestic:
                    lblIndividualTask.Content = "Is fee deduction allowed?";
                    break;
                case CandidateType.International:
                    lblIndividualTask.Content = "Is allowed to work?";
                    break;
                case CandidateType.LicenseRenew:
                    lblIndividualTask.Content = "Is license expired?";
                    break;
            }
        }
        AppointmentList appointmentList = new AppointmentList();
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (txtAppointmentCount.Text.Length == 0 || cmbAppointmentTime.SelectedIndex == -1)
            {
                MessageBox.Show("Enter appointment count and click the 'Show Timings'","Error",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (cmbAppointmentTime.SelectedIndex == -1)
                {
                    MessageBox.Show("Select an appointment time");
                    flag = false;
                }
                else if (cmbTestLocation.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a test location");
                    flag = false;
                }
                else
                {
                    int slot = cmbAppointmentTime.SelectedIndex + 1;
                    if (appArray[slot - 1].CandidateData == null)
                    {

                        Candidate candidate = new Domestic();
                        appArray[slot - 1].AppointmentTime = appArray[slot - 1].AppointmentTime;
                        appArray[slot - 1].TestLocation = cmbTestLocation.Text;
                        appArray[slot - 1].SlotNumber = slot;
                        int candidateType = cmbCandidateType.SelectedIndex + 1;


                        switch (candidateType)
                        {
                            case (int)CandidateType.Domestic:
                                candidate = new Domestic();
                                candidate.CandidateType = "Domestic";
                                string IsFeeDeduction = cmbIndividualTask.Text;
                                ((Domestic)candidate).IsFeeDeduction = (IsFeeDeduction.ToUpper() == "YES") ? true : false;
                                break;
                            case (int)CandidateType.International:
                                candidate = new International();
                                candidate.CandidateType = "International";
                                string isWorkPermit = cmbIndividualTask.Text;
                                ((International)candidate).IsWorkPermit = (isWorkPermit.ToUpper() == "YES") ? true : false;
                                break;
                            case (int)CandidateType.LicenseRenew:
                                candidate = new LicenseRenew();
                                candidate.CandidateType = "LicenseRenew";
                                string isLicenseExpired = cmbIndividualTask.Text;
                                ((LicenseRenew)candidate).IsLicenseExpired = (isLicenseExpired.ToUpper() == "YES") ? true : false;
                                break;
                        }

                        string candidateName = txtName.Text;
                        string candidateAddress = txtAddress.Text;
                        string candidateEmail = txtEmail.Text;
                        string passport = txtPassport.Text;
                        decimal amount;
                        string amountString = txtAmount.Text;
                        string creditCard = txtCreditcard.Text;
                        candidate.CreditCard = creditCard;
                        candidate.Passport = passport;
                        candidate.Email = candidateEmail;
                        if (candidateName.Length == 0)
                        {
                            if (Validation.GetHasError(txtName) == true)
                                return;
                            txtName.Foreground = Brushes.Red;
                            txtName.Select(0, txtName.Text.Length);
                            flag = false;
                        }
                        else
                        {
                            txtName.Foreground = Brushes.Black;
                        }
                        if (candidateAddress.Length == 0)
                        {
                            txtAddress.Foreground = Brushes.Red;
                            txtAddress.Select(0, txtAddress.Text.Length);
                            flag = false;
                        }
                        else
                        {
                            txtAddress.Foreground = Brushes.Black;
                        }
                        if (!candidate.CheckEmail())
                        {
                            txtEmail.Foreground = Brushes.Red;
                            txtEmail.Select(0, txtEmail.Text.Length);
                            flag = false;
                        }
                        else
                        {
                            txtEmail.Foreground = Brushes.Black;
                        }
                        if (!(decimal.TryParse(amountString, out amount)))
                        {
                            txtAmount.Foreground = Brushes.Red;
                            txtAmount.Select(0, txtAmount.Text.Length);
                            flag = false;
                        }
                        else
                        {
                            txtAmount.Foreground = Brushes.Black;
                        }
                        if (!candidate.CheckCreditCard())
                        {
                            txtCreditcard.Foreground = Brushes.Red;
                            txtCreditcard.Select(0, txtCreditcard.Text.Length);
                            flag = false;
                        }
                        else
                        {
                            txtCreditcard.Foreground = Brushes.Black;
                        }
                        if (!candidate.CheckPassport())
                        {
                            txtPassport.Foreground = Brushes.Red;
                            txtPassport.Select(0, txtPassport.Text.Length);
                            flag = false;
                        }
                        else
                        {
                            txtPassport.Foreground = Brushes.Black;
                        }
                        if (cmbAppointmentTime.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select an appointment time");
                            flag = false;
                        }
                        if (cmbTestLocation.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a test location");
                            flag = false;
                        }
                        if (cmbCandidateType.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select an landscape type");
                            flag = false;
                        }
                        if (cmbIndividualTask.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select individual task");
                            flag = false;
                        }
                        if (flag)
                        {
                            candidate.CandidateName = candidateName;
                            candidate.CandidateAddress = candidateAddress;
                            candidate.Email = candidateEmail;
                            candidate.Passport = passport;
                            candidate.AmountPaid = amount;
                            candidate.CreditCard = creditCard;
                            appArray[slot - 1].CandidateData = candidate;
                            appointmentList.Add(appArray[slot - 1]);
                            //DisplayAppointment.Add(appArray[slot - 1]);
                            slotCount--;
                            // AddToAppointment(appArray);
                            xmlObject.WriteToXMLFile(appointmentList);
                            MessageBox.Show("Successfully added candidate details to file");

                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("Fill all the fields..");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Slot Taken..");
                    }
                }
            }
        }

        private void ResetFields()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtCreditcard.Clear();
            txtAmount.Clear();
            txtEmail.Clear();
            txtPassport.Clear();
            cmbIndividualTask.SelectedIndex = -1;
            cmbCandidateType.SelectedIndex = -1;
            cmbTestLocation.SelectedIndex = -1;
            cmbAppointmentTime.SelectedIndex = -1;
        }
        XmlFunctions xmlObject = new XmlFunctions();

        public string Alpha { get => alpha; set => alpha = value; }
        public ObservableCollection<Appointment> DisplayAppointment { get => displayAppointment; set => displayAppointment = value; }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private void CmbCandidateTypeSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string search = this.cmbCandidateTypeSearch.SelectedItem.ToString();
            if (search.Length > 0)
            {
                var query = from myList in appointmentListXML
                            where myList.CandidateData.CandidateType.ToUpper() == search.ToUpper()
                            select myList;

                grdAppointment.ItemsSource = query;

            }
        }
    }
}
