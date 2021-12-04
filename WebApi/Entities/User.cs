using System;
namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}
