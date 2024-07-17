using DigitalPrint.Core.Domain.UserProfiles.Commands;
using DigitalPrint.Core.Domain.UserProfiles.Data;
using DigitalPrint.Core.Domain.UserProfiles.ValueObjects;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.UserProfiles.CommandHandlers
{
    public class UpdateUserDisplayNameHandler : ICommandHandler<UpdateUserDisplayName>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProfileRepository _userProfileRepository;
        public UpdateUserDisplayNameHandler(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _userProfileRepository = userProfileRepository;
        }
        public void Handle(UpdateUserDisplayName command)
        {
            var user = _userProfileRepository.Load(command.UserId);
            if (user == null)
                throw new InvalidOperationException($"کاربری با شناسه {command.UserId} یافت نشد.");
            user.UpdateDisplayName(DisplayName.FromString(command.DisplayName));
            _unitOfWork.Commit();
        }
    }
}
