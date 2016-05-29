using System.Collections.Generic;
using Windows.Data.Json;

namespace Paris_Saveur_UWP.Models
{
    class Tag
    {
        public string Name { get; set; }
        public int NumbersTagged { get; set; }
        public string TagToString { get; set; }
        public List<Tag> TagList { get; set; }

        public Tag(JsonObject jsonObject)
        {
            Name = jsonObject.GetNamedString("name");
            NumbersTagged = (int)jsonObject.GetNamedNumber("num_tagged");
            TagToString = Name + " (" + NumbersTagged + ")";
        }
    }
}
