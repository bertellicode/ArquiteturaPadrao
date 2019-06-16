using ArquiteturaPadrao.Domain.CustomerAggregate.Commands;

namespace ArquiteturaPadrao.Domain.CustomerAggregate.Validations
{
    public class RemoveCustomerCommandValidation : CustomerValidation<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}