namespace WiiExplorer
{
    public struct UpdateInformation
    {
        public Version Version;
        public List<string> Notes;
        public bool IsUpdateRequired = false;

        private static readonly string[] NewlineSeparators = ["\r\n", "\r", "\n"];

        public static UpdateInformation? IsUpdateExist(string? UPDATEALERT_URL)
        {
            try
            {
                using HttpClient client = new() { Timeout = new(0, 0, 15) };
                string content = client.GetStringAsync(UPDATEALERT_URL).Result;
                string[] lines = content.Split(NewlineSeparators, StringSplitOptions.None);
                return new(lines);
            }
            catch (Exception)
            {
                // No handling here for now...
                return null;
            }
        }
        

        public UpdateInformation(string[] Data)
        {
            Version = new(Data[0]);

            Notes = [];
            bool IsStarted = false;
            for (int i = 1; i < Data.Length; i++)
            {
                if (!IsStarted)
                {
                    if (Data[i].Equals("#required", StringComparison.OrdinalIgnoreCase))
                        IsUpdateRequired = true;
                    if (Data[i].Equals("#notes", StringComparison.OrdinalIgnoreCase))
                        IsStarted = true;
                    continue;
                }

                Notes.Add(Data[i]);
            }
        }

        public readonly bool IsNewer()
        {
            Version Local = new(ApplicationVersion);
            return Local.CompareTo(Version) < 0;
        }

        public override readonly string ToString()
        {
            // Unfortunately, the patch notes will always be in english
            string v = Notes.Count > 0 ? "\n\n" : "";
            for (int i = 0; i < Notes.Count; i++)
                v += Notes[i] + Environment.NewLine;

            string header = Properties.Resources.String_ReleaseInformation;
            string verstr = Properties.Resources.String_Version;
            return
                $"""
                ---- {header} ----
                {verstr} {Version.Major}.{Version.Minor}.{Version.MajorRevision}.{Version.Build}{v}
                -----------------------------
                """;
        }

        // Microsoft made a change that broke my old system :(
        public static string ApplicationVersion
        {
            get
            {
                string ver = Application.ProductVersion;
                string[] WhyMicrosoft = ver.Split('+');
                return WhyMicrosoft[0];
            }
        }
    }
}
