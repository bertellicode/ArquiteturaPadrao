using ArquiteturaPadrao.Domain.CustomerAggregate.Commands;

namespace ArquiteturaPadrao.Domain.CustomerAggregate.Validations
{
    public class RegisterNewCustomerCommandValidation : CustomerValidation<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}