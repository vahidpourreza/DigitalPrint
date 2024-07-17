using DigitalPrint.Core.Domain.UserProfiles.Commands;
using DigitalPrint.Core.Domain.UserProfiles.Data;
using DigitalPrint.Core.Domain.UserProfiles.Entities;
using DigitalPrint.Core.Domain.UserProfiles.ValueObjects;
using Framework.Domain.ApplicationServices;
using Framework.Domain.Data;

namespace DigitalPrint.Core.ApplicationServices.UserProfiles.CommandHandlers;

public class RegisterUserHandler : ICommandHandler<RegisterUser>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileRepository _userProfileRepository;
    public RegisterUserHandler(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
    {
        unitOfWork = unitOfWork;
        _userProfileRepository = userProfileRepository;
    }
    public void Handle(RegisterUser command)
    {
        if (_userProfileRepository.Exists(command.UserId))
            throw new InvalidOperationException($"قبلا کاربری با شناسه {command.UserId} ثبت شده است.");

        UserProfile userProfile = new UserProfile(command.UserId,FirstName.FromString(command.FirstName),
                                                  LastName.FromString(command.LastName),
                                                  DisplayName.FromString(command.DisplayName));
        _userProfileRepository.Add(userProfile);
        _unitOfWork.Commit();
    }
}