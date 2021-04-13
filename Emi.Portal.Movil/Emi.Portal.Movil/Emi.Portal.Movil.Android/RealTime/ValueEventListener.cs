using System;
using Emi.Portal.Movil.Droid.Services;
using Emi.Portal.Movil.Logic.Models.Domain;
using Firebase.Database;
using Xamarin.Forms;

namespace Emi.Portal.Movil.Droid.RealTime
{
    public class ValueEventListener : Java.Lang.Object, IValueEventListener
	{
		public void OnCancelled(DatabaseError error) { }

		public void OnDataChange(DataSnapshot snapshot)
		{
			if (snapshot.Value != null)
			{
				var position = snapshot.Child("position").Value;
				var room = snapshot.Child("room").Value;
				var doctor = snapshot.Child("doctor").Value;
				var onLineFrom = snapshot.Child("onLineFrom").Value;
				var patient = new PatientRealTime
				{
					Position = (int?)position,
					Doctor = (int?)doctor,
                    OnLineFrom = onLineFrom?.ToString(),
                    Room = room?.ToString()
				};

				MessagingCenter.Send(QueingFirebaseService.KEY_MESSAGE, QueingFirebaseService.KEY_MESSAGE, patient);
			}
		}
	}
}
