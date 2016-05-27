using Paris_Saveur_UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace Paris_Saveur_UWP.Tools
{
    class ImageDownloader
    {

        public static async void DownloadImageIntoImage(Restaurant restaurant)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                if (restaurant.thumbnail_url != null)
                {
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync(new Uri(ConnectionContext.BASE_URL + restaurant.thumbnail_url));
                    var buffer = await response.Content.ReadAsBufferAsync();
                    var img = buffer.ToArray();
                    InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
                    DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
                    writer.WriteBytes(img);
                    await writer.StoreAsync();
                    BitmapImage b = new BitmapImage();
                    b.SetSource(randomAccessStream);
                    restaurant.ThumbnailBitmap = b;
                }
            }
        }
    }
}
