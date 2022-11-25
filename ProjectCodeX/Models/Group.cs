namespace ProjectCodeX.Models
{
    public class Group
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public int UserCount { get; set; }
        public IList<User> Users { get; set; } = new List<User>();

        public Group(string id, string name, int userCount, List<User> users)
        {
            Id = id;
            Name = name;
            UserCount = userCount;
            Users = users;
        }
    }

}
