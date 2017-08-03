using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace SpotifyLockedScreenPauser
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            var installer = FindInstaller(Installers);
            if (installer != null)
            {
                installer.Log = "SpotifyLockedScreenPauserLog";
                installer.Source = "SpotifyLockedScreenPauserSource";
            }
        }

        EventLogInstaller FindInstaller(InstallerCollection installers)
        {
            foreach (Installer installer in installers)
            {
                if (installer is EventLogInstaller)
                {
                    return (EventLogInstaller)installer;
                }

                EventLogInstaller eventLogInstaller = FindInstaller(installer.Installers);
                if (eventLogInstaller != null)
                {
                    return eventLogInstaller;
                }
            }
            return null;
        }
    }
}
