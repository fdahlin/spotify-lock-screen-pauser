using SpotifyAPI.Local;
using System.ServiceProcess;

namespace SpotifyLockedScreenPauser
{
    public partial class SpotifyLockedScreenPauserService : ServiceBase
    {
        readonly SpotifyLocalAPI _spotify;
        bool _wasPlaying;

        public SpotifyLockedScreenPauserService()
        {
            InitializeComponent();
            CanHandleSessionChangeEvent = true;
            _spotify = new SpotifyLocalAPI();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);

            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLock:
                    Pause();
                    break;
                case SessionChangeReason.SessionUnlock:
                    Play();
                    break;
            }
        }

        bool ConnectToSpotifyIfRunning()
        {
            if (SpotifyLocalAPI.IsSpotifyRunning())
            {
                return _spotify.Connect();
            }
            return false;
        }

        void Pause()
        {
            if (ConnectToSpotifyIfRunning())
            {
                if (_spotify.GetStatus().Playing)
                {
                    _wasPlaying = true;
                    _spotify.Pause();
                } else
                {
                    _wasPlaying = false;
                }
            }
        }

        void Play()
        {
            if (ConnectToSpotifyIfRunning())
            {
                if (_wasPlaying)
                {
                    _spotify.Play();
                }
            }
        }
    }
}
