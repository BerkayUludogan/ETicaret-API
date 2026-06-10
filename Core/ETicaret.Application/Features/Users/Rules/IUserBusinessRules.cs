namespace ETicaret.Application.Features.Users.Rules
{
    public interface IUserBusinessRules
    {
        Task UserEmailMustBeUnique(string email);
        Task UserNameMustBeUnique(string userName);
    }
}
