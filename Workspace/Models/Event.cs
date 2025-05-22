using System.Data;

namespace Workspace.Models
{
    public class Event
    {
        public int ID { get; set; }
        public int NUM_Attendes { get; set; }

        public string Title { get; set; }
        public int Cost { get; set; }
        public int RoomID { get; set; }

        public DateTime Time { get; set; }
        
    }
}
