using Realms;

namespace ArkadiumLibrary.Models
{
    public class Book : RealmObject
    {
        //book data
        [PrimaryKey] public int id { get; set; }

        public string title { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public int year { get; set; }
        public bool isFavorite { get; set; }
    }
}