using System;
using Emi.Portal.Movil.iOS.Services;
using Emi.Portal.Movil.Logic.Contracts.Services;
using Emi.Portal.Movil.Logic.Models.Domain;
using Emi.Portal.Movil.Logic.Models.Responses;
using Firebase.Database;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(QueingFirebaseService))]
namespace Emi.Portal.Movil.iOS.Services
{
    public class QueingFirebaseService : IQueingFirebaseService
    {
        DatabaseReference database;

        public static string KEY_MESSAGE = "value";
        public static string KEY_DOCTOR = "doctor";
        public object ExistsPatient { get; set; }

        public void Connect()
        {
            Database.DefaultInstance.GoOnline();
            if (database == null)
            {
                database = Database.DefaultInstance.GetRootReference();
                database.RemoveAllObservers();
            }
        }

        public void GetOutSesionWaitingRoom(string patientServiceType, string patientDocument)
        {
            if (database != null)
            {
                database.RemoveAllObservers();
                database.Dispose();
                database = null;
            }

            Database.DefaultInstance.GoOffline();
        }

        public void InitializeApp(RealTimeConfigurationResponse realTime)
        {

        }

        string patientServicesType { get; set; }
        string patientDocuments { get; set; }

        nuint handleReferencePatient;

        public void LoadSesionWaitingRoom(string patientServiceType, string patientDocument)
        {
            patientServicesType = patientServiceType;
            patientDocuments = patientDocument;

            // Se llena la información del paciente.
            object[] noteKeys = { "onLineFrom", "id" };
            object[] noteValues = { ExistsPatient != null ? ExistsPatient : ServerValue.Timestamp, patientDocument };
            var patient = NSDictionary.FromObjectsAndKeys(noteValues, noteKeys, noteKeys.Length);

            // Envío la información del paciente.
            database.GetChild($"queques/{patientServiceType}/patients/{patientDocument}").UpdateChildValues(patient);
            ExistsPatient = ExistsPatient != null ? ExistsPatient : new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

            // Si el paciente se desconecta, se limpia la información de la base de datos.
            database.GetChild($"queques/{patientServiceType}/patients/{patientDocument}").RemoveValueOnDisconnect();
            database.GetChild($"display/{patientDocument}").RemoveValueOnDisconnect();

            // Me suscribo a la información relevante para el paciente conectado.
            var messages = database.GetChild($"display/{patientDocument}");

            handleReferencePatient = messages.ObserveEvent(DataEventType.Value, (snapshot) => {
                // Do magic with the folder data
                PatientRealTime patientLocal = null;

                if (snapshot.GetValue() != NSNull.Null)
                {
                    var folderData = snapshot.GetValue<NSDictionary>();
                    var position = folderData.ValueForKey((NSString)"position");
                    var room = folderData.ValueForKey((NSString)"room");
                    var doctor = folderData.ValueForKey((NSString)"doctor");
                    var onLineFrom = folderData.ValueForKey((NSString)"onLineFrom");

                    patientLocal = new PatientRealTime
                    {
                        Position = position == null ? 1 : Convert.ToInt32(position.ToString()),
                        Doctor = doctor == null ? 0 : Convert.ToInt32(doctor.ToString()),
                        OnLineFrom = onLineFrom?.ToString(),
                        Room = room?.ToString()
                    };

                    MessagingCenter.Send(KEY_MESSAGE, KEY_MESSAGE, patientLocal);
                }
            });
        }

        public string GetMessageKey()
        {
            return KEY_MESSAGE;
        }

        public string GetDoctorKey()
        {
            return KEY_DOCTOR;
        }

        nuint handleReferenceDoctor;

        public void GetDoctors(string patientServiceType)
        {
            var messages = database.GetChild($"summary/{patientServiceType}/doctors");

            messages.ObserveEvent(DataEventType.Value, (snapshot) => {
                //var folderData = snapshot.GetValue<NSDictionary>();
                // Do magic with the folder data
                String message = "";
                if (snapshot.GetValue() != NSNull.Null)
                {
                    message = snapshot.GetValue().ToString();
                }
                MessagingCenter.Send(KEY_DOCTOR, KEY_DOCTOR, message);
            });
        }
    }
}
