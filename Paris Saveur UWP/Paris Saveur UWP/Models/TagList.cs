using System.Collections.Generic;
using Windows.Data.Json;

namespace Paris_Saveur_UWP.Models
{
    class TagList
    {
        public List<Tag> TagCollection { get; set; }

        public TagList(string jsonString)
        {
            JsonObject json = JsonValue.Parse(jsonString).GetObject();
            JsonArray array = json.GetNamedArray("tag_cloud");
            TagCollection = new List<Tag>();
            for (int i = 0; i < array.Count; i++)
            {
                TagCollection.Add(new Tag(array[i].GetObject()));

            }
        }
    }
}
