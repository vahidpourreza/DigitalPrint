using DigitalPrint.Core.Domain.UserProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPrint.Core.Domain.UserProfiles.Data
{
    public interface IUserProfileRepository
    {
        UserProfile Load(Guid id);
        void Add(UserProfile entity);
        bool Exists(Guid id);
    }
}
