using DigitalPrint.Core.Domain.Shared.ValueObjects;
using DigitalPrint.Core.Domain.UserProfiles.Commands;
using DigitalPrint.Core.Domain.UserProfiles.Data;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.UserProfiles.CommandHandlers;

public class UpdateUserEmailHandler : ICommandHandler<UpdateUserEmail>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileRepository _userProfileRepository;
    public UpdateUserEmailHandler(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
    {
        _unitOfWork = unitOfWork;
        _userProfileRepository = userProfileRepository;
    }
    public void Handle(UpdateUserEmail command)
    {
        var user = _userProfileRepository.Load(command.UserId);
        if (user == null)
            throw new InvalidOperationException($"کاربری با شناسه {command.UserId} یافت نشد.");
        user.UpdateEmail(Email.FromString(command.Email));
        _unitOfWork.Commit();
    }
}