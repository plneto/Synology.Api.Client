namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public enum DownloadStationTaskStatus
    {
        None = 0,
        Waiting = 1,
        Downloading = 2,
        Paused = 3,
        Finishing = 4,
        Finished = 5,
        HashChecking = 6,
        PreSeeding = 7,
        Seeding = 8,
        FileHostingWaiting = 9,
        Extracting = 10,
        Preprocessing = 11,
        PreprocessPass = 12,
        Downloaded = 13,
        Postprocessing = 14,
        CaptchaNeeded = 15
    }
}
