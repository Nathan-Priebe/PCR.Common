using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PCR.Common
{
    public class Audio
    {
        public static async Task<List<T>> GetAudioStatus<T>(string url)
        {
            var appList = new List<T>();

            try
            {
                var response = await WebRequest.GetWebRequest(url, "Audio/All");
                var responseBody = await response.Content.ReadAsStringAsync();
                appList = JsonConvert.DeserializeObject<List<T>>(responseBody);
            }
            catch (Exception)
            {
                throw new Exception("Unable to retrieve audio session, check server is running and try again");
            }
            return appList;
        }

        public static async Task UpdateAppVolume<T>(string url, T message)
        {
            try
            {
                await WebRequest.PostWebRequest(url, "Audio/Update", message);
            }
            catch (Exception e)
            {
                await WebRequest.PostWebRequest(url, "System/UpdateLog", e.Message);
            }
        }

        public static async Task MuteApp(string url, uint processId)
        {
            try
            {
                await WebRequest.PostWebRequest(url, "Audio/Mute", processId);
            }
            catch (Exception e)
            {
                await WebRequest.PostWebRequest(url, "System/UpdateLog", e.Message);
            }
        }
    }
}
