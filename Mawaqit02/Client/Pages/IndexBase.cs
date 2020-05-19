using Blazored.LocalStorage;
using Mawaqit02.Client.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mawaqit02.Client.Pages
{
    public class IndexBase: ComponentBase
    {

        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        public string CardBackgroundColor { get; set; } = "#1b8046";

        public string SelectedCardBackgroundColor { get; set; } = "#275eb0";

        public string CardTextColor { get; set; } = "#FFFFFF";

        public string SelectedCardTextColor { get; set; } = "#FFFFFF";

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            CalculateTimes();

            CardBackgroundColor = await LocalStorage.GetItemAsync<string>("fill") ?? CardBackgroundColor;
            SelectedCardBackgroundColor = await LocalStorage.GetItemAsync<string>("selectedfill") ?? SelectedCardBackgroundColor;
            CardTextColor = await LocalStorage.GetItemAsync<string>("text") ?? CardTextColor;
            SelectedCardTextColor = await LocalStorage.GetItemAsync<string>("selectedtext") ?? SelectedCardTextColor;
        }


        private (string EnName, string ArName, DateTime Time) _closestTime;

        private List<List<(string EnName, string ArName, DateTime Time)>> _allTimes;

        private List<List<(string EnName, string ArName, DateTime Time)>> AllTimes
        {
            get
            {
                if (_allTimes == null)
                {
                    CalculateTimes();
                }

                return _allTimes;
            }
        }

        protected List<List<(string Title, DateTime Time, bool IsSelected)>> AllDisplayValues = new List<List<(string Title, DateTime Time, bool IsSelected)>>();

        private void CalculateTimes()
        {
            var now = DateTime.Now;
            var todayTimes = GetTimes(DateTime.Now);
            _allTimes = new List<List<(string, string, DateTime)>>();
            _allTimes.Add(todayTimes.ToList());
            if (now > todayTimes.Last().time.AddMinutes(10))
            {
                _allTimes.Add(GetTimes(now.AddDays(1)).ToList());
            }
 
            var displayedTimes = _allTimes.First();
            var timesButShuruq = displayedTimes.Take(1).Union(displayedTimes.Skip(2));
            //var subh = timesButShuruq.First();
            //_closestTime = timesButShuruq.Aggregate(subh, (t, x) => (x.Time - now).Duration() < (t.Time - now).Duration() ? x : t);
            _closestTime = timesButShuruq.Reverse()
                .Aggregate(timesButShuruq.Last(), (t, x) => now < x.Time ? x : t);

            AllDisplayValues.Clear();
            AllDisplayValues.AddRange(_allTimes.Select(l1 => l1
                .Take(1).Union(l1.Skip(2))
                .Select(k => (k.ArName, k.Time, 
                k == timesButShuruq.Reverse()
                .Aggregate(timesButShuruq.Last(), (t, x) => now < x.Time ? x : t)
                //false
                )).ToList()));
        }

        private IEnumerable<(string enName, string arName, DateTime time)> GetTimes(DateTime dt)
        {
            var st = new Mawaqit.Shared.SalatTime();
            st.SetCalcMethod(Mawaqit.Shared.CalculationMethod.Isna);

            double latitude = 45.5977;
            double longitude = -73.5937;
            int timeZone = -4;

            var eventTimes = st.GetTimes(dt.Year, dt.Month, dt.Day, latitude, longitude, timeZone);

            // Time Names
            String[] englishNames = { "Subh", "Sunrise", "Dhuhr", "Asr", "Sunset", "Maghrib", "Isha" };
            // Arabic Names
            String[] arabicNames = { "صبح", "شروق", "ظهر", "عصر", "غروب", "مغرب", "عشاء" };
            // Mixed Names
            String[] mixedNames = { "Subh", "Sunrise", "Dhuhr", "Asr", "Sunset", "Maghrib", "Isha" };

            var result = new List<(string, string, DateTime)>();

            int i = 0;
            foreach (var time in eventTimes)
            {
                if (i == 4)
                {
                    i++;
                    continue;
                }
                result.Add((mixedNames[i], arabicNames[i], time));
                i++;
            }

            return result;
        }

        private string GetBoxClass((string EnName, string ArName, DateTime Time) v)
        {
            if (_closestTime == default)
                return "sbox";

            return v == _closestTime ? "sbox-sel-border-top sbox-sel-color" : "sbox";
        }

        System.Timers.Timer _timer;

        private void OnTimerTick(object state, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();

            CalculateTimes();

            this.StateHasChanged();

            var now = DateTime.Now;
            _timer.Interval = TimeSpan.FromSeconds(60 - now.Second).TotalMilliseconds;
            _timer.Start();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _timer = new System.Timers.Timer();
            _timer.Elapsed += OnTimerTick;
            var now = DateTime.Now;
            _timer.Interval = TimeSpan.FromSeconds(60 - now.Second).TotalMilliseconds;
            _timer.Start();
        }
    }
}
