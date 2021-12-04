using System.Collections.Generic;

namespace WebApi.Models
{
    public class QueryAllUserResponseModel
    {
        public IEnumerable<UserModel> Users { get; set; }
    }
}
