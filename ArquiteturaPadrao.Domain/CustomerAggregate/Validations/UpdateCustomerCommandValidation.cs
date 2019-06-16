using ArquiteturaPadrao.Domain.CustomerAggregate.Commands;

namespace ArquiteturaPadrao.Domain.CustomerAggregate.Validations
{
    public class UpdateCustomerCommandValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}