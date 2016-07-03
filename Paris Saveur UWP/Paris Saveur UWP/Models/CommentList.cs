using System.Collections.ObjectModel;
using Windows.Data.Json;

namespace Paris_Saveur_UWP.Models
{
    class CommentList
    {
        private ObservableCollection<Comment> _commentCollection = new ObservableCollection<Comment>();
        public ObservableCollection<Comment> CommentCollection
        {
            get { return _commentCollection; }
            set
            {
                _commentCollection = value;
            }
        }

        public bool HasNextPage;
        public int CurrentPage = 1;
        public int MaxPage;

        public CommentList(string jsonString)
        {
            loadMoreComments(jsonString);
        }
        public CommentList() { }

        public void loadMoreComments(string jsonString)
        {
            JsonObject json = JsonValue.Parse(jsonString).GetObject();
            JsonArray array = json.GetNamedArray("rating_list");
            for (int i = 0; i < array.Count; i++)
            {
                var comment = new Comment(array[i].GetObject());
                _commentCollection.Add(comment);
            }
            var pagination = json.GetNamedObject("pagination");
            if (pagination != null)
            {
                this.HasNextPage = pagination.GetNamedBoolean("has_next");
                this.CurrentPage = (int)pagination.GetNamedNumber("number");
                this.MaxPage = (int)pagination.GetNamedNumber("num_pages");
            }
        }
    }
}
