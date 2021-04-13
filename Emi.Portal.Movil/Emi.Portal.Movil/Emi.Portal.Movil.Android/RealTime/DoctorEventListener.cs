using System;
using Emi.Portal.Movil.Droid.Services;
using Firebase.Database;
using Xamarin.Forms;

namespace Emi.Portal.Movil.Droid.RealTime
{
    public class DoctorEventListener : Java.Lang.Object, IValueEventListener
	{
		public void OnCancelled(DatabaseError error) { }

		public void OnDataChange(DataSnapshot snapshot)
		{
			String message = snapshot.Value?.ToString();
			MessagingCenter.Send(QueingFirebaseService.KEY_DOCTOR, QueingFirebaseService.KEY_DOCTOR, message);
		}
	}
}
