using DigitalPrint.Core.Domain.UserProfiles.Commands;
using DigitalPrint.Core.Domain.UserProfiles.Data;
using DigitalPrint.Core.Domain.UserProfiles.ValueObjects;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.UserProfiles.CommandHandlers
{
    public class UpdateUserNameHandler : ICommandHandler<UpdateUserName>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository _userProfileRepository;
        public UpdateUserNameHandler(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
        {
            this.unitOfWork = unitOfWork;
            _userProfileRepository = userProfileRepository;
        }
        public void Handle(UpdateUserName command)
        {
            var user = _userProfileRepository.Load(command.UserId);
            if (user == null)
                throw new InvalidOperationException($"کاربری با شناسه {command.UserId} یافت نشد.");
            user.UpdateName(FirstName.FromString(command.FirstName), LastName.FromString(command.LastName));
            unitOfWork.Commit();
        }
    }
}
