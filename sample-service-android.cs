using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Android.Util;
using Xamarin.Forms;
using Alme.Senders;
using Alme.Repositories;

namespace Alme.Droid.Services
{
    [Service]
    public class AlarmService : Service
    {
        private readonly IAlarmRepository alarmRepository =  DependencyService.Get<IAlarmRepository>();
        static readonly string TAG = "X:" + typeof(AlarmService).Name;
        static readonly int TimerWait = 30000;
        Timer timer;
        DateTime startTime;
        bool isStarted = false;

        public override void OnCreate()
        {
            base.OnCreate();
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug(TAG, $"OnStartCommand called at {startTime}, flags={flags}, startid={startId}");
            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                Log.Debug(TAG, $"This service was already started, it's been running for {runtime:c}.");
            }
            else
            {
                startTime = DateTime.UtcNow;
                Log.Debug(TAG, $"Starting the service, at {startTime}.");
                timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;
            }
            return StartCommandResult.RedeliverIntent;
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override void OnDestroy()
        {
            timer.Dispose();
            timer = null;
            isStarted = false;

            TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
            Log.Debug(TAG, $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.");
            base.OnDestroy();
        }
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 475645;
        async void HandleTimerCallback(object state)
        {
           // do stuff

        }
        
    }
}