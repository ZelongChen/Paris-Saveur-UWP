﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Paris_Saveur_UWP.Tools
{
    class RestClient
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<string> GetResponseStringFromUrl(string uri)
        {
            var response = await _client.GetAsync(new Uri(uri));
            var buffer = await response.Content.ReadAsBufferAsync();
            var bufferArray = buffer.ToArray();
            var result = Encoding.UTF8.GetString(bufferArray, 0, bufferArray.Length);
            return result;
        }
    }
}
