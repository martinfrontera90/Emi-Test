using System;
using System.Collections.Generic;
using Emi.Portal.Movil.Droid.RealTime;
using Emi.Portal.Movil.Droid.Services;
using Emi.Portal.Movil.Logic.Contracts.Services;
using Emi.Portal.Movil.Logic.Models.Responses;
using Firebase;
using Firebase.Database;
using Java.Lang;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(QueingFirebaseService))]
namespace Emi.Portal.Movil.Droid.Services
{
    public class QueingFirebaseService : IQueingFirebaseService
    {
        FirebaseApp firebase;
        FirebaseDatabase database;
        public static string KEY_MESSAGE = "value";
        public static string KEY_DOCTOR = "doctor";
        public object ExistsPatient { get; set; }

        public void InitializeApp(RealTimeConfigurationResponse realTime)
        {
            var options = new FirebaseOptions.Builder()
                .SetApiKey(realTime.ApiKey)
                .SetApplicationId(realTime.AppId)
                .SetDatabaseUrl(realTime.DatabaseURL)
                .SetProjectId(realTime.ProjectId)
                .SetStorageBucket(realTime.StorageBucket)
                .Build();

            if (firebase == null)
            {
                firebase = FirebaseApp.InitializeApp(CrossCurrentActivity.Current.AppContext, options, "FirebaseDateBase");
            }
        }

        public void Connect()
        {
            if (database == null)
            {
                database = FirebaseDatabase.GetInstance(firebase);
            }
            database.GoOnline();
        }

        public void LoadSesionWaitingRoom(string patientServiceType, string patientDocument)
        {
            var timeStamp = ExistsPatient != null ? new Long(ExistsPatient.ToString()) : (Java.Lang.Object)ServerValue.Timestamp;
            // Se llena la información del paciente.
            var patient = new Dictionary<string, Java.Lang.Object>();
            patient.Add("onLineFrom", timeStamp);
            patient.Add("id", patientDocument);

            // Envío la información del paciente.
            database.GetReference($"queques/{patientServiceType}/patients/{patientDocument}").UpdateChildren(patient);
            ExistsPatient = ExistsPatient != null ? ExistsPatient : new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

            // Si el paciente se desconecta, se limpia la información de la base de datos.
            database.GetReference($"queques/{patientServiceType}/patients/{patientDocument}").OnDisconnect().RemoveValue();
            database.GetReference($"display/{patientDocument}").OnDisconnect().RemoveValue();

            // Me suscribo a la información relevante para el paciente conectado.
            database.GetReference($"display/{patientDocument}").AddValueEventListener(new ValueEventListener());
        }

        public void GetOutSesionWaitingRoom(string patientServiceType, string patientDocument)
        {
            //Si el paciente se sale de la página actual o reinicia la página, se desconecta de la base de datos y se cierra la instancia de firebase.
            database.GetReference($"display/{patientDocument}").RemoveEventListener(new ValueEventListener());
            database.GetReference($"summary/{patientServiceType}/doctors").RemoveEventListener(new DoctorEventListener());
            database.GetReference($"display/{patientDocument}").RemoveValue();
            database.GoOffline();
        }

        public void GetDoctors(string patientServiceType)
        {
            database.GetReference($"summary/{patientServiceType}/doctors").AddValueEventListener(new DoctorEventListener());
        }

        public string GetMessageKey()
        {
            return KEY_MESSAGE;
        }

        public string GetDoctorKey()
        {
            return KEY_DOCTOR;
        }
    }
}
